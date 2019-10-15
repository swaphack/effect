using System;
using System.Collections.Generic;
using System.Collections;

namespace Assets.Foundation.Managers
{
    /// <summary>
    /// 迭代器管理，延迟通知事件
    /// </summary>
    public class EnumeratorManager : Singleton<EnumeratorManager>
    {
        private List<IEnumerator> _values;

        public EnumeratorManager()
        {
            _values = new List<IEnumerator>();
        }

        public bool Empty
        {
            get
            {
                return _values.Count == 0;
            }
        }


        public void Add(IEnumerator value)
        {
            if (value == null)
            {
                return;
            }

            _values.Add(value);
        }

        public IEnumerator Front()
        {
            if (_values.Count == 0)
            {
                return null;
            }
            return _values[0];
        }

        public IEnumerator PopFront()
        {
            if (_values.Count == 0)
            {
                return null;
            }
            var value = _values[0];
            _values.RemoveAt(0);
            return value;
        }

        public void Clear()
        {
            _values.Clear();
        }

        void Update()
        {
            if (Empty)
            {
                return;
            }
            var front = PopFront();
            this.StartCoroutine(front);
        }
    }
}
