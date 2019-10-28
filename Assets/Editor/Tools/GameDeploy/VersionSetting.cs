using System;
using System.IO;
using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using Assets.Foundation.Data;
using Assets.SDK.Project;
using UnityEngine;

namespace Assets.Editor.Tools.GameDeploy
{
    public class VersionSetting : UIWindow
    {
        private VersionDetail _detail = new VersionDetail();
        private string _fullpath;

        private void Load()
        {
            TextAsset textAsset = EditorAssets.LoadAssetAtPath<TextAsset>(_fullpath);
            if (textAsset != null)
            {
                _detail = ConfigHelper.LoadFromXmlText<VersionDetail>(textAsset.text);
            }
        }

        private void Save()
        {
            ConfigHelper.SaveToXmlFile<VersionDetail>(_detail, _fullpath);
        }

        private void OnEnable()
        {
            var fileName = _detail.GetType().Name;
            _fullpath = Path.Combine(EditorAssets.Root, string.Format("Resources/App/{0}.xml", fileName));
        }

        protected override void InitUI(UIWidget layout)
        {
            UIObjectFieldWidget objectField = new UIObjectFieldWidget(_detail.GetType().Name, _detail);
            objectField.OnValueChanged = (object value) =>
            {
                _detail = (VersionDetail)value;
            };
            layout.Add(objectField);

            layout.Add(new EditorHorizontalLine());

            UITextFieldWidget saveFileField = new UITextFieldWidget("File Path :", _fullpath);
            saveFileField.OnValueChanged = (object value) =>
            {
                _fullpath = (string)value;
            };
            layout.Add(saveFileField);

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
        }
    }
}
