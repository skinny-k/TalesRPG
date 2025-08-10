using UnityEditor;
using UnityEngine;

namespace SkinnyUtils 
{
    public class SkinnyGUI
    {
        public static float svs { get; private set; } = EditorGUIUtility.standardVerticalSpacing;
        public static float lineHeight { get; private set; } = EditorGUIUtility.singleLineHeight + svs;
    }
}
