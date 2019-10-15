using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Editor.EGUI
{
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
}
