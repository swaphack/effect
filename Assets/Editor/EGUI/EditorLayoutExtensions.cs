using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.EGUI
{
    public abstract class ELayout : Layout
    {
    }

    public class EFadeGroup : ELayout
    {
        private float _value;

        public float Value
        {
            get { return _value; }

            set { _value = value; }
        }

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

    public class EScrollView : ELayout
    {
        /// <summary>
        /// 位置
        /// </summary>
        private Vector2 _scrollPosition;
        /// <summary>
        /// 是否总显示水平滑条
        /// </summary>
        private bool _alwaysShowHorizontal;
        /// <summary>
        /// 是否总显示垂直滑条
        /// </summary>
        private bool _alwaysShowVertical;

        private Vector2 ScrollPosition
        {
            get { return _scrollPosition; }
            set { _scrollPosition = value; }
        }
        private bool AlwaysShowHorizontal
        {
            get { return _alwaysShowHorizontal; }
            set { _alwaysShowHorizontal = value; }
        }
        private bool AlwaysShowVertical
        {
            get { return _alwaysShowVertical; }
            set { _alwaysShowVertical = value; }
        }

        public EScrollView()
        {
        }

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
    public class EHorizontalLayout : ELayout
    {
        public EHorizontalLayout()
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
    public class EVerticalLayout : ELayout
    {
        public EVerticalLayout()
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

    public class ESeparator : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Separator();
        }
    }

    public class ESpace : Widget
    {
        protected override void OnDraw()
        {
            EditorGUILayout.Space();
        }
    }

    public class ESlider : Widget
    {
        /// <summary>
        /// 当前值
        /// </summary>
        private float _value;
        /// <summary>
        /// 最小值
        /// </summary>
        private float _minValue;
        /// <summary>
        /// 最大值
        /// </summary>
        private float _maxValue = 100;

        /// <summary>
        /// 当前值
        /// </summary>
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        protected override void OnDraw()
        {
            float value = EditorGUILayout.Slider(Content, Value, MinValue, MaxValue, Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }

    public class ETagField : Widget
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


    public class ETextArea : BText
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

    public class ETextField : BText
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

    public class EToggle : Widget
    {
        private bool _value;

        public bool Value
        {
            get { return _value; }

            set { _value = value; }
        }

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

    public class EToggleGroup : Widget
    {
        private bool _value;
        private List<EToggle> _toggles;

        public bool Value
        {
            get { return _value; }

            set { _value = value; }
        }

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

        public EToggleGroup()
        {
            _toggles = new List<EToggle>();
        }

        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="widget"></param>
        public void Add(EToggle toggle)
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
        public void Remove(EToggle toggle)
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

    public class EToggleLeft : EToggle
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

    public class EVector2Field : Widget
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

    public class EVector3Field : Widget
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


    public class EVector4Field : Widget
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
