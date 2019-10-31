using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Foundation.Effects
{
    /// <summary>
    /// 相机快照
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraSnapshot : MonoBehaviour
    {
        private Texture2D _texture2D;

        void OnPostRender()
        {
            //在每次相机渲染完成时再删除上一帧的texture
            if (_texture2D != null)
            {
                Destroy(_texture2D);
            }
            var tempCamera = this.GetComponent<Camera>();

            RenderTexture rt = tempCamera.targetTexture;
            if (rt == null)
            {
                rt = new RenderTexture(Screen.width, Screen.height, 32);
                rt.name = "Snapshot Texture";
                rt.dimension = TextureDimension.Tex2D;
                rt.format = RenderTextureFormat.ARGB32;
                tempCamera.targetTexture = rt;
            }
            RenderTexture.active = rt;

            _texture2D = new Texture2D(rt.width, rt.height);
            _texture2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            _texture2D.Apply();

            this.SetTexture(_texture2D);
        }

        protected virtual void SetTexture(Texture2D texture)
        { 
        }
    }

    /// <summary>
    /// 图片快照
    /// </summary>
    public class ImageSnapshot : CameraSnapshot
    {
        public Image Image;

        protected override void SetTexture(Texture2D texture)
        {
            if (Image == null)
            {
                return;
            }
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 0.5f * Vector2.one);
            Image.sprite = sprite;
        }
    }

    /// <summary>
    /// 材质快照
    /// </summary>
    public class MaterialSnapshot : CameraSnapshot
    {
        public Material Material;

        protected override void SetTexture(Texture2D texture)
        {
            if (Material != null)
            {
                Material.mainTexture = texture;
            }
        }
    }
}
