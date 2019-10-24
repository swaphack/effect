using Assets.Editor.Widgets;
using Assets.Foundation.UI;
using UnityEditor;

namespace Assets.Editor.Inspectors
{
    [CustomEditor(typeof(UIText))]
    class UITextInspector : UIInspector
    {
        public UITextInspector()
        {
            UseDefaultInspector = true;
        }

        protected override void InitUI(UIWidget layout)
        {
            UIText text = GetTarget<UIText>();

            GUIButton btn = new GUIButton();
            btn.Text = "Format";
            btn.TriggerHandler = (Widget w) => 
            {
                text.text = text.text;
            };
            layout.Add(btn);
        }
    }
}
