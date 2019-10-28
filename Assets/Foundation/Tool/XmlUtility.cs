using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using Assets.Foundation.Protocol;
using Assets.Foundation.Data;

namespace Assets.Foundation.Tool
{
    /// <summary>
    /// xml工具
    /// </summary>
    public class XmlUtility
    {
        public static XmlDocument LoadFromStream(Stream stream)
        {
            if (stream == null)
            {
                return null;
            }
            var doc = new XmlDocument();
            doc.Load(stream);
            return doc;
        }

        public static XmlDocument LoadFromBuff(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return null;
            }
            var stream = new MemoryStream(data);
            {
                var reader = XmlReader.Create(stream);

                var doc = new XmlDocument();
                doc.Load(reader);
                stream.Dispose();
                return doc;
            }
        }

        public static XmlDocument LoadFromFile(string filepath)
        {
            if (filepath == null || filepath.Length == 0)
            {
                return null;
            }

            if (!File.Exists(filepath))
            {
                return null;
            }

            var doc = new XmlDocument();
            doc.Load(filepath);
            return doc;
        }


        /// <summary>
        /// 使用 PacketReader解析xmlattribute
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static KeyValuePair<string, Object> ReadObjectByNodeAttribute(XmlNode node)
        {
            KeyValuePair<string, Object> value = new KeyValuePair<string,object>();
            if (node == null)
            {
                return value;
            }

            var attrs = node.Attributes;
            if (attrs.Count == 3)
            {
                string key = attrs["name"].Value;
                string strObj = attrs["value"].Value;
                string type = attrs["type"].Value;

                var reader = new PacketReader(Convert.FromBase64String(strObj));
                var t = Type.GetType(type);
                if (t != null)
                {
                    var obj = DataHelper.Create(t);
                    if (obj != null)
                    {
                        obj = reader.ReadObject(obj);
                        if (obj != null)
                        {
                            value = new KeyValuePair<string, object>(key, obj);
                        }
                    }
                    
                }
                
            }

            return value;
        }

        /// <summary>
        /// 写入对象到节点的属性中
        /// </summary>
        /// <param name="node"></param>
        /// <param name="item"></param>
        public static void WriteNodeAttributeWithObject(XmlElement node, KeyValuePair<string, Object> item)
        {
            if (node == null)
            {
                return;
            }
            var writer = new PacketWriter();
            writer.WriteObject(item.Value);
            var bytes = writer.ToBytes();
            var value = Convert.ToBase64String(bytes, Base64FormattingOptions.None);

            node.SetAttribute("name", item.Key);
            node.SetAttribute("value", value);
            node.SetAttribute("type", item.Value.GetType().ToString());
        }
    }
}
