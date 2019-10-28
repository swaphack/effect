using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using Assets.Foundation.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Editor.Tools.EditorControl
{
    /// <summary>
    /// 图片展示
    /// </summary>
    class TextureDisplay : UIWindow
    {
        private string _searchExpress = "";

        private Vector2 ImageSize = new Vector2(100, 100);

        private float InnerSpace = 20;

        public void OnEnable()
        {
 
        }

        /// <summary>
        /// 设置选中对象的图片
        /// </summary>
        /// <param name="fullpath"></param>
        private void SetSelectImageSprite(string fullpath)
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }

            UIImage newImage = Selection.activeGameObject.GetComponent<UIImage>();
            if (newImage != null)
            {
                var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(fullpath);
                newImage.texture = texture;
                EditorUtility.SetDirty(newImage);
                return;
            }

            Image image = Selection.activeGameObject.GetComponent<Image>();
            if (image != null)
            {
                var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(fullpath);
                var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 0.5f * Vector2.one);
                sprite.name = texture.name;
                image.sprite = sprite;
                EditorUtility.SetDirty(image);
            }
        }

        /// <summary>
        /// 创建图片预览
        /// </summary>
        /// <param name="allpath"></param>
        /// <returns></returns>
        private Widget CreateImagePreviewWidget(string[] allpath)
        {
            EditorScrollView scrollView = new EditorScrollView();
            scrollView.InnerSpace = InnerSpace;

            if (allpath == null || allpath.Length == 0)
            {
                return scrollView;
            }

            var frameSize = this.position.size;

            float w = frameSize.x - 40;
            w -= InnerSpace;
            int count = (int)(w / (ImageSize.x + InnerSpace));

            EditorHorizontalLayout hLayout = null;

            for (int i = 0; i < allpath.Length; i++)
            {
                if (i % count == 0)
                {
                    hLayout = new EditorHorizontalLayout();
                    hLayout.InnerSpace = InnerSpace;
                    scrollView.Add(hLayout);
                }

                EditorVerticalLayout vLayout = new EditorVerticalLayout();
                vLayout.Option.Width = ImageSize.x;
                hLayout.Add(vLayout);

                {
                    string fullpath = allpath[i];

                    string name = fullpath.Substring(fullpath.LastIndexOf('/') + 1);

                    GUIButton button = new GUIButton();
                    button.Option.Width = ImageSize.x;
                    button.Option.Height = ImageSize.y;
                    button.ImagePosition = ImagePosition.ImageAbove;
                    button.ImagePath = fullpath;
                    button.Tooltip = fullpath;
                    button.TriggerHandler = (Widget widget) =>
                    {
                        SetSelectImageSprite(button.ImagePath);
                        
                    };
                    vLayout.Add(button);
                    
                    var label = new GUILabel();
                    label.Option.Height = 25;
                    label.Option.Width = ImageSize.x;
                    label.Text = name;
                    label.FontSize = 10;
                    label.Alignment = TextAnchor.MiddleCenter;
                    vLayout.Add(label);
                }
            }
            return scrollView;
        }

        protected override void InitUI(UIWidget layout)
        {
            string express = string.Format("*{0}*.png", _searchExpress);

            EditorTextField textField = new EditorTextField();
            textField.Text = _searchExpress;
            textField.TriggerHandler = (Widget w) =>
            {
                _searchExpress = textField.Text;
            };
            layout.Add(textField);

            string[] allpath = EditorAssets.GetFilePaths(EditorAssets.Root, express);
            layout.Add(CreateImagePreviewWidget(allpath));
        }
    }
}
