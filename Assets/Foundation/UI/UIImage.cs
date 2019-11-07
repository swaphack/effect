using UnityEngine;
using UnityEngine.UI;

namespace Game.Foundation.UI
{
    /// <summary>
    /// 图片
    /// </summary>
    public class UIImage : Image
    {
        public Texture2D texture
        {
            set
            {
                if (value == null)
                {
                    this.sprite = null;
                    return;
                }
                var rect = new Rect(0, 0, value.width, value.height);
                var sprite = Sprite.Create(value, rect, 0.5f * Vector2.one);
                this.sprite = sprite;
            }
        }
    }
}
