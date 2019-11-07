
using Game.Editor.Widgets;
using Game.Foundation.UI;
using System;
using UnityEditor;
using UnityEngine.UI;

namespace Game.Editor.Inspectors
{

    public class UIListViewBaseInspector : UIInspector
    {
        private UIFloatFieldWidget elasticity;
        private void UpdateElasticity(Layout layout, Widget w)
        {
            UIListView listView = GetTarget<UIListView>();

            if (listView.movementType == ScrollRect.MovementType.Elastic)
            {
                if (elasticity == null)
                {
                    elasticity = new UIFloatFieldWidget("  Elasticity", listView.elasticity);
                    elasticity.OnValueChanged = (Object value) =>
                    {
                        listView.elasticity = (float)value;
                    };
                }
                layout.InsertAfter(w, elasticity);
            }
            else
            {
                layout.Remove(elasticity);
            }
        }

        private UIFloatFieldWidget decelerationRate;
        private void UpdateDecelerationRate(Layout layout, Widget w)
        {
            UIListView listView = GetTarget<UIListView>();

            if (listView.inertia)
            {
                if (decelerationRate == null)
                {
                    decelerationRate = new UIFloatFieldWidget("  DecelerationRate", listView.decelerationRate);
                    decelerationRate.OnValueChanged = (Object value) =>
                    {
                        listView.decelerationRate = (float)value;
                    };
                }
                layout.InsertAfter(w, decelerationRate);
            }
            else
            {
                layout.Remove(decelerationRate);
            }
        }

        protected override void InitUI(UIWidget layout)
        {
            UIListView listView = GetTarget<UIListView>();

            UIEnumFieldWidget movementType = new UIEnumFieldWidget("MovementType", listView.movementType);
            movementType.OnValueChanged = (Object value) =>
            {
                listView.movementType = (ScrollRect.MovementType)value;
                UpdateElasticity(layout, movementType);
            };
            layout.Add(movementType);
            UpdateElasticity(layout, movementType);

            ////////////////////////////////////////////////////////////
            UIBooleanFieldWidget inertia = new UIBooleanFieldWidget("Inertia", listView.inertia);
            inertia.OnValueChanged = (Object value) =>
            {
                listView.inertia = (bool)value;
                UpdateDecelerationRate(layout, inertia);
            };
            layout.Add(inertia);
            UpdateDecelerationRate(layout, inertia);
            
            ////////////////////////////////////////////////////////////
            UIFloatFieldWidget scrollSensitivity = new UIFloatFieldWidget("ScrollSensitivity", listView.scrollSensitivity);
            scrollSensitivity.OnValueChanged = (Object value) =>
            {
                listView.scrollSensitivity = (float)value;
            };
            layout.Add(scrollSensitivity);

            ////////////////////////////////////////////////////////////
            UIEnumFieldWidget direction = new UIEnumFieldWidget("Direction", listView.Direction);
            direction.OnValueChanged = (Object value) =>
            {
                listView.Direction = (ScrollDirection)value;
            };
            layout.Add(direction);

            GUIButton btn = new GUIButton();
            btn.Text = "Format";
            btn.TriggerHandler = (Widget w) =>
            {
                listView.FormatScrollView();
            };
            layout.Add(btn);
        }
    }

    [CustomEditor(typeof(UIListView))]
    class UIListViewInspector : UIListViewBaseInspector
    { 
    }

    [CustomEditor(typeof(UIScrollText))]
    class UIScrollTextInspector : UIListViewBaseInspector
    {
    }
}
