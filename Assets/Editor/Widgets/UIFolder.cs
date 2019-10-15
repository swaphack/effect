using Assets.Editor.EGUI;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    /// <summary>
    /// 文件夹控件
    /// </summary>
    public class UIFolder : UIWidget
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Filepath
        {
            get { return textField.Text; }
            set { textField.Text = value; }
        }

        private BTextField textField;
        private BButton button;

        public UIFolder()
        {
            EHorizontalLayout layout = new EHorizontalLayout();
            this.Add(layout);

            textField = new BTextField();
            layout.Add(textField);

            button = new BButton();
            button.TriggerHandler = this.OnClick;
            layout.Add(button);
            
        }

        protected override void UpdateContent()
        {
            if (Text == null)
            {
                return;
            }
            button.Text = Text;
        }

        private void OnClick(Widget w)
        {
            string filepath = EditorUtility.OpenFolderPanel("Choose Directory", Filepath, "");
            if (string.IsNullOrEmpty(filepath))
            {
                return;
            }
            Filepath = filepath.Replace("\\", "/");
        }
    }
}
