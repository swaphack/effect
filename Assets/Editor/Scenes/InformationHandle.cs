
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Scenes
{
    public class InformationHandle : UnityEditor.Editor
    {
        private List<UnityEditor.Editor> _allEditors = new List<UnityEditor.Editor>();

        void OnEnable()
        {
            MonoBehaviour obj = (MonoBehaviour)target;
            Component[] children = obj.gameObject.GetComponents<Component>();

            _allEditors.Add(CreateEditor(obj.gameObject));

            if (children != null && children.Length != 0)
            {
                for (int i = 0; i < children.Length; i++)
                {
                    /*
                    if (children[i].GetType() != typeof(InformationBehaviour))
                    {
                        _allEditors.Add(UnityEditor.Editor.CreateEditor(children[i]));
                    }
                    */
                }
            }
        }

        protected void OnSceneGUI()
        {
            Handles.BeginGUI();

            GUI.skin.label.normal.textColor = Color.black;

            EditorGUILayout.BeginScrollView(Vector2.zero);

            foreach (var item in _allEditors)
            {
                EditorGUILayout.BeginVertical();
                item.DrawHeader();
                EditorGUILayout.BeginVertical();
                item.DrawDefaultInspector();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();

            Handles.EndGUI();
        }

    }
}
