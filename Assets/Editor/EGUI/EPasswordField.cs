using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public class EPasswordField : Widget
    {
        private string _password;

        public string Password
        {
            get { return _password; }

            set { _password = value; }
        }

        protected override void OnDraw()
        {
            string value = EditorGUILayout.PasswordField(Content, Password,Option.Values);
            if (value != Password)
            {
                Password = value;
                this.DipatchEvent();
            }
        }
    }
}
