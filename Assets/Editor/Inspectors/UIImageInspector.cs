using Assets.Editor.EGUI;
using Assets.Editor.Widgets;
using Assets.Foundation.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor.Inspectors
{
    [CustomEditor(typeof(UIImage))]
    class UIImageInspector : UIInspector
    {
        public UIImageInspector()
        {
        }

        protected override void InitUI(UIWidget layout)
        {
            UIImage image = this.GetTarget<UIImage>();
            /*
            UISpriteFieldWidget sprite = new UISpriteFieldWidget("Source Image", image.sprite);
            sprite.OnValueChanged = (object value) =>
            {
                image.sprite = (Sprite)value;
            };
            layout.Add(sprite);
            */
            UITextureFieldWidget texture = new UITextureFieldWidget("Source Texture", image.mainTexture);
            texture.OnValueChanged = (object value) =>
            {
                image.texture = (Texture2D)value;
                EditorUtility.SetDirty(image);
            };
            layout.Add(texture);
            
            UIColorFieldWidget color = new UIColorFieldWidget("Color", image.color);
            color.OnValueChanged = (object value) =>
            {
                image.color = (Color)value;
            };
            layout.Add(color);

            UIMaterialFieldWidget mat = new UIMaterialFieldWidget("Material", image.material);
            mat.OnValueChanged = (object value) =>
            {
                image.material = (Material)value;
            };
            layout.Add(mat);

            UIBooleanFieldWidget raycastTarget = new UIBooleanFieldWidget("Raycast Target", image.raycastTarget);
            raycastTarget.OnValueChanged = (object value) =>
            {
                image.raycastTarget = (bool)value;
            };
            layout.Add(raycastTarget);

            BButton btn = new BButton();
            btn.Text = "SetNativeSize";
            btn.TriggerHandler = (Widget w) => 
            {
                if (image.mainTexture != null)
                {
                    image.rectTransform.sizeDelta = new Vector2(image.mainTexture.width, image.mainTexture.height);
                }
            };
            layout.Add(btn);
        }
    }
}
