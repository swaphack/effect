using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Assets.Foundation.Effects
{
    class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //按下多长时间算长按
        private float timeLongPress = 0.1f;

        //是否按下
        private bool isPointerDown = false;

        //按下时刻
        private float timePointerDown = 0;

        private UnityEvent _pressCallBack;
        /// <summary>
        /// 长按事件
        /// </summary>
        public UnityEvent PressCallBack
        {
            get { return _pressCallBack; }
            set { _pressCallBack = value; }
        }

        public PressButton()
        {
            _pressCallBack = new UnityEvent();
        }

        void Update()
        {
            float span = Time.time - timePointerDown;
            if (isPointerDown && span > timeLongPress)
            {
                PressCallBack.Invoke();
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            isPointerDown = true;
            timePointerDown = Time.time;

            PressCallBack.Invoke();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
        }
    }
}
