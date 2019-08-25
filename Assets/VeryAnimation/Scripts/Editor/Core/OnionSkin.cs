using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.Animations;
using System;
using System.Linq;
using System.Collections.Generic;

namespace VeryAnimation
{
    public class OnionSkin
    {
        private VeryAnimationWindow vaw { get { return VeryAnimationWindow.instance; } }
        private VeryAnimation va { get { return VeryAnimation.instance; } }

        private class OnionSkinObject
        {
            public DummyObject dummyObject;
            private List<Material> createdMaterials;

            public bool active { get { return dummyObject.gameObject.activeSelf; } set { dummyObject.gameObject.SetActive(value); } }

            private static readonly int ShaderID_Color = Shader.PropertyToID("_Color");
            private static readonly int ShaderID_FaceColor = Shader.PropertyToID("_FaceColor");

            public OnionSkinObject(GameObject go)
            {
                dummyObject = new DummyObject();
                dummyObject.Initialize(go);
                #region ChangeMaterials
                createdMaterials = new List<Material>();
                {
                    foreach (var pair in dummyObject.rendererDictionary)
                    {
                        bool changeShader = pair.Key is SkinnedMeshRenderer;
                        if (!changeShader && pair.Key is MeshRenderer)
                        {
                            changeShader = true;
                            foreach (var comp in pair.Key.GetComponents<Component>())
                            {
                                if (comp.GetType().Name.StartsWith("TextMesh"))
                                {
                                    changeShader = false;
                                    break;
                                }
                            }
                        }
                        if (changeShader)
                        {
                            Shader shader;
#if UNITY_2018_1_OR_NEWER
                            if (GraphicsSettings.renderPipelineAsset != null)
                            {
                                shader = Shader.Find("Very Animation/OnionSkin-1pass");
                            }
                            else
#endif
                            {
                                shader = Shader.Find("Very Animation/OnionSkin-2pass");
                            }
                            var materials = new Material[pair.Key.sharedMaterials.Length];
                            for (int i = 0; i < materials.Length; i++)
                            {
                                var keyMat = pair.Key.sharedMaterials[i];
                                if (keyMat == null) continue;
                                Material mat;
                                {
                                    mat = new Material(shader);
                                    mat.hideFlags |= HideFlags.HideAndDontSave;
                                    #region SetTexture
                                    {
                                        Action<string> SetTexture = (name) =>
                                        {
                                            if (mat.mainTexture == null && keyMat.HasProperty(name))
                                                mat.mainTexture = keyMat.GetTexture(name);
                                        };
                                        SetTexture("_MainTex");
                                        SetTexture("_BaseColorMap");    //HDRP
                                        SetTexture("_BaseMap");         //LWRP
#if UNITY_2018_2_OR_NEWER
                                        if (mat.mainTexture == null)
                                        {
                                            foreach (var name in keyMat.GetTexturePropertyNames())
                                            {
                                                SetTexture(name);
                                            }
                                        }
#endif
                                    }
                                    #endregion
                                    createdMaterials.Add(mat);
                                }
                                materials[i] = mat;
                            }
                            pair.Key.sharedMaterials = materials;
                        }
                        else
                        {
                            var materials = new Material[pair.Key.sharedMaterials.Length];
                            for (int i = 0; i < materials.Length; i++)
                            {
                                var keyMat = pair.Key.sharedMaterials[i];
                                if (keyMat == null) continue;
                                Material mat;
                                {
                                    mat = Material.Instantiate<Material>(keyMat);
                                    mat.hideFlags |= HideFlags.HideAndDontSave;
                                    createdMaterials.Add(mat);
                                }
                                materials[i] = mat;
                            }
                            pair.Key.sharedMaterials = materials;
                        }
                    }
                }
                #endregion
                dummyObject.gameObject.SetActive(false);
            }
            public void Release()
            {
                if (dummyObject != null)
                {
                    dummyObject.Release();
                    dummyObject = null;
                }
                if (createdMaterials != null)
                {
                    foreach (var mat in createdMaterials)
                    {
                        if (mat != null)
                            Material.DestroyImmediate(mat);
                    }
                    createdMaterials = null;
                }
            }

            public void SetRenderQueue(int renderQueue)
            {
                foreach (var mat in createdMaterials)
                {
                    mat.renderQueue = renderQueue;
                }
            }

            public void SetColor(Color color)
            {
                foreach (var mat in createdMaterials)
                {
                    if (mat.HasProperty(ShaderID_Color))
                        mat.SetColor(ShaderID_Color, color);
                    if (mat.HasProperty(ShaderID_FaceColor))
                        mat.SetColor(ShaderID_FaceColor, color);
                }
            }
        }
        private Dictionary<int, OnionSkinObject> onionSkinObjects;

        public OnionSkin()
        {
            onionSkinObjects = new Dictionary<int, OnionSkinObject>();
        }
        public void Release()
        {
            foreach (var pair in onionSkinObjects)
            {
                pair.Value.Release();
            }
            onionSkinObjects.Clear();
        }

        public void Update()
        {
            if (!vaw.editorSettings.settingExtraOnionSkin)
            {
                Release();
                return;
            }
            var show = !va.uAw.GetPlaying() && !va.prefabMode;
            foreach (var pair in onionSkinObjects)
            {
                pair.Value.active = false;
            }
            if (!show) return;

            var lastFrame = va.GetLastFrame();

            if (vaw.editorSettings.settingExtraOnionSkinMode == EditorSettings.OnionSkinMode.Keyframes)
            {
                #region Keyframes
                float[] nextTimes = vaw.editorSettings.settingExtraOnionSkinNextCount > 0 ? new float[vaw.editorSettings.settingExtraOnionSkinNextCount] : null;
                float[] prevTimes = vaw.editorSettings.settingExtraOnionSkinPrevCount > 0 ? new float[vaw.editorSettings.settingExtraOnionSkinPrevCount] : null;
                va.uAw.GetNearKeyframeTimes(nextTimes, prevTimes);
                #region Next
                if (nextTimes != null)
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinNextCount; i++)
                    {
                        if (Mathf.Approximately(va.currentTime, nextTimes[i])) break;
                        frame = va.uAw.TimeToFrameRound(nextTimes[i]);
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame((i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinNextColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinNextCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinNextCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinNextMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #region Prev
                if (prevTimes != null)
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinPrevCount; i++)
                    {
                        if (Mathf.Approximately(va.currentTime, prevTimes[i])) break;
                        frame = va.uAw.TimeToFrameRound(prevTimes[i]);
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame(-(i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinPrevColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinPrevCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinPrevCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinPrevMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #endregion
            }
            else if (vaw.editorSettings.settingExtraOnionSkinMode == EditorSettings.OnionSkinMode.Frames)
            {
                #region Frames
                #region Next
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinNextCount; i++)
                    {
                        frame += vaw.editorSettings.settingExtraOnionSkinFrameIncrement;
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame((i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinNextColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinNextCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinNextCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinNextMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #region Prev
                {
                    var frame = va.uAw.GetCurrentFrame();
                    for (int i = 0; i < vaw.editorSettings.settingExtraOnionSkinPrevCount; i++)
                    {
                        frame -= vaw.editorSettings.settingExtraOnionSkinFrameIncrement;
                        if (frame < 0 || frame > lastFrame) break;
                        var oso = SetFrame(-(i + 1), va.GetFrameTime(frame));
                        var color = vaw.editorSettings.settingExtraOnionSkinPrevColor;
                        var rate = vaw.editorSettings.settingExtraOnionSkinPrevCount > 1 ? i / (float)(vaw.editorSettings.settingExtraOnionSkinPrevCount - 1) : 0f;
                        color.a = Mathf.Lerp(color.a, vaw.editorSettings.settingExtraOnionSkinPrevMinAlpha, rate);
                        oso.SetColor(color);
                    }
                }
                #endregion
                #endregion
            }
        }

        private OnionSkinObject SetFrame(int frame, float time)
        {
            OnionSkinObject oso;
            if(!onionSkinObjects.TryGetValue(frame, out oso))
            {
                oso = new OnionSkinObject(va.editGameObject);
                {
                    const int StartOffset = 20;
                    var offset = Math.Abs(frame * 2) + (frame > 0 ? 0 : 1);
                    oso.SetRenderQueue((int)RenderQueue.GeometryLast - (StartOffset - offset));
                }
                onionSkinObjects.Add(frame, oso);
            }

            oso.active = true;
            oso.dummyObject.SetSource();
            oso.dummyObject.UpdateState();
            oso.dummyObject.SampleAnimation(va.currentClip, time);

            return oso;
        }
    }
}
