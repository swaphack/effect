using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Assets.Editor.EGUI
{
    public class EIntPopup : Widget
    {
        public class DisplayItem
        {
            public GUIContent item;
            public int value;

            public DisplayItem(GUIContent item, int value)
            {
                this.item = item;
                this.value = value;
            }

            public DisplayItem(string text, int value)
                :this(new GUIContent(text), value)
            {
            }
        }

        /// <summary>
        /// 当前值
        /// </summary>
        private int _value;

        private List<DisplayItem> _displayItems;
        /// <summary>
        /// 当前值
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public EIntPopup()
        {
            _displayItems = new List<DisplayItem>();
        }

        public void AddRange(string[] textAry, int[] valueAry)
        {
            if (textAry == null || textAry.Length == 0
                || valueAry == null || valueAry.Length == 0)
            {
                return;
            }

            if (textAry.Length > valueAry.Length)
            {
                Debug.LogError("Not Match Length With Input Text And Value");
                return;
            }

            for (var i = 0; i < textAry.Length; i++)
            {
                this.Add(textAry[i], valueAry[i]);
            }
        }

        public void Add(string text, int value)
        {
            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            this.Add(new GUIContent(text), value);
        }

        public void Add(GUIContent content, int value)
        {
            if (content == null)
            {
                return;
            }

            this.Add(new DisplayItem(content, value));
        }

        public void Add(DisplayItem item)
        {
            if (item == null)
            {
                return;
            }

            _displayItems.Add(item);
        }

        public void Remove(DisplayItem item)
        {
            if (item == null)
            {
                return;
            }

            _displayItems.Remove(item);
        }

        public void Clear()
        {
            _displayItems.Clear();
        }

        protected override void OnDraw()
        {
            List<GUIContent> displayedOptions = new List<GUIContent>();
            List<int> optionValues = new List<int>();

            foreach(var item in _displayItems)
            {
                displayedOptions.Add(item.item);
                optionValues.Add(item.value);
            }

            int value = EditorGUILayout.IntPopup(Content, Value, displayedOptions.ToArray(), optionValues.ToArray(), Option.Values);
            if (value != Value)
            {
                Value = value;
                this.DipatchEvent();
            }
        }
    }
}
