
using System;
using System.IO;
using Assets.Foundation.Common;
using Assets.Foundation.DataAccess;
using UnityEngine;

namespace Assets.App
{
    public class AppInstance : MonoBehaviour
    {
        private bool _bPause = false;

        private bool _bFocus = false;
        /// <summary>
        /// 是否保存日志，使用文件记录
        /// </summary>
        private bool _bSaveDebug = false;
        /// <summary>
        /// 是否保存日志，使用文件记录
        /// </summary>
        public bool IsSaveDebug
        {
            get
            {
                return _bSaveDebug;
            }
            set
            {
                _bSaveDebug = value;
            }
        }

        private static AppInstance _app;
        public static AppInstance App
        {
            get { return _app; }
        }
        /// <summary>
        /// 退出应用，防止重新生成
        /// </summary>
        private static bool _bQuit = false;

        public static bool IsQuit
        {
            get
            {
                return _bQuit;
            }
        }

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

            if (_bPause)
            {
                _bFocus = true;
            }
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

            _app = this;

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
            _bQuit = true;
        }
    }
}
