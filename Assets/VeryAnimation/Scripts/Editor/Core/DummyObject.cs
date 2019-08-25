﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Animations;
using System;
using System.Collections.Generic;

namespace VeryAnimation
{
    public class DummyObject
    {
        private VeryAnimationWindow vaw { get { return VeryAnimationWindow.instance; } }
        private VeryAnimation va { get { return VeryAnimation.instance; } }

        public GameObject gameObject { get; private set; }
        public Transform gameObjectTransform { get; private set; }
        public GameObject sourceObject { get; private set; }
        public Animator animator { get; private set; }
        public Animation animation { get; private set; }
        public GameObject[] bones { get; private set; }
        public Dictionary<GameObject, int> boneDic { get; private set; }
        public GameObject[] humanoidBones { get; private set; }
        public Transform humanoidHipsTransform { get; private set; }
        public HumanPoseHandler humanPoseHandler { get; private set; }
        public VeryAnimationEditAnimator vaEdit { get; private set; }
        public bool isTransformOrigin { get; private set; }
        public Dictionary<Renderer, Renderer> rendererDictionary { get; private set; }

        private MaterialPropertyBlock materialPropertyBlock;
        private UnityEditor.Animations.AnimatorController tmpAnimatorController;

        ~DummyObject()
        {
            if (vaEdit != null || gameObject != null)
            {
                EditorApplication.delayCall += () =>
                {
                    if (vaEdit != null)
                        Component.DestroyImmediate(vaEdit);
                    if (gameObject != null)
                        GameObject.DestroyImmediate(gameObject);
                };
            }
        }

        public void Initialize(GameObject sourceObject)
        {
            Release();

            this.sourceObject = sourceObject;
            gameObject = sourceObject != null ? GameObject.Instantiate<GameObject>(sourceObject) : new GameObject();
            gameObject.hideFlags |= HideFlags.HideAndDontSave | HideFlags.HideInInspector;
            gameObject.name = sourceObject.name;
            EditorCommon.DisableOtherBehaviors(gameObject);
            gameObjectTransform = gameObject.transform;

            animator = gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.fireEvents = false;
                animator.applyRootMotion = false;
                animator.updateMode = AnimatorUpdateMode.Normal;
                animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                if (animator.runtimeAnimatorController == null) //In case of Null, AnimationClip.SampleAnimation does not work, so create it.
                {
                    tmpAnimatorController = new UnityEditor.Animations.AnimatorController();
                    tmpAnimatorController.name = "Very Animation Temporary Controller";
                    tmpAnimatorController.hideFlags |= HideFlags.HideAndDontSave;
                    tmpAnimatorController.AddLayer("Very Animation Layer");
                    UnityEditor.Animations.AnimatorController.SetAnimatorController(animator, tmpAnimatorController);
                }
            }
            animation = gameObject.GetComponent<Animation>();

            UpdateBones();

            #region rendererDictionary
            {
                rendererDictionary = new Dictionary<Renderer, Renderer>();
                var sourceRenderers = sourceObject.GetComponentsInChildren<Renderer>(true);
                var objectRenderers = gameObject.GetComponentsInChildren<Renderer>(true);
                foreach (var sr in sourceRenderers)
                {
                    if (sr == null)
                        continue;
                    var spath = AnimationUtility.CalculateTransformPath(sr.transform, sourceObject.transform);
                    var index = ArrayUtility.FindIndex(objectRenderers, (x) => AnimationUtility.CalculateTransformPath(x.transform, gameObject.transform) == spath);
                    Assert.IsTrue(index >= 0);
                    if (index >= 0 && !rendererDictionary.ContainsKey(objectRenderers[index]))
                        rendererDictionary.Add(objectRenderers[index], sr);
                }
            }
            #endregion

            UpdateState();
        }
        public void Release()
        {
            if (tmpAnimatorController != null)
            {
                if (animator != null)
                    animator.runtimeAnimatorController = null;
                {
                    var layerCount = tmpAnimatorController.layers.Length;
                    for (int i = 0; i < layerCount; i++)
                        tmpAnimatorController.RemoveLayer(0);
                }
                UnityEditor.Animations.AnimatorController.DestroyImmediate(tmpAnimatorController);
                tmpAnimatorController = null;
            }
            animator = null;
            animation = null;
            if (vaEdit != null)
            {
                Component.DestroyImmediate(vaEdit);
                vaEdit = null;
            }
            if (gameObject != null)
            {
                GameObject.DestroyImmediate(gameObject);
                gameObject = null;
            }
            sourceObject = null;
        }

        public void SetOrigin()
        {
            if (gameObjectTransform.parent != null)
                gameObjectTransform.SetParent(null);
            gameObjectTransform.localPosition = Vector3.zero;
            gameObjectTransform.localRotation = Quaternion.identity;
            gameObjectTransform.localScale = Vector3.one;
            isTransformOrigin = true;
        }
        public void SetOutside()
        {
            gameObjectTransform.SetPositionAndRotation(new Vector3(10000f, 10000f, 10000f), Quaternion.identity);
            gameObjectTransform.localScale = Vector3.one;
            isTransformOrigin = false;
        }
        public void SetSource()
        {
            var t = sourceObject.transform;
            gameObjectTransform.SetPositionAndRotation(t.position, t.rotation);
            gameObjectTransform.localScale = t.localScale;
            isTransformOrigin = false;
        }

        public void SetColor(Color color)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            materialPropertyBlock.SetColor("_Color", color);
            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer == null)
                    continue;
                renderer.SetPropertyBlock(materialPropertyBlock);
            }
        }
        public void ResetColor()
        {
            materialPropertyBlock = null;
            foreach (var renderer in gameObject.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer == null)
                    continue;
                renderer.SetPropertyBlock(materialPropertyBlock);
            }
        }

        public void AddEditComponent()
        {
            Assert.IsNull(vaEdit);
            vaEdit = gameObject.AddComponent<VeryAnimationEditAnimator>();
            vaEdit.hideFlags |= HideFlags.HideAndDontSave;
        }
        public void RemoveEditComponent()
        {
            if (vaEdit != null)
            {
                Component.DestroyImmediate(vaEdit);
                vaEdit = null;
            }
        }

        public void SetRendererEnabled(bool enable)
        {
            foreach (var pair in rendererDictionary)
            {
                if (pair.Key == null || pair.Value == null) continue;
                pair.Key.enabled = enable;
            }
        }
        public void RendererForceUpdate()
        {
            //It is necessary to avoid situations where only display is not updated.
            foreach (var pair in rendererDictionary)
            {
                if (pair.Key == null || pair.Value == null) continue;
                pair.Key.enabled = !pair.Key.enabled;
                pair.Key.enabled = !pair.Key.enabled;
            }
        }

        public void UpdateState()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                if (bones[i] == null || va.bones[i] == null) continue;
                if (bones[i].activeSelf != va.bones[i].activeSelf)
                    bones[i].SetActive(va.bones[i].activeSelf);
            }
            foreach (var pair in rendererDictionary)
            {
                if (pair.Key == null || pair.Value == null) continue;
                if (pair.Key.enabled != pair.Value.enabled)
                    pair.Key.enabled = pair.Value.enabled;
            }
        }

        public void AnimatorRebind()
        {
            if (animator == null) return;
            if (!animator.isInitialized)
                animator.Rebind();
        }

        public void SampleAnimation(AnimationClip clip, float time)
        {
            AnimatorRebind();

            WrapMode? beforeWrapMode = null;
            try
            {
                if (vaw.animation != null)
                {
                    if (clip.wrapMode != WrapMode.Default)
                    {
                        beforeWrapMode = clip.wrapMode;
                        clip.wrapMode = WrapMode.Default;
                    }
                }

                clip.SampleAnimation(gameObject, time);
            }
            finally
            {
                if (beforeWrapMode.HasValue)
                {
                    clip.wrapMode = beforeWrapMode.Value;
                }
            }
        }

        public int BonesIndexOf(GameObject go)
        {
            if (boneDic != null && go != null)
            {
                int boneIndex;
                if (boneDic.TryGetValue(go, out boneIndex))
                {
                    return boneIndex;
                }
            }
            return -1;
        }

        private void UpdateBones()
        {
            #region Humanoid
            if (va.isHuman && animator != null)
            {
                if (!animator.isInitialized)
                    animator.Rebind();

                humanoidBones = new GameObject[HumanTrait.BoneCount];
                for (int bone = 0; bone < HumanTrait.BoneCount; bone++)
                {
                    var t = animator.GetBoneTransform((HumanBodyBones)bone);
                    if (t != null)
                    {
                        humanoidBones[bone] = t.gameObject;
                    }
                }
                humanoidHipsTransform = humanoidBones[(int)HumanBodyBones.Hips].transform;
                humanPoseHandler = new HumanPoseHandler(animator.avatar, va.uAnimator.GetAvatarRoot(animator));
                #region Avoiding Unity's bug
                {
                    //Hips You need to call SetHumanPose once if there is a scale in the top. Otherwise, the result of GetHumanPose becomes abnormal.
                    var hp = new HumanPose()
                    {
                        bodyPosition = new Vector3(0f, 1f, 0f),
                        bodyRotation = Quaternion.identity,
                        muscles = new float[HumanTrait.MuscleCount],
                    };
                    humanPoseHandler.SetHumanPose(ref hp);
                }
                #endregion
            }
            else
            {
                humanoidBones = null;
                humanoidHipsTransform = null;
                humanPoseHandler = null;
            }
            #endregion
            #region bones
            bones = EditorCommon.GetHierarchyGameObject(gameObject).ToArray();
            boneDic = new Dictionary<GameObject, int>(bones.Length);
            for (int i = 0; i < bones.Length; i++)
            {
                boneDic.Add(bones[i], i);
            }
            #endregion

            #region EqualCheck
            {/*
                Assert.IsTrue(bones.Length == va.bones.Length);
                for (int i = 0; i < bones.Length; i++)
                {
                    Assert.IsTrue(bones[i].name == va.bones[i].name);
                }
                if (va.isHuman)
                {
                    Assert.IsTrue(humanoidBones.Length == va.humanoidBones.Length);
                    for (int i = 0; i < humanoidBones.Length; i++)
                    {
                        Assert.IsTrue(humanoidBones[i].name == va.humanoidBones[i].name);
                    }
                }*/
            }
            #endregion
        }
    }
}
