using UnityEditor;
using UnityEngine;

namespace Koboct.Editor
{
    [CustomEditor(typeof(Game.TriggerOnTag))]
    public class TriggerOnTagEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Get reference to the target script (TriggerOnTag)
            Koboct.Game.TriggerOnTag script = (Koboct.Game.TriggerOnTag)target;

            // Display a dropdown for Unity tags
            script.tag = EditorGUILayout.TagField("Tag", script.tag);
            DrawDefaultInspector();

            // Save changes when something is modified
            if (GUI.changed)
            {
                EditorUtility.SetDirty(script);
            }
        }
    }
}