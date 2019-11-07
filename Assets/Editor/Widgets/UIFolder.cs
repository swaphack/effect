using UnityEditor;

namespace Game.Editor.Widgets
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

        private GUITextField textField;
        private GUIButton button;

        public UIFolder()
        {
            EditorHorizontalLayout layout = new EditorHorizontalLayout();
            this.Add(layout);

            textField = new GUITextField();
            layout.Add(textField);

            button = new GUIButton();
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
