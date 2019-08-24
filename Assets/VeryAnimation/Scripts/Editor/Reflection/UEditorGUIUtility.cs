using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Reflection;

namespace VeryAnimation
{
    public class UEditorGUIUtility
    {
        private Func<string, Texture2D> dg_LoadIcon;
        private Func<object, int> dg_get_s_LastControlID;

        public UEditorGUIUtility()
        {
            var asmUnityEditor = Assembly.LoadFrom(InternalEditorUtility.GetEditorAssemblyPath());
            var editorGUIUtilityType = asmUnityEditor.GetType("UnityEditor.EditorGUIUtility");

            Assert.IsNotNull(dg_LoadIcon = (Func<string, Texture2D>)Delegate.CreateDelegate(typeof(Func<string, Texture2D>), null, editorGUIUtilityType.GetMethod("LoadIcon", BindingFlags.Static | BindingFlags.NonPublic)));
            Assert.IsNotNull(dg_get_s_LastControlID = EditorCommon.CreateGetFieldDelegate<int>(editorGUIUtilityType.GetField("s_LastControlID", BindingFlags.NonPublic | BindingFlags.Static)));
        }

        public Texture2D LoadIcon(string name)
        {
            return dg_LoadIcon(name);
        }

        public int GetLastControlID()
        {
            return dg_get_s_LastControlID(null);
        }
    }
}
