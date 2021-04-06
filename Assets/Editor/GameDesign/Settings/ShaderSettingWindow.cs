using Game.Editor.DataAccess;
<<<<<<< HEAD
using Game.Editor.Widgets;
using UnityEngine;

namespace Game.Editor.GameDesign.Settings
{
    public class ShaderSettingWindow : UIWindow
    {
        protected override void InitUI(UIWidget layout)
        {
            var tex = EditorAssets.LoadAssetAtPath<Texture>(EditorAssets.GetResourcePath("Textures/wooden-box.png"));
            var shader = Shader.Find("UI/Default");
            var mat = new Material(shader);

            EditorPreviewTexture previewTexture = new EditorPreviewTexture();
            previewTexture.Image = tex;
            previewTexture.Position = new Rect(0, 0, 128, 128);
            previewTexture.Mat = mat;

            layout.Add(previewTexture);
            
            var fields = shader.GetType().GetFields();

            var shaderKeywords = mat.shaderKeywords;
            if (shaderKeywords != null || shaderKeywords.Length != 0)
            {
                for (var i = 0; i < shaderKeywords.Length; i++)
                {
                    var lable = new GUILabel();
                    lable.Text = shaderKeywords[i];
                    layout.Add(lable);
                }
            }
        }
    }
}
=======
using Game.Editor.Widgets;
using UnityEngine;

namespace Game.Editor.GameDesign.Settings
{
    public class ShaderSettingWindow : UIWindow
    {
        protected override void InitUI(UIWidget layout)
        {
            var tex = EditorAssets.LoadAssetAtPath<Texture>(EditorAssets.GetResourcePath("Textures/wooden-box.png"));
            var shader = Shader.Find("UI/Default");
            var mat = new Material(shader);

            EditorPreviewTexture previewTexture = new EditorPreviewTexture();
            previewTexture.Image = tex;
            previewTexture.Position = new Rect(0, 0, 128, 128);
            previewTexture.Mat = mat;

            layout.Add(previewTexture);
            
            var fields = shader.GetType().GetFields();

            var shaderKeywords = mat.shaderKeywords;
            if (shaderKeywords != null || shaderKeywords.Length != 0)
            {
                for (var i = 0; i < shaderKeywords.Length; i++)
                {
                    var lable = new GUILabel();
                    lable.Text = shaderKeywords[i];
                    layout.Add(lable);
                }
            }
        }
    }
}
>>>>>>> 953175f322bcac1057ea6a522db447f36fa4f397
