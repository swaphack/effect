using UnityEditor;
using UnityEngine;
using Assets.Foundation.Extensions;
using Assets.Foundation.UI;
using UnityEngine.UI;
using UnityEngine.Video;
using Assets.Foundation.DataAccess;
using UnityEngine.Rendering;

namespace Assets.Editor.Windows
{
    class UIExtensions
    {
        private static GameObject CreateUIRectTransform(float w, float h)
        {
            GameObject go = Selection.activeGameObject;
            if (go == null)
            {
                return null;
            }

            GameObject child = new GameObject();
            go.AddChild(child);

            RectTransform rect = child.AddComponent<RectTransform>();
            child.name = rect.GetType().Name;

            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.pivot = 0.5f * Vector2.one;
            /*
            rect.offsetMin = new Vector2((parentRect.rect.width - w) * 0.5f, (parentRect.rect.height - h) * 0.5f);
            rect.offsetMax = new Vector2((w - parentRect.rect.width) * 0.5f, (h - parentRect.rect.height) * 0.5f);
            */
            rect.SetMiddleWidth(w);
            rect.SetCenterHeight(h);

            EditorUtility.SetDirty(child);

            return child;
        }

        [MenuItem("GameObject/UIExtension/UI RectTransform", priority=10)]
        private static void CreateUIRectTransform()
        {
            CreateUIRectTransform(300, 300);
        }

        private static T CreateUIListView<T>() where T : UIListView
        {
            GameObject child = CreateUIRectTransform(300, 300);
            if (child == null)
            {
                return null;
            }

            T listView = child.AddComponent<T>();
            child.name = listView.GetType().Name;
            child.layer = 5;

            GameObject viewport = new GameObject();
            child.AddChild(viewport);

            viewport.name = "Viewport";

            RectTransform viewportRectTransform = viewport.AddComponent<RectTransform>();
            viewportRectTransform.anchorMin = Vector2.zero;
            viewportRectTransform.anchorMax = Vector2.one;
            viewportRectTransform.pivot = 0.5f * Vector2.one;
            viewportRectTransform.SetMiddleWidth(300);
            viewportRectTransform.SetCenterHeight(300);

            listView.viewport = viewportRectTransform;
            viewport.AddComponent<Mask>();
            viewport.AddComponent<Image>();


            GameObject content = new GameObject();
            viewport.AddChild(content);

            content.name = "Content";

            RectTransform contentRectTransform = content.AddComponent<RectTransform>();
            contentRectTransform.anchorMin = Vector2.zero;
            contentRectTransform.anchorMax = Vector2.one;
            contentRectTransform.pivot = 0.5f * Vector2.one;

            contentRectTransform.SetMiddleWidth(300);
            contentRectTransform.SetCenterHeight(300);

            listView.content = contentRectTransform;


            return listView;
        }

        [MenuItem("GameObject/UIExtension/UI ListView", priority = 10)]
        private static UIListView CreateUIListView()
        {
            return CreateUIListView<UIListView>();
        }

        [MenuItem("GameObject/UIExtension/UI Image")]
        private static UIImage CreateUIImage()
        {
            GameObject child = CreateUIRectTransform(200, 200);
            if (child == null)
            {
                return null;
            }

            UIImage image = child.AddComponent<UIImage>();
            child.name = image.GetType().Name;
            child.layer = 5;

            image.raycastTarget = true;
            image.type = Image.Type.Simple;

            return image;
        }

        [MenuItem("GameObject/UIExtension/UI Text", priority = 10)]
        private static UIText CreateUIText()
        {
            GameObject child = CreateUIRectTransform(160, 55);
            if (child == null)
            {
                return null;
            }

            UIText text = child.AddComponent<UIText>();
            child.name = text.GetType().Name;
            child.layer = 0;

            text.fontSize = 24;
            text.color = Color.black;
            text.text = "fasd fsadfdf dfdfsd";

            return text;
        }

        [MenuItem("GameObject/UIExtension/UI ScrollText", priority = 10)]
        private static void CreateUIScrollText()
        {
            UIScrollText listView = CreateUIListView<UIScrollText>();
            if (listView == null)
            {
                return;
            }

            UIText text = CreateUIText();
            if (text == null)
            {
                return;
            }
            
            listView.AddItem(text);

            text.text = "fasd fsadfdf dfdfsd";

            var layoutElement = text.gameObject.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = text.rectTransform.GetHeight();

            text.OnTextChanged += (UIText t) =>
            {
                layoutElement.preferredHeight = text.rectTransform.GetHeight();
            };
        }

        [MenuItem("GameObject/UIExtension/UI Video", priority = 10)]
        private static void CreateUIVideo()
        {
            VideoClip clip = AssetDatabase.LoadAssetAtPath<VideoClip>("Assets/Bundles/Videos/4.mp4");
            int width = 256;
            int height = 256;
            if (clip != null)
            {
                width = (int)clip.width;
                height = (int)clip.height;
            }

            GameObject child = CreateUIRectTransform(width, height);
            if (child == null)
            {
                return;
            }

            UIVideo video = child.AddComponent<UIVideo>();
            child.name = video.GetType().Name;
            child.layer = 5;

            video.raycastTarget = true;
            video.type = Image.Type.Simple;
            
            var videoPlayer = video.GetComponent<VideoPlayer>();
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            videoPlayer.controlledAudioTrackCount = 1;
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = clip;

            var audioSrc = video.GetComponent<AudioSource>();
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, audioSrc);
            
            Material mat = new Material(Shader.Find("Unlit/Texture"));
            video.material = mat;

            RenderTexture texture = new RenderTexture(width, height, 32);
            texture.name = "Video Render Texture";
            texture.dimension = TextureDimension.Tex2D;
            texture.format = RenderTextureFormat.ARGB32;
            mat.mainTexture = texture;
            videoPlayer.targetTexture = texture;
        }
    }
}

