using Game.Foundation.Data;
using Game.Foundation.Tool;
using System;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Game.SDK.Project
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 从节点加载对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T LoadFromXmlNode<T>(XmlNode node)
        {
            T t = default(T);
            if (node == null)
            {
                return t;
            }

            Type type = typeof(T);
            object obj = t;
            DXmlReader reader = new DXmlReader(node);
            if (!reader.Read(type.Name, ref obj))
            {
                return t;
            }

            return (T)obj;
        }

        /// <summary>
        /// 从节点加载对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T LoadFromXmlDocument<T>(XmlDocument doc)
        {
            T t = default(T);
            if (doc == null || doc.HasChildNodes == false)
            {
                return t;
            }

            var desc = doc.FirstChild;
            if (desc == null)
            {
                return t;
            }
            var root = desc.NextSibling;
            if (root == null)
            {
                return t;
            }
            return LoadFromXmlNode<T>(root.FirstChild);
        }

        public static void SaveToXmlFile<T>(T target, string filepath)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                return;
            }

            FileUtility.AutoCreateFile(filepath);
            
            XmlDocument doc = new XmlDocument();
            var version = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(version);
            var root = doc.CreateElement("root");
            doc.AppendChild(root);
            DXmlWriter writer = new DXmlWriter(root);
            writer.Write(target.GetType().Name, target);
            doc.Save(filepath);
        }


        /// <summary>
        /// 从resources目录下读取文件，转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T LoadFromXmlResource<T>(string url)
        {
            var asset = Resources.Load<TextAsset>(url);
            var doc = new XmlDocument();
            doc.LoadXml(asset.text);

            return LoadFromXmlDocument<T>(doc);
        }

        /// <summary>
        /// 从外部目录下读取xml文件，转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T LoadFromXmlFile<T>(string fullpath)
        {
            var doc = XmlUtility.LoadFromFile(fullpath);
            return LoadFromXmlDocument<T>(doc);
        }

        /// <summary>
        /// 从xml文本解析，转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T LoadFromXmlText<T>(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return LoadFromXmlDocument<T>(doc);
        }

        /// <summary>
        /// 从字节数组解析，转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T LoadFromXmlBytes<T>(byte[] data)
        {
            var doc = XmlUtility.LoadFromBuff(data);
            return LoadFromXmlDocument<T>(doc);
        }
    }
}
