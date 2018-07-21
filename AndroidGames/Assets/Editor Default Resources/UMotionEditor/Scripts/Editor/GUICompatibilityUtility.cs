using System;
using UnityEngine;
using UnityEditor;

namespace UMotionEditor
{
    public static class GUICompatibilityUtility
    {
        //********************************************************************************
        // Public Properties
        //********************************************************************************

        public static event Action HierarchyChanged
        {
            add
            {
                #if UNITY_2018_1_OR_NEWER
                EditorApplication.hierarchyChanged += value;
                #else
                hierarchyChangedInternal += value;
                if (!hierarchyHelperEventSetup)
                {
                    EditorApplication.hierarchyWindowChanged += OnHierarchyChanged_HelperEvent;
                    hierarchyHelperEventSetup = true;
                }
                #endif
            }
            remove
            {
                #if UNITY_2018_1_OR_NEWER
                EditorApplication.hierarchyChanged -= value;
                #else
                hierarchyChangedInternal -= value;
                #endif
            }
        }

        //********************************************************************************
        // Private Properties
        //********************************************************************************

        //----------------------
        // Inspector
        //----------------------

        //----------------------
        // Internal
        //----------------------
        #if !UNITY_2018_1_OR_NEWER
        private static bool hierarchyHelperEventSetup = false;
        private static event Action hierarchyChangedInternal;
        #endif

        //********************************************************************************
        // Public Methods
        //********************************************************************************

        public static Color ColorField(GUIContent label, Color value, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
        {
            #if UNITY_2018_1_OR_NEWER
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, options);
            #else
            return EditorGUILayout.ColorField(label, value, showEyedropper, showAlpha, hdr, null, options);
            #endif
        }

        //********************************************************************************
        // Private Methods
        //********************************************************************************

        #if !UNITY_2018_1_OR_NEWER
        private static void OnHierarchyChanged_HelperEvent()
        {
            if (hierarchyChangedInternal != null)
            {
                hierarchyChangedInternal();
            }
        }
        #endif
    }
}