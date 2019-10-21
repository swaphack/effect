
using UnityEngine;

namespace Assets.SDK.App
{
    public class AppInstance : MonoBehaviour
    {
        private bool _bPause = false;

        private bool _bFocus = false;

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


        private void Start()
        {
            Debug.Log("On Application Start");

            _app = this;

            this.Init();
        }

        protected virtual void Init()
        { 
        }

        protected virtual void Pause()
        {
            _app = null;
        }

        protected virtual void Resume()
        {
            _app = this;
        }

        protected virtual void Exit()
        {
            _bQuit = true;
        }
    }
}
