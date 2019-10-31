using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using Assets.Foundation.Extensions;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 内部资源加载
    /// </summary>
    public class InnerResource : ResourceLoad
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
            public string path { get { return _path; } }

            private string _path;
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
                _path = path;
            }
        }
        public class LoadTask : Task
        {
            private ResourceRequest _request;
            public LoadTask(string path, ResourceItemDelegate hand)
            {
                this._item = new ResourceItem(path);
                this._callback = hand;
                this._request = null;
            }

            public override void Init()
            {
            }

            public override IEnumerator Run()
            {
                if (_request == null)
                {
                    _request = Resources.LoadAsync(Item.path);
                    yield return _request;
                }
            }

            public override void DoCallback()
            {
                ((ResourceItem)this._item).Object  = _request.asset;

                base.DoCallback();
            }

            public override void End()
            {
            }
        }
    }
}
