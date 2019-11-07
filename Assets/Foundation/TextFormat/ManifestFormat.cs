using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Game.Foundation.TextFormat
{
    /// <summary>
    /// u3d清单格式
    /// </summary>
    public class ManifestFormat
    {
        public class TextLine
        {
            /// <summary>
            /// 文本内容
            /// </summary>
            public string Text;
            /// <summary>
            /// 左边空格个数
            /// </summary>
            public int LeftEmptyCount;
            /// <summary>
            /// 父节点
            /// </summary>
            public TextLine parent;
            /// <summary>
            /// 子节点
            /// </summary>
            public List<TextLine> Children;

            public TextLine(string text)
            {
                Text = text;
                Children = new List<TextLine>();
            }

            public TextLine()
            {
                Children = new List<TextLine>();
            }

            public void AddChild(string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                var node = new TextLine(text);
                node.LeftEmptyCount = GetLeftEmptyCount(text);
                node.parent = this;
                Children.Add(node);
            }

            public TextLine LastChild
            {
                get
                {
                    if (Children.Count == 0)
                    {
                        return null;
                    }
                    return Children[Children.Count - 1];
                }
            }
        }

        /// <summary>
        /// 根节点
        /// </summary>
        public TextLine Root { get; private set; }

        public ManifestFormat()
        {
            Root = new TextLine();
        }

        /// <summary>
        /// 获取左边空格数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int GetLeftEmptyCount(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ')
                {
                    return i;
                }
            }
            return 0;
        }


        public void Read(string text)
        {
            Root = new TextLine();

            var tempNode = Root;

            using (TextReader reader = new StringReader(text))
            {
                while (reader.Peek() != -1)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    do
                    {
                        TextLine lastChild = tempNode.LastChild;
                        if (lastChild == null)
                        {
                            tempNode.AddChild(line);
                            break;
                        } 
                        int curEmptyIndex = GetLeftEmptyCount(line);
                        if (curEmptyIndex == lastChild.LeftEmptyCount)
                        {// 同级
                            tempNode.AddChild(line);
                            break;
                        }
                        if (curEmptyIndex > lastChild.LeftEmptyCount)
                        { // 子级 
                            lastChild.AddChild(line);
                            tempNode = lastChild;
                            break;
                        }
                        else
                        {// 父级
                            tempNode = tempNode.parent;
                        }
                    } while (true);
                }
                reader.Close();
            }
        }
    }
}
