using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

namespace Game.Foundation.DataAccess
{
    /// <summary>
    /// 内部流加载
    /// </summary>
    public class InnerStream : ResourceLoad
    {
        internal class ResourceItem : IResourceFileItem
        {
            /// <summary>
            /// 字节
            /// </summary>
            public byte[] bytes { get { return _www.bytes; } }
            /// <summary>
            /// 文本
            /// </summary>
            public string text { get { return _www.text; } }
            /// <summary>
            /// 图片
            /// </summary>
            public Texture2D texture2D { get { return _www.texture; } }
            /// <summary>
            /// 音频
            /// </summary>
            public AudioClip audioClip { get { return null; } }
            /// <summary>
            /// 资源路径
            /// </summary>
            public string path { get; }

            private WWW _www;

            public WWW www
            {
                set
                {
                    _www = value;
                }
            }

            public ResourceItem(string path)
            {
                this.path = path;
            }
        }
        public class LoadTask : Task
        {
            private WWW _www;
            public LoadTask(string path, ResourceItemDelegate hand)
            {
                this._item = new ResourceItem(path);
                this._callback = hand;
            }

            public override void Init()
            {
                this._www = new WWW(this._item.path);
                GetItem<ResourceItem>().www = this._www;
            }

            public override IEnumerator Run()
            {
                yield return _www;
            }

            public override void End()
            {
                _www.Dispose();
            }
        }
    }
}
