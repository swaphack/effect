using UnityEditor;


namespace Assets.Editor.EGUI
{
    public class EDelayedTextField : Widget
    {

        protected override void OnDraw()
        {
            string value = EditorGUILayout.DelayedTextField(Content, Text,Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }
}
