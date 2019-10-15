using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EHelpBox : Widget
    {
        private string _message;
        private MessageType _type;

        public string Message
        {
            get { return _message; }

            set { _message = value; }
        }

        public MessageType Type
        {
            get { return _type; }

            set { _type = value; }
        }

        protected override void OnDraw()
        {
            EditorGUILayout.HelpBox(Message, Type);
        }
    }
}
