using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Foundation.Creator
{
    /// <summary>
    /// 对象模板
    /// </summary>
    public class ObjectBehaviour : MonoBehaviour
    {
        public bool Dirty;

        public string Name;

        public int Length;

        public float dd;

        public Dictionary<string, string> Properties;

        public ObjectBehaviour()
        {
            Properties = new Dictionary<string, string>();
        }

        public void Add(string name, string type)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type))
            {
                return;
            }

            Properties.Add(name, type);
        }

        public void Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            Properties.Remove(name);
        }

        public void Clean()
        {
            Properties.Clear();
        }
    }
}
