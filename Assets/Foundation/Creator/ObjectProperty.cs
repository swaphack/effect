using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Foundation.Creator
{
    [System.Serializable]
    public class ObjectProperty
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string Type;
        /// <summary>
        /// 名字
        /// </summary>
        public string Name;
        public ObjectProperty(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
    }
}
