using UnityEngine;
using UnityEngine.UI;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.UI
{
    /// <summary>
    /// 文本
    /// </summary>
    public class UIText : Text
    {
        public delegate void TextFunc(UIText text);

        public event TextFunc OnTextChanged;
        /// <summary>
        /// 文本
        /// </summary>
        public override string text
        {
            get
            {
                return base.text;
            }
            set
            {
                this.SetString(value);
            }
        }

        private void DispatchTextChangedEvent()
        {
            if (OnTextChanged != null)
            {
                OnTextChanged(this);
            }
        }

        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="text"></param>
        public void SetString(string text)
        {
            var font = this.font;

            Vector2 extents = new Vector2(rectTransform.rect.width, 0);
            TextGenerationSettings setting = GetGenerationSettings(extents);

            TextGenerator cachedTextGenerator = this.cachedTextGenerator;
            TextGenerator cachedTextGeneratorForLayout = this.cachedTextGeneratorForLayout;

            float newHeight = cachedTextGeneratorForLayout.GetPreferredHeight(text, setting);
            rectTransform.SetHeight(newHeight);

            base.text = text;

            this.DispatchTextChangedEvent();
        }
    }
}
