using System.Collections.Generic;
using Assets.Editor.Widgets;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Tools.GameDesign
{
    /// <summary>
    /// 对象创建
    /// </summary>
    public class GameObjectCreator : UIWindow
    {
        private Transform _transform;
        private int _width = 100;
        private int _height = 100;
        private int _count = 1;

        private Dictionary<Vector3, GameObject> _objects = new Dictionary<Vector3, GameObject>();

        void OnEnable()
        {
            _transform = Selection.activeTransform;
        }

        private void CreateGameObject()
        {
            if (_transform == null)
            {
                return;
            }

            int count = 0;

            while (count < _count)
            {
                int x = Random.Range(-_width, _width);
                int y = Random.Range(-_height, _height);
                var pos = new Vector3(x, 1, y);
                if (_objects.ContainsKey(pos))
                {
                    continue;
                }

                var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                go.transform.localPosition = pos;
                go.transform.SetParent(_transform);

                count++;
            }
        }

        protected override void InitUI(UIWidget layout)
        {
            UITransformFieldWidget transform = new UITransformFieldWidget("GameObject",  _transform);
            transform.OnValueChanged = (object value) => {
                _transform = (Transform)value;
            };
            layout.Add(transform);


            UIIntFieldWidget width = new UIIntFieldWidget("width", _width);
            width.OnValueChanged = (object value) => {
                _width = (int)value;
            };
            layout.Add(width);

            UIIntFieldWidget height = new UIIntFieldWidget("height", _height);
            height.OnValueChanged = (object value) =>
            {
                _height = (int)value;
            };
            layout.Add(height);

            UIIntSlideFieldWidget count = new UIIntSlideFieldWidget("count", _count);
            count.MinValue = 1;
            count.MaxValue = 100;
            count.OnValueChanged = (object value) =>
            {
                _count = (int)value;
            };
            layout.Add(count);

            GUIButton btn = new GUIButton();
            btn.Text = "Create";
            btn.TriggerHandler = (Widget w) =>
            {
                CreateGameObject();
            };
            layout.Add(btn);
        }
    }
}

