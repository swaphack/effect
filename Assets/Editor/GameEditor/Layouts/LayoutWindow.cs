using System.Collections.Generic;
using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.GameEditor.Layouts
{
    public class LayoutWindow : UIWindow
    {
        enum OrderAxis
        {
            X,
            Y,
            Z
        }

        private GameObject[] _gameObjects;
        private OrderAxis _orderAxis;
        private TextAlignment _alignment;


        void OnEnable()
        {
            _gameObjects = Selection.gameObjects;
        }

        private void OrderGameObjects()
        {
            if (_gameObjects == null || _gameObjects.Length == 0)
            {
                return;
            }

            Vector3 centorPos = Vector3.zero;

            for (var i = 0; i < _gameObjects.Length; i++)
            {
                centorPos.x += _gameObjects[i].transform.localPosition.x;
                centorPos.y += _gameObjects[i].transform.localPosition.y;
                centorPos.z += _gameObjects[i].transform.localPosition.z;
            }

            centorPos /= _gameObjects.Length;

            if (_orderAxis == OrderAxis.X)
            {
                for (var i = 0; i < _gameObjects.Length; i++)
                {
                    var pos = _gameObjects[i].transform.localPosition;
                    pos.x = centorPos.x;
                    _gameObjects[i].transform.localPosition = pos;
                }
            }
            else if (_orderAxis == OrderAxis.Y)
            {
                for (var i = 0; i < _gameObjects.Length; i++)
                {
                    var pos = _gameObjects[i].transform.localPosition;
                    pos.y = centorPos.y;
                    _gameObjects[i].transform.localPosition = pos;
                }
            }
            else
            {
                for (var i = 0; i < _gameObjects.Length; i++)
                {
                    var pos = _gameObjects[i].transform.localPosition;
                    pos.z = centorPos.z;
                    _gameObjects[i].transform.localPosition = pos;
                }
            }
        }

        private void OrderBy(OrderAxis axis)
        {
            _orderAxis = axis;

            OrderGameObjects();
        }

        private void OrderBy(TextAlignment alignment)
        {
            _alignment = alignment;

            OrderGameObjects();
        }

        protected override void InitUI(UIWidget layout)
        {
            EditorHorizontalLayout hlayout1 = new EditorHorizontalLayout();
            layout.Add(hlayout1);
            GUIButton axisX = new GUIButton();
            axisX.Text = "X";
            axisX.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.X);
            };
            hlayout1.Add(axisX);

            GUIButton axisY = new GUIButton();
            axisY.Text = "Y";
            axisY.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.Y);
            };
            hlayout1.Add(axisY);

            GUIButton axisZ = new GUIButton();
            axisZ.Text = "Z";
            axisZ.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.Z);
            };
            hlayout1.Add(axisZ);

            EditorHorizontalLayout hlayout2 = new EditorHorizontalLayout();
            layout.Add(hlayout2);

            GUIButton left = new GUIButton();
            left.ImagePath = EditorAssets.GetResourcePath("Icons/layout_alignment_left.png");
            left.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(TextAlignment.Left);
            };
            hlayout2.Add(left);

            GUIButton center = new GUIButton();
            center.ImagePath = EditorAssets.GetResourcePath("Icons/layout_alignment_center.png");
            center.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(TextAlignment.Center);
            };
            hlayout2.Add(center);

            GUIButton right = new GUIButton();
            right.ImagePath = EditorAssets.GetResourcePath("Icons/layout_alignment_right.png");
            right.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(TextAlignment.Right);
            };
            hlayout2.Add(right);
        }
    }
}
