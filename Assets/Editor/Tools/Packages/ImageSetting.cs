using Assets.Editor.EGUI;
using Assets.Editor.Widgets;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Tools
{
    /// <summary>
    /// 图片纹理设置
    /// </summary>
    class ImageSetting : UIWindow
    {
        class SettingParams
        {
            public int anisoLevel;
            public bool alphaIsTransparency;
            public FilterMode filterMode;
            public TextureWrapMode wrapMode;
            public TextureImporterType textureType;
            public int maxTextureSize;
            public TextureImporterCompression textureCompression;
            public bool sRGBTexture;
            public TextureImporterAlphaSource alphaSource;
            public SpriteImportMode spriteMode;
        }
        private SettingParams setting;


        private int[] TextureIntArray = new int[] { 0, 1, 2, 8, 7, 4, 6, 10 };
        private string[] TextureTypeString = new string[] { "Default", "Normal Map", "GUI", "Sprite", "Cursor", "Cookie", "Lightmap", "Single Channel" };

        private int[] SizeIntArray = new int[] { 32, 64, 128, 256, 512, 1024, 2048, 4096 };
        private string[] MaxSizeString = new string[] { "32", "64", "128", "256", "512", "1024", "2048", "4096" };

        /// <summary>
        /// 临时存储int[]
        /// </summary>
        private int[] IntArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };

        private string[] AlphaSourceString = new string[] { "None", "FromInput", "FromGrayScale" };
        private string[] FilterModeString = new string[] { "Point", "Bilinear", "Trilinear" };
        private string[] WrapModeString = new string[] { "Repeat", "Clamp" };
        private string[] FormatString = new string[] { "Uncompressed", "Compressed", "CompressedHQ", "CompressedLQ" };
        private string[] SpriteModeString = new string[] { "None", "Single", "Multiple", "Polygon" };


        public ImageSetting()
        {
            setting = new SettingParams();
            setting.sRGBTexture = true;
            setting.alphaSource = TextureImporterAlphaSource.FromInput;
            setting.alphaIsTransparency = true;
            setting.anisoLevel = 0;
            setting.filterMode = FilterMode.Bilinear;
            setting.wrapMode = TextureWrapMode.Clamp;
            setting.textureType = TextureImporterType.Sprite;
            setting.maxTextureSize = 1024;
            setting.textureCompression = TextureImporterCompression.Compressed;
            setting.sRGBTexture = true;
            setting.alphaSource = TextureImporterAlphaSource.FromInput;
            setting.spriteMode = SpriteImportMode.Single;
        }

        /// <summary>
        /// 获取贴图设置
        /// </summary>
        public TextureImporter GetTextureSettings(string path)
        {
            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            textureImporter.anisoLevel = setting.anisoLevel;
            textureImporter.alphaIsTransparency = setting.alphaIsTransparency;
            textureImporter.filterMode = setting.filterMode;
            textureImporter.wrapMode = setting.wrapMode;
            textureImporter.textureType = setting.textureType;
            textureImporter.maxTextureSize = setting.maxTextureSize;
            textureImporter.textureCompression = setting.textureCompression;
            textureImporter.spriteImportMode = setting.spriteMode;
            return textureImporter;
        }

        /// <summary>
        /// 循环设置选择的贴图
        /// </summary>
        private void FormatTextures()
        {
            Object[] textures = GetSelectedTextures();
            Selection.objects = new Object[0];
            foreach (Texture2D texture in textures)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                TextureImporter texImporter = GetTextureSettings(path);
                TextureImporterSettings tis = new TextureImporterSettings();
                texImporter.ReadTextureSettings(tis);
                texImporter.SetTextureSettings(tis);
                AssetDatabase.ImportAsset(path);
            }
        }

        /// <summary>
        /// 获取选择的贴图
        /// </summary>
        /// <returns></returns>
        private Object[] GetSelectedTextures()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }

        protected override void InitUI(UIWidget layout)
        {
            EVerticalLayout vLayout = new EVerticalLayout();
            layout.Add(vLayout);
            
            UIIntPopupFieldWidget textureType = new UIIntPopupFieldWidget("Texture Type", setting.textureType);
            textureType.Describes = TextureTypeString;
            textureType.Indexs = TextureIntArray;
            textureType.OnValueChanged = (object value) => 
            {
                setting.textureType = (TextureImporterType)value;
            };
            vLayout.Add(textureType);

            UIIntPopupFieldWidget spriteMode = new UIIntPopupFieldWidget("Sprite Mode", setting.spriteMode);
            spriteMode.Describes = SpriteModeString;
            spriteMode.Indexs = IntArray;
            spriteMode.OnValueChanged = (object value) =>
            {
                setting.spriteMode = (SpriteImportMode)value;
            };
            vLayout.Add(spriteMode);


            UIBooleanFieldWidget sRGBTexture = new UIBooleanFieldWidget("sRGBTexture", setting.sRGBTexture);
            sRGBTexture.OnValueChanged = (object value) =>
            {
                setting.sRGBTexture = (bool)value;
            };
            vLayout.Add(sRGBTexture);

            UIIntPopupFieldWidget alphaSource = new UIIntPopupFieldWidget("Alpha Source", setting.alphaSource);
            alphaSource.Describes = AlphaSourceString;
            alphaSource.Indexs = IntArray;
            alphaSource.OnValueChanged = (object value) =>
            {
                setting.alphaSource = (TextureImporterAlphaSource)value;
            };
            vLayout.Add(alphaSource);

            UIBooleanFieldWidget alphaIsTransparency = new UIBooleanFieldWidget("Alpha Is Transparency", setting.alphaIsTransparency);
            alphaIsTransparency.OnValueChanged = (object value) =>
            {
                setting.alphaIsTransparency = (bool)value;
            };
            vLayout.Add(alphaIsTransparency);

            UIIntPopupFieldWidget filterMode = new UIIntPopupFieldWidget("Filter Mode", setting.filterMode);
            filterMode.Describes = FilterModeString;
            filterMode.Indexs = IntArray;
            filterMode.OnValueChanged = (object value) =>
            {
                setting.filterMode = (FilterMode)value;
            };
            vLayout.Add(filterMode);

            UIIntPopupFieldWidget wrapMode = new UIIntPopupFieldWidget("Wrap Mode", setting.wrapMode);
            wrapMode.Describes = WrapModeString;
            wrapMode.Indexs = IntArray;
            wrapMode.OnValueChanged = (object value) =>
            {
                setting.wrapMode = (TextureWrapMode)value;
            };
            vLayout.Add(wrapMode);


            UIIntSlideFieldWidget anisoLevel = new UIIntSlideFieldWidget("Aniso Level", setting.anisoLevel);
            anisoLevel.MinValue = 0;
            anisoLevel.MaxValue = 9;
            anisoLevel.OnValueChanged = (object value) =>
            {
                setting.anisoLevel = (int)value;
            };
            vLayout.Add(anisoLevel);

            UIIntPopupFieldWidget maxTextureSize = new UIIntPopupFieldWidget("Max Size", setting.maxTextureSize);
            maxTextureSize.Describes = MaxSizeString;
            maxTextureSize.Indexs = SizeIntArray;
            maxTextureSize.OnValueChanged = (object value) =>
            {
                setting.maxTextureSize = (int)value;
            };
            vLayout.Add(maxTextureSize);

            UIIntPopupFieldWidget textureCompression = new UIIntPopupFieldWidget("Texture Compression", setting.textureCompression);
            textureCompression.Describes = FormatString;
            textureCompression.Indexs = IntArray;
            textureCompression.OnValueChanged = (object value) =>
            {
                setting.textureCompression = (TextureImporterCompression)value;
            };
            vLayout.Add(maxTextureSize);

            EHorizontalLine line = new EHorizontalLine();
            vLayout.Add(line);

            BButton btn = new BButton();
            btn.Text = "Format";
            btn.TriggerHandler = (Widget w) =>
            {
                FormatTextures();
            };
            vLayout.Add(btn);
        }
    }
}
