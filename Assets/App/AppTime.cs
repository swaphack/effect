using UnityEngine;
using System.Collections;
using Assets.Foundation.Managers;

namespace Assets.App
{
    public class AppTime : SingletonBehaviour<AppTime>
    {
        /// <summary>
        /// 应用开始时间
        /// </summary>
        private float _startUpTime;
        /// <summary>
        /// 时间缩放比例
        /// </summary>
        private float _timeScale = 3;
        /// <summary>
        /// app开始时间
        /// </summary>
        private float _appStartTime;
        /// <summary>
        /// 时间缩放比例
        /// </summary>
        public float TimeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public float CurrentTime
        {
            get
            {
                return _appStartTime + (Time.realtimeSinceStartup - _startUpTime) * TimeScale;
            }
        }


        private void Start()
        {
            _startUpTime = Time.realtimeSinceStartup;
        }
    }
}


