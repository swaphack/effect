using Assets.Foundation.Events;
using UnityEngine;

namespace Assets.Foundation.Effects
{
    /// <summary>
    /// 地图缩放
    /// </summary>
    public class MapScale : TouchScrollBehaviour
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
        private float _scaleDeltaPercent = 0.01f;

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

        public MapScale()
        {
        }

        /// <summary>
        /// 设置缩放比
        /// </summary>
        /// <param name="scale"></param>
        public void SetScale(float scale)
        {
            scale = Mathf.Clamp(scale, MinScale, MaxScale);
            this.transform.localScale = new Vector3(scale, scale, scale);
        }

        /// <summary>
        /// 缩放比增加固定值
        /// </summary>
        /// <param name="scaleDelta"></param>
        public void IncreaseScale(float scaleDelta)
        {
            float scale = this.transform.localScale.x;
            scale += scaleDelta;
            SetScale(scale);
        }

        public override void DoScale(float delta)
        {
            this.IncreaseScale(delta * _scaleDeltaPercent);
        }
    }
}
