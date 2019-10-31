using System;
using System.Collections.Generic;
using Assets.Editor.DataAccess;
using Assets.Editor.Widgets;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Layouts
{
    public class LayoutWindow : UIWindow
    {
        enum OrderAxis
        {
            X=1,
            Y=2,
            Z=4,
        }

        private GameObject[] _gameObjects;
        private int _orderAxis;
        private TextAlignment _alignment;
        private float _distance = 1;


        void OnEnable()
        {
            _gameObjects = Selection.gameObjects;
        }

        private void OrderByCenter()
        {
            var centerPos = Vector3.zero;
            for (var i = 0; i < _gameObjects.Length; i++)
            {
                centerPos += _gameObjects[i].transform.position;
            }

            int axisX = _orderAxis & (int)(OrderAxis.X);
            int axisY = _orderAxis & (int)(OrderAxis.X);
            int axisZ = _orderAxis & (int)(OrderAxis.X);

            float half = _gameObjects.Length * 0.5f;
            centerPos /= _gameObjects.Length;
            for (var i = 0; i < _gameObjects.Length; i++)
            {
                var pos = _gameObjects[i].transform.position;
                if (axisX != 0)
                {
                    pos.x = (i + 1 - half) * _distance + centerPos.x;
                }
                if (axisY != 0)
                {
                    pos.y = (i + 1 - half) * _distance + centerPos.y;
                }
                if (axisZ != 0)
                {
                    pos.z = (i + 1 - half) * _distance + centerPos.z;
                }

                _gameObjects[i].transform.position = pos;
            }
        }

        private void OrderBySide()
        {
            var increase = Vector3.zero;

            int axisX = _orderAxis & (int)(OrderAxis.X);
            int axisY = _orderAxis & (int)(OrderAxis.X);
            int axisZ = _orderAxis & (int)(OrderAxis.X);

            if (axisX != 0)
            {
                Array.Sort(_gameObjects, (object x, object y) => {
                    GameObject a = (GameObject)x;
                    GameObject b = (GameObject)y;
                    return a.transform.position.x.CompareTo(b.transform.position.x);
                });

                if (_alignment == TextAlignment.Left)
                {
                    increase.x = _distance;
                }
                else if (_alignment == TextAlignment.Right)
                {
                    Array.Reverse(_gameObjects);
                    increase.x = -_distance;
                }
            }

            if (axisY != 0)
            {
                Array.Sort(_gameObjects, (object x, object y) => {
                    GameObject a = (GameObject)x;
                    GameObject b = (GameObject)y;
                    return a.transform.position.y.CompareTo(b.transform.position.y);
                });

                if (_alignment == TextAlignment.Left)
                {
                    increase.y = _distance;
                }
                else if (_alignment == TextAlignment.Right)
                {
                    Array.Reverse(_gameObjects);
                    increase.y = -_distance;
                }
            }

            if (axisZ != 0)
            {
                Array.Sort(_gameObjects, (object x, object y) => {
                    GameObject a = (GameObject)x;
                    GameObject b = (GameObject)y;
                    return a.transform.position.z.CompareTo(b.transform.position.z);
                });

                if (_alignment == TextAlignment.Left)
                {
                    increase.z = _distance;
                }
                else if (_alignment == TextAlignment.Right)
                {
                    Array.Reverse(_gameObjects);
                    increase.z = -_distance;
                }
            }

            var firstPos = _gameObjects[0].transform.position;
            var increasePos = increase;

            for (var i = 1; i < _gameObjects.Length; i++)
            {
                var pos = _gameObjects[i].transform.position;
                if (!Mathf.Approximately(increase.x, 0)) pos.x = firstPos.x + increasePos.x;
                else if (!Mathf.Approximately(increase.y, 0)) pos.y = firstPos.y + increasePos.y;
                else if (!Mathf.Approximately(increase.z, 0)) pos.z = firstPos.z + increasePos.z;

                _gameObjects[i].transform.position = pos;

                increasePos += increase;
            }
        }

        private void OrderGameObjects()
        {
            if (_gameObjects == null || _gameObjects.Length == 0)
            {
                return;
            }

            if (_alignment == TextAlignment.Center)
            {
                this.OrderByCenter();   
            }
            else
            {
                this.OrderBySide();
            }
        }

        private void OrderBy(OrderAxis axis)
        {
            var value = (int)axis;
            if ((_orderAxis & value) == value)
            {
                _orderAxis -= value;
            }
            else
            {
                _orderAxis += value;
            }
        }

        private void OrderBy(TextAlignment alignment)
        {
            _alignment = alignment;

            OrderGameObjects();
        }

        private string GetText()
        {
            string text = "";
            var valueX = (int)OrderAxis.X;
            var valueY = (int)OrderAxis.Y;
            var valueZ = (int)OrderAxis.Z;
            if ((_orderAxis & valueX) == valueX)
            {
                text += "X,";
            }
            if ((_orderAxis & valueY) == valueY)
            {
                text += "Y,";
            }
            if ((_orderAxis & valueZ) == valueZ)
            {
                text += "Z,";
            }
            if (text.Length > 0)
            {
                text = text.Substring(0, text.Length - 1);
            }

            return text;
        }

        private void SetText(UITextFieldWidget aixs)
        {
            aixs.GetFieldWidget<EditorTextField>().Value = GetText();
        }

        protected override void InitUI(UIWidget layout)
        {
            UIFloatFieldWidget distance = new UIFloatFieldWidget("Distance", _distance);
            distance.OnValueChanged = (object value) =>
            {
                _distance = (float)value;
            };
            layout.Add(distance);

            layout.Add(new EditorHorizontalLine());

            UITextFieldWidget axis = new UITextFieldWidget("AXIS:", GetText());
            layout.Add(axis);

            EditorHorizontalLayout hlayout1 = new EditorHorizontalLayout();
            layout.Add(hlayout1);
            GUIButton axisX = new GUIButton();
            axisX.Text = "X";
            axisX.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.X);
                this.SetText(axis);
            };
            hlayout1.Add(axisX);

            GUIButton axisY = new GUIButton();
            axisY.Text = "Y";
            axisY.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.Y);
                this.SetText(axis);
            };
            hlayout1.Add(axisY);

            GUIButton axisZ = new GUIButton();
            axisZ.Text = "Z";
            axisZ.TriggerHandler = (Widget w) =>
            {
                this.OrderBy(OrderAxis.Z);
                this.SetText(axis);
            };
            hlayout1.Add(axisZ);

            EditorPrefixLabel alignment = new EditorPrefixLabel();
            alignment.Text = "Alignment:";
            layout.Add(alignment);

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
