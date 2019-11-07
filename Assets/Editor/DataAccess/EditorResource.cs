using UnityEngine;
using UnityEditor;
using System.Collections;
using Game.Foundation.DataAccess;

namespace Game.Editor.DataAccess
{
    public class EditorResource : ResourceLoad
    {
        internal class ResourceItem : IResourceFileItem
        {
            /// <summary>
            /// 字节
            /// </summary>
            public byte[] bytes { get { return (_object as TextAsset).bytes; } }
            /// <summary>
            /// 文本
            /// </summary>
            public string text { get { return (_object as TextAsset).text; } }
            /// <summary>
            /// 图片
            /// </summary>
            public Texture2D texture2D { get { return _object as Texture2D; } }
            /// <summary>
            /// 音频
            /// </summary>
            public AudioClip audioClip { get { return _object as AudioClip; } }
            /// <summary>
            /// 资源路径
            /// </summary>
            public string path { get; }

            private object _object;

            public object Object
            {
                set
                {
                    _object = value;
                }
            }

            public ResourceItem(string path)
            {
                this.path = path;
            }
        }
        public class LoadTask : Task
        {
            public LoadTask(string path, ResourceLoad.ResourceItemDelegate hand)
            {
                this._item = new ResourceItem(path);
                this._callback = hand;
            }

            public override void Init()
            {
            }

            public override IEnumerator Run()
            {
                yield return null;
            }

            public override void DoCallback()
            {
                this.GetItem<ResourceItem>().Object = AssetDatabase.LoadAssetAtPath<Object>(this._item.path);

                base.DoCallback();
            }

            public override void End()
            {
            }
        }
    }
}
