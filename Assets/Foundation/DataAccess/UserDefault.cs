using Assets.Foundation.Managers;
using Assets.Foundation.Protocol;
using Assets.Foundation.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 用户默认文件
    /// </summary>
    public class UserDefault : Singleton<UserDefault>
    {
        /// <summary>
        /// 文件名
        /// </summary>
        private const string FileName = "UserDefault.xml";

        private Dictionary<string, object> _values;

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FullPath 
        {
            get
            {
                return Path.Combine(FilePath.PersistentDataPath, FileName);
            }
 
        }

        private UserDefault()
        {
            _values = new Dictionary<string, object>();
            this.Load();
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            _values[key] = value;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            if (_values.ContainsKey(key))
            {
                return _values[key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var obj = this.Get(key);
            if (obj == null)
            {
                return default(T);
            }

            if (obj is T)
            {
                return (T)obj;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 保存到本地文件
        /// </summary>
        public void Save()
        {
            FileUtility.AutoCreateFile(FullPath);

            XmlDocument doc = new XmlDocument();
            var root = doc.CreateElement("root");
            doc.AppendChild(root);

            foreach (var item in _values)
            {
                var node = doc.CreateElement("Item");
                XmlUtility.WriteNodeAttributeWithObject(node, item);
                root.AppendChild(node);
            }
            doc.Save(FullPath);
        }

        public void Clear()
        {
            _values.Clear();
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            _values.Clear();
            if (!File.Exists(FullPath))
            {
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(FullPath);
            var root = doc.FirstChild;
            if (root == null)
            {
                return;
            }

            var node = root.FirstChild;
            while (node != null)
            {
                var item = XmlUtility.ReadObjectByNodeAttribute(node);
                if (item.Key != null)
                {
                    if (this._values.ContainsKey(item.Key))
                    {
                        Debug.LogErrorFormat("UserDefault has same key : {0}", item.Key);
                    }
                    this._values.Add(item.Key, item.Value);
                }

                node = node.NextSibling;
            }
        }
    }
}
