using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Game.Foundation.Effects
{
    class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //按下多长时间算长按
        private float timeLongPress = 0.1f;

        //是否按下
        private bool isPointerDown;

        //按下时刻
        private float timePointerDown;

        /// <summary>
        /// 长按事件
        /// </summary>
        public UnityEvent PressCallBack { get; set; }

        /// <summary>
        /// 放开长按事件
        /// </summary>
        public UnityEvent UpCallBack { get; set; }

        public PressButton()
        {
            PressCallBack = new UnityEvent();
            UpCallBack = new UnityEvent();
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
            UpCallBack.Invoke();
        }
    }
}
