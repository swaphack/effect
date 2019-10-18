using Assets.Foundation.Data;
using Assets.Foundation.Tool;
using System;
using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.SDK.Project
{
    public class ConfigHelper
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
            System.Object obj = (System.Object)t;
            DXmlReader reader = new DXmlReader(node);
            if (!reader.Read(type.Name, ref obj))
            {
                return t;
            }

            return (T)obj;
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
            var root = doc.FirstChild;
            if (root == null)
            {
                return default(T);
            }
            return LoadFromXmlNode<T>(root.NextSibling);
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
            var root = doc.FirstChild;
            if (root == null)
            {
                return default(T);
            }

            return LoadFromXmlNode<T>(root.NextSibling);
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
            var root = doc.FirstChild;
            if (root == null)
            {
                return default(T);
            }
            return LoadFromXmlNode<T>(root.NextSibling);
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
            var root = doc.FirstChild;
            if (root == null)
            {
                return default(T);
            }

            return LoadFromXmlNode<T>(root.NextSibling);
        }
    }
}
