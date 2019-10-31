using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Widgets
{
    
    public abstract class EditorLayout : Layout
    {
    }

    public class EFadeGroup : EditorLayout
    {
        public float Value { get; set; }

        protected override void BeginDraw()
        {
            bool value = EditorGUILayout.BeginFadeGroup(Value);
            if (value)
            {
                this.DipatchEvent();
            }
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndFadeGroup();
        }
    }

    public class EditorScrollView : EditorLayout
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Vector2 _scrollPosition;

        private Vector2 ScrollPosition
        {
            get { return _scrollPosition; }
            set { _scrollPosition = value; }
        }
        private bool AlwaysShowHorizontal { get; set; }
        private bool AlwaysShowVertical { get; set; }

        protected override void BeginDraw()
        {
            Vector2 pos = EditorGUILayout.BeginScrollView(ScrollPosition, AlwaysShowHorizontal, AlwaysShowVertical,Option.Values);
            if (pos != ScrollPosition)
            {
                ScrollPosition = pos;
                this.DipatchEvent();
            }
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndScrollView();
        }
    }

    /// <summary>
    /// 水平布局
    /// </summary>
    public class EditorHorizontalLayout : EditorLayout
    {
        public EditorHorizontalLayout()
        {
        }

        protected override void BeginDraw()
        {
            EditorGUILayout.BeginHorizontal(Option.Values);
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 垂直布局
    /// </summary>
    public class EditorVerticalLayout : EditorLayout
    {
        public EditorVerticalLayout()
        {
        }

        protected override void BeginDraw()
        {
            EditorGUILayout.BeginVertical(Option.Values);
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndVertical();
        }
    }

    public class EditorSeparator : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Separator();
        }
    }

    public class EditorSpace : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Space();
        }
    }

    public class EditorSlider : Widget
    {

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue { get; set; } = 100;

        protected override void OnDraw()
        {
            float value = EditorGUILayout.Slider(Content, Value, MinValue, MaxValue, Option.Values);
            if (!Mathf.Approximately(value, Value))
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorTagField : Widget
    {
        protected override void OnDraw()
        {
            string value = EditorGUILayout.TagField(Content, Text,Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }


    public class EditorTextArea : GUIText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.textArea;
        }

        protected override void OnDraw()
        {
            string value = EditorGUILayout.TextArea(Text, Style, Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorTextField : GUIText
    {
        protected override void InitStyle()
        {
            Style = GUI.skin.textField;
        }
        protected override void OnDraw()
        {
            string value = EditorGUILayout.TextField(Text, Style, Option.Values);
            if (value != Text)
            {
                Text = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorToggle : Widget
    {
        public bool Value { get; set; }

        protected override void OnDraw()
        {
            bool value = EditorGUILayout.Toggle(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorToggleGroup : Widget
    {
        private List<EditorToggle> _toggles;

        public bool Value { get; set; }

        public int Count 
        {
            get 
            {
                return _toggles.Count;
            }
        }

        public bool this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                {
                    return false;
                }
                return _toggles[index].Value;
            }
            set
            {
                if (index >= Count || index < 0)
                {
                    return;
                }
                _toggles[index].Value = value;
            }
        }

        public EditorToggleGroup()
        {
            _toggles = new List<EditorToggle>();
        }

        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="toggle"></param>
        public void Add(EditorToggle toggle)
        {
            if (toggle == null)
            {
                return;
            }

            if (_toggles.Contains(toggle))
            {
                return;
            }

            _toggles.Add(toggle);
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        /// <param name="toggle"></param>
        public void Remove(EditorToggle toggle)
        {
            if (toggle == null)
            {
                return;
            }

            if (!_toggles.Contains(toggle))
            {
                return;
            }

            _toggles.Remove(toggle);
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            _toggles.Clear();
        }

        protected override void BeginDraw()
        {
            bool value = EditorGUILayout.BeginToggleGroup(Content, Value);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }

        protected override void OnDraw()
        {
            foreach (var item in _toggles)
            {
                item.Draw();
            }
        }

        protected override void EndDraw()
        {
            EditorGUILayout.EndToggleGroup();
        }
    }

    public class EditorToggleLeft : EditorToggle
    {
        protected override void OnDraw()
        {
            bool value = EditorGUILayout.ToggleLeft(Content, Value,Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorVector2Field : Widget
    {
        private Vector2 _value;

        public Vector2 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector2 value = EditorGUILayout.Vector2Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class EditorVector3Field : Widget
    {
        private Vector3 _value;

        public Vector3 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector3 value = EditorGUILayout.Vector3Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }


    public class EditorVector4Field : Widget
    {
        private Vector4 _value;

        public Vector4 Value
        {
            get { return _value; }

            set { _value = value; }
        }

        protected override void OnDraw()
        {
            Vector4 value = EditorGUILayout.Vector4Field(Content, Value, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    

}
