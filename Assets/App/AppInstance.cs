
using System;
using System.IO;
using Game.Foundation.Common;
using Game.Foundation.DataAccess;
using UnityEngine;

namespace Game.App
{
    public class AppInstance : MonoBehaviour
    {
        private bool _bPause;

        private bool _bFocus;

        /// <summary>
        /// 是否保存日志，使用文件记录
        /// </summary>
        public bool IsSaveDebug { get; set; } = false;
        public static AppInstance App { get; private set; }

        public static bool IsQuit { get; private set; }

        private void OnEnable()
        {
            Debug.Log("On Application Enable");

            _bPause = false;
            _bFocus = false;
        }

        private void OnApplicationPause()
        {
            Debug.Log("On Application Pause");
#if UNITY_IPHONE || UNITY_ANDROID
            if (!_bPause)
            {
                Pause();
            }
            else
            {
                _bFocus = true;

            }
            _bPause = true;
#else
            if (!_bPause)
            {
                Pause();
            }
            _bPause = true;
#endif
        }


        private void OnApplicationFocus()
        {
            Debug.Log("On Application Focus");
#if UNITY_IPHONE || UNITY_ANDROID
            if (_bFocus)
            {
                Resume();
                _bPause = false;
                _bFocus = false;
            }

            _bFocus |= _bPause;
#elif UNITY_EDITOR
            if (!_bFocus)
            {
                Resume();
                _bFocus = true;
            }

            if (!Application.isPlaying)
            {
                Exit();
            }
#endif
        }

        private ILogHandler CreateFileLog()
        {
            if (!IsSaveDebug)
            {
                return null;
            }
            string logName = string.Format("log/{0}.txt", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
            string logPath = Path.Combine(FilePath.PersistentDataPath, logName);
            return new FileLogHandler(logPath);
        }

        private void InitLoggers()
        {
            ILogHandler logHandler = Debug.unityLogger.logHandler;
            Debug.unityLogger.logHandler = new LogHandlers(logHandler, CreateFileLog());
        }

        private void Awake()
        {
            this.InitLoggers();

            Debug.Log("On Application Awake");

            App = this;

            this.Init();
        }

        protected virtual void Init()
        { 
        }

        protected virtual void Pause()
        {
        }

        protected virtual void Resume()
        {
        }

        protected virtual void Exit()
        {
            IsQuit = true;
        }
    }
}
