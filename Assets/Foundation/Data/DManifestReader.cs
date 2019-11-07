using Game.Foundation.TextFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game.Foundation.Data
{
    /// <summary>
    /// u3d AssetBundleManifest配置
    /// </summary>
    public class DManifestReader
    {
        public class Node 
        {
            public string Key { get; set; }
            public string Value { get; set; }
            public Dictionary<string, Node> Map { get; private set; }

            public Node()
            {
                Map = new Dictionary<string, Node>();
            }

            public Node(string key)
            {
                Key = key;
                Map = new Dictionary<string, Node>();
            }

            public Node(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public Node(string key, Dictionary<string, Node> value)
            {
                if (string.IsNullOrEmpty(key))
                {
                    return;
                }
                Key = key;
                if (value == null)
                    Map = new Dictionary<string,Node>();
                else
                    Map = value;
            }

            public void AddChild(string key, string value)
            {
                Map.Add(key, new Node(key, value));
            }

            public void AddChild(string key, Node value)
            {
                Map.Add(key, value);
            }

            public void AddChild(Node value)
            {
                Map.Add(value.Key, value);
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        public Node Root { get; private set; }

        /// <summary>
        /// 解析文本
        /// </summary>
        /// <param name="text"></param>
        public void Read(string text)
        {
            ManifestFormat format = new ManifestFormat();
            format.Read(text);

            Root = new Node();
            this.Parse(format.Root, Root);
        }

        /// <summary>
        /// 解析行
        /// </summary>
        /// <param name="lineNode"></param>
        /// <param name="node"></param>
        private void Parse(ManifestFormat.TextLine lineNode, Node node)
        {
            if (lineNode == null || node == null)
            {
                return;
            }

            string line = lineNode.Text;

            if (!string.IsNullOrEmpty(line))
            { // 文本
                string[] param = line.Split(':');
                if (param == null || param.Length != 2)
                {
                    return;
                }
                node.Key = param[0].Trim();
                string value = param[1].Trim();
                if (!string.IsNullOrEmpty(value))
                {
                    node.Value = value;
                }
            }

            if (lineNode.Children.Count != 0)
            {// 子节点
                for (int i = 0; i < lineNode.Children.Count; i++)
                {
                    var childLine = lineNode.Children[i];
                    var childNode = new Node();
                    this.Parse(childLine, childNode);
                    node.AddChild(childNode);
                }
            }
        }
    }
}
