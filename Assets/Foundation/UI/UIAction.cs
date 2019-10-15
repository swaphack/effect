using Assets.Foundation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Foundation.UI
{
    public class UIAction : ActionInterval
    {
        protected override void DoInterval(float dt)
        {

        }
    }

    /// <summary>
    /// 进度条
    /// </summary>
    public abstract class SliderAction : UIAction
    {
        /// <summary>
        /// 开始值
        /// </summary>
        private float _source;

        /// <summary>
        /// 差距
        /// </summary>
        private float _different;

        /// <summary>
        /// 结束值
        /// </summary>
        private float _destination;
        protected float Different
        {
            get { return _different; }
            set { _different = value; }
        }

        public float Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public float Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }
    }

    /// <summary>
    /// 进度条
    /// </summary>
    public class SliderFromTo : SliderAction
    {
        protected override void DoStep(float percent)
        {
            Slider slider = GetTarget<Slider>();
            if (slider == null)
            {
                return;
            }
            slider.value = Source + percent * Different;
            if (Mathf.Approximately(percent, 1.0f))
            {
                slider.value = Destination;
            }
        }

        protected override void DoWithTarget()
        {
            Slider slider = GetTarget<Slider>();
            if (slider == null)
            {
                return;
            }

            Source = Mathf.Clamp(Source, slider.minValue, slider.maxValue);
            Destination = Mathf.Clamp(Destination, slider.minValue, slider.maxValue);
            Different = Destination - Source;
        }

        public static SliderFromTo Create(float time, float from, float to)
        {
            var action = new SliderFromTo();
            action.TotalTime = time;
            action.Source = from;
            action.Destination = to;
            return action;
        }
    }

    /// <summary>
    /// 进度条
    /// </summary>
    public class SliderTo : SliderFromTo
    {
        protected override void DoStep(float percent)
        {
            base.DoStep(percent);
        }

        protected override void DoWithTarget()
        {
            Slider slider = GetTarget<Slider>();
            if (slider == null)
            {
                return;
            }
            Source = slider.value;
            base.DoWithTarget();
        }

        public static SliderTo Create(float time, float to)
        {
            var action = new SliderTo();
            action.TotalTime = time;
            action.Destination = to;
            return action;
        }
    }

    public abstract class ImageProgress : SliderAction
    {
        public enum OriginCircle
        {
            Bottom = 0,
            Right = 1,
            Top = 2,
            Left = 3,
        }

        /// <summary>
        /// horizontal left-right
        /// vertical botton-up
        /// </summary>
        public enum OriginBar
        {
            Low = 0,
            Height = 1,
        }

        /// <summary>
        /// 图片进度条形状
        /// </summary>
        private Image.FillMethod _shape;
        /// <summary>
        /// 圆形开始位置
        /// </summary>
        private OriginCircle _originCircle;
        /// <summary>
        /// 条形开始位置
        /// </summary>
        private OriginBar _originBar;
        
        public Image.FillMethod Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        public ImageProgress.OriginBar BarOrigin
        {
            get { return _originBar; }
            set { _originBar = value; }
        }

        public ImageProgress.OriginCircle CircleOrigin
        {
            get { return _originCircle; }
            set { _originCircle = value; }
        }

        public ImageProgress()
        {
            Shape = Image.FillMethod.Horizontal;
            BarOrigin = ImageProgress.OriginBar.Low;
            CircleOrigin = ImageProgress.OriginCircle.Bottom;
        }
    }

    /// <summary>
    /// 图片的进度条播放方式
    /// </summary>
    public class ImageProgressFromTo : ImageProgress
    {
        protected override void DoStep(float percent)
        {
            Image image = GetTarget<Image>();
            if (image == null)
            {
                return;
            }

            image.fillAmount = Source + percent * Different;
            if (Mathf.Approximately(percent, 1.0f))
            {
                image.fillAmount = Destination;
            }
        }

        protected override void DoWithTarget()
        {
            Image image = GetTarget<Image>();
            if (image == null)
            {
                return;
            }
            image.type = Image.Type.Filled;
            image.fillMethod = Shape;
            if (Shape == Image.FillMethod.Horizontal || Shape == Image.FillMethod.Vertical)
            {
                image.fillOrigin = (int)BarOrigin;
            }
            else
            {
                image.fillOrigin = (int)CircleOrigin;
            }
            

            Source = Mathf.Clamp(Source, 0, 1);
            Destination = Mathf.Clamp(Destination, 0, 1);
            Different = Destination - Source;
        }

        public static ImageProgressFromTo Create(float time, float from, float to)
        {
            var action = new ImageProgressFromTo();
            action.TotalTime = time;
            action.Source = from;
            action.Destination = to;
            return action;
        }
    }

    public class ImageProgressTo : ImageProgress
    {
        protected override void DoStep(float percent)
        {
            base.DoStep(percent);
        }

        protected override void DoWithTarget()
        {
            Image image = GetTarget<Image>();
            if (image == null)
            {
                return;
            }

            Source = image.fillAmount;
            base.DoWithTarget();
        }

        public static ImageProgressTo Create(float time, float to)
        {
            var action = new ImageProgressTo();
            action.TotalTime = time;
            action.Destination = to;
            return action;
        }
    }
}
