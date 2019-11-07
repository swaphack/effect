using Game.Editor.Widgets;
using Game.Foundation.UI;
using UnityEditor;

namespace Game.Editor.Inspectors
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
