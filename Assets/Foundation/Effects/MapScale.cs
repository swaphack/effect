using Game.Foundation.Events;
using UnityEngine;

namespace Game.Foundation.Effects
{
    /// <summary>
    /// 地图缩放
    /// </summary>
    public class MapScale : ScrollBehaviour
    {
        /// <summary>
        /// 最小缩放比例
        /// </summary>
        [SerializeField]
        private float _minScale = 0.8f;
        /// <summary>
        /// 最大缩放比例
        /// </summary>
        [SerializeField]
        private float _maxScale = 1.0f;
        /// <summary>
        /// 缩放比差距百分比
        /// </summary>
        [SerializeField]
        private float _scaleDeltaPercent = 0.002f;
        /// <summary>
        /// 影响目标
        /// </summary>
        [SerializeField]
        private RectTransform _content;

        public float MinScale
        {
            get { return _minScale; }
            set { _minScale = value; }
        }
        public float MaxScale
        {
            get { return _maxScale; }
            set { _maxScale = value; }
        }

        public float ScaleDeltaPercent
        {
            get { return _scaleDeltaPercent; }
            set { _scaleDeltaPercent = value; }
        }

        public RectTransform Content
        {
            get { return _content; }
            set { _content = value; }
        }

        protected override void UpdateEventStatus(bool status)
        {
            if (ScrollManager.Instance != null)
            {
                if (status)
                {
                    ScrollManager.Instance.AddBehaviour(this);
                }
                else
                {
                    ScrollManager.Instance.RemoveBehaviour(this);
                }
            }
        }

        /// <summary>
        /// 设置缩放比
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(float scale)
        {
            if (this.Content == null)
            {
                return;
            }

            scale = Mathf.Clamp(scale, MinScale, MaxScale);
            this.Content.localScale = new Vector3(scale, scale, scale);
            this.UpdatePivot();
        }

        /// <summary>
        /// 缩放比增加固定值
        /// </summary>
        /// <param name="scaleDelta"></param>
        public void IncreaseScale(float scaleDelta)
        {
            if (this.Content == null)
            {
                return;
            }
            float scale = this.Content.localScale.x;
            scale += scaleDelta;
            SetScale(scale);
        }

        public override void DoScale(float delta)
        {
            Debug.LogFormat("MapScale {0}", delta);
            this.IncreaseScale(delta * _scaleDeltaPercent);
        }

        private void UpdatePivot()
        {
            this.Content.anchorMin = Vector2.zero;
            this.Content.anchorMax = Vector2.one;

            var rect = this.GetComponent<RectTransform>();

            float fx = (Mathf.Abs(this.Content.offsetMin.x) + rect.rect.width * 0.5f) / this.Content.rect.width;
            float fy = (Mathf.Abs(this.Content.offsetMin.y) + rect.rect.height * 0.5f) / this.Content.rect.height;

            var pivot = new Vector2(fx, fy);
            this.Content.pivot = pivot;
        }

        void Update()
        {
            this.UpdatePivot();
        }
    }
}
