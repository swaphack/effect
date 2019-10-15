using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public abstract class ELayout : Layout
    {
    }

    /// <summary>
    /// 水平布局
    /// </summary>
    public class EHorizontalLayout : ELayout
    {
        public EHorizontalLayout()
        {
        }

        protected override void BeginDraw()
        {
            EditorGUILayout.BeginHorizontal(Option.Values);
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 垂直布局
    /// </summary>
    public class EVerticalLayout : ELayout
    {
        public EVerticalLayout()
        {
        }

        protected override void BeginDraw()
        {
            EditorGUILayout.BeginVertical(Option.Values);
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
