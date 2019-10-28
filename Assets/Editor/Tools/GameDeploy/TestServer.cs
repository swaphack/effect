using System;
using Assets.Editor.Widgets;

namespace Assets.Editor.Tools.GameDeploy
{
    public class TestServer : UIWindow
    {
        private string _ip;
        private int _port;
        private GameServer _server;

        public TestServer()
        {
            _ip = "127.0.0.1";
            _port = 9547;
            _server = new GameServer();
        }

        protected override void InitUI(UIWidget layout)
        {
            UITextFieldWidget fieldIP = new UITextFieldWidget("IP", _ip);
            fieldIP.OnValueChanged = (object value) =>
            {
                _ip = (string)value;
            };
            layout.Add(fieldIP);

            UITextFieldWidget fieldPort = new UITextFieldWidget("Port", _port);
            fieldPort.OnValueChanged = (object value) =>
            {
                _port = (int)value;
            };
            layout.Add(fieldPort);

            GUIButton btnStart = new GUIButton();
            btnStart.Text = "Start";
            btnStart.TriggerHandler = (Widget w) =>
            {
                _server.SetEndPoint(_ip, _port);
                _server.StartAccept();
            };
            layout.Add(btnStart);

            GUIButton btnStop = new GUIButton();
            btnStop.Text = "Stop";
            btnStop.TriggerHandler = (Widget w) =>
            {
                _server.Stop();
            };
            layout.Add(btnStop);
        }
    }
}


