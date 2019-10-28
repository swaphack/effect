using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using UnityEngine;

namespace Assets.Editor.GameDesign.Settings
{
    public class MaterialSettingWindow : UIWindow
    {
        protected override void InitUI(UIWidget layout)
        {
            var tex = EditorAssets.LoadAssetAtPath<Texture>(EditorAssets.GetResourcePath("Textures/wooden-box.png"));
            var shader = Shader.Find("UI/Default");
            var mat = new Material(shader);

            EditorVerticalLayout vLayout = new EditorVerticalLayout();
            vLayout.Option.Height = 128;
            vLayout.Option.Width = 128;

            layout.Add(vLayout);

            EditorPreviewTexture previewTexture = new EditorPreviewTexture();
            previewTexture.Image = tex;
            previewTexture.Position = new Rect(0, 0, 128, 128);
            previewTexture.Mat = mat;

            vLayout.Add(previewTexture);

            var textureField = new UITextureFieldWidget("Texture", tex);
            textureField.OnValueChanged = (object value) =>
            {
                previewTexture.Image = (Texture)value;
            };
            layout.Add(textureField);

            var shaderField = new UIShaderFieldWidget("Shader", shader);
            shaderField.OnValueChanged = (object value) =>
            {
                previewTexture.Mat = new Material((Shader)value);
            };
            layout.Add(shaderField);

            var matField = new UIMaterialFieldWidget("Material", mat);
            matField.OnValueChanged = (object value) =>
            {
                previewTexture.Mat = (Material)value;
            };
            layout.Add(matField);
        }
    }
}
