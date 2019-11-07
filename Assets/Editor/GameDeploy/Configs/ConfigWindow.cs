using System;
using System.IO;
using Game.Editor.DataAccess;
using Game.Editor.Widgets;
using Game.SDK.Project;
using UnityEngine;

namespace Game.Editor.GameDeploy.Configs
{
    public class ConfigWindow<T> : UIWindow where T : new()
    {
        /// <summary>
        /// 版本信息
        /// </summary>
        private T _config = new T();
        /// <summary>
        /// 存储路径
        /// </summary>
        private string _fullpath;

        /// <summary>
        /// 加载配置
        /// </summary>
        private void Load()
        {
            TextAsset textAsset = EditorGame.LoadAssetAtPath<TextAsset>(_fullpath);
            if (textAsset != null)
            {
                _config = ConfigHelper.LoadFromXmlText<T>(textAsset.text);
            }
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        private void Save()
        {
            ConfigHelper.SaveToXmlFile<T>(_config, _fullpath);
        }

        private void OnEnable()
        {
            var fileName = _config.GetType().Name;
            _fullpath = Path.Combine(EditorGame.Root, string.Format("Resources/App/{0}.xml", fileName));
            this.Load();
        }

        protected override void InitUI(UIWidget layout)
        {
            UIObjectFieldWidget objectField = new UIObjectFieldWidget(_config.GetType().Name, _config);
            objectField.OnValueChanged = (object value) =>
            {
                _config = (T)value;
                this.Save();
            };
            layout.Add(objectField);

            layout.Add(new EditorHorizontalLine());

            UITextFieldWidget saveFileField = new UITextFieldWidget("File Path :", _fullpath);
            saveFileField.OnValueChanged = (object value) =>
            {
                _fullpath = (string)value;
            };
            layout.Add(saveFileField);

            /*
            GUIButton btnLoad = new GUIButton();
            btnLoad.Text = "Load";
            btnLoad.TriggerHandler = (Widget w) =>
            {
                this.Load();
                Dirty = true;
            };
            layout.Add(btnLoad);

            GUIButton btnSave = new GUIButton();
            btnSave.Text = "Save";
            btnSave.TriggerHandler = (Widget w) =>
            {
                this.Save();
            };
            layout.Add(btnSave);
            */
        }
    }
}
