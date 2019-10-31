using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using Assets.Foundation.Common;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 下载
    /// </summary>
    public sealed class DownloadManager : SingletonBehaviour<DownloadManager>
    {
        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="error"></param>
        /// <param name="url"></param>
        /// <param name="progress"></param>
        public delegate void Callback(string error, string url, float progress);
        /// <summary>
        /// 任务
        /// </summary>
        internal struct Task
        {
            /// <summary>
            /// 回调
            /// </summary>
            private Callback _callback;

            /// <summary>
            /// 本地文件
            /// </summary>
            private StorageFile _localFile;
            /// <summary>
            /// 本地文件缓存位置
            /// </summary>
            private long _position;
            /// <summary>
            /// 是否初始化
            /// </summary>
            private bool _bInit;

            public string url { get; }
            public string local { get; }

            /// <summary>
            /// 下载是否完成
            /// </summary>
            private bool _bFinish;


            public UnityWebRequest WWW { get; private set; }

            public bool IsDone
            {
                get
                {
                    if (WWW == null)
                    {
                        return false;
                    }
                    if (WWW.isNetworkError)
                    {
                        return true;
                    }
                    if (WWW.isHttpError)
                    {
                        return true;
                    }
                    if (!WWW.isDone)
                    {
                        return false;
                    }
                    if (this._position != WWW.downloadHandler.data.Length)
                    {
                        return false;
                    }

                    return true;
                }
            }
            

            public Task(string url, string local, Callback callback)
            {
                this.url = url;
                this.local = local;
                this._callback = callback;
                this.WWW = UnityWebRequest.Get(url);
                this._localFile = new StorageFile(local);
                this._localFile.SeekEnd();

                this._bInit = false;
                this._bFinish = false;
                this._position = 0;
                this.WWW.SetRequestHeader("Range", string.Format("bytes={0}-", this._localFile.Length));
            }

            public void InitTask()
            {
                if (_bInit == false)
                {
                    _bInit = true;
                }
                if (WWW != null)
                {
                    WWW.SendWebRequest();
                }
            }

            public void ProgressTask()
            {
                if (_bFinish)
                {
                    return;
                }
                long offset = _position;
                long length = WWW.downloadHandler.data.Length;
                long count = length - offset;
                _position = length;
                this._localFile.Append(WWW.downloadHandler.data, offset, count);
                this._localFile.Save();

                _callback?.Invoke(WWW.error, url, WWW.downloadProgress);

                if (WWW.downloadProgress >= 1)
                {
                    this.Dispose();
                    _bFinish = true;
                }
            }

            public void Dispose()
            {
                if (WWW != null)
                {
                    WWW.Dispose();
                    WWW = null;
                }
                if (_localFile != null)
                {
                    _localFile.Dispose();
                    _localFile = null;
                }
                _callback = null;
            }
        }

        /// <summary>
        /// 下载任务
        /// </summary>
        private List<Task> _tasks;
        private bool _bStartTask;

        private DownloadManager()
        {
            _tasks = new List<Task>();
        }

        public void AddTask(string url, string local, Callback callback)
        {
            _tasks.Add(new Task(url, local, callback));
            this.StartTask();
        }

        void StartTask()
        {
            if (_bStartTask == true)
            {
                return;
            }
            _bStartTask = true;
            this.StartCoroutine("download");
        }

        void StopTask()
        {
            this.StopCoroutine("download");
            _bStartTask = false;
        }

        IEnumerator download()
        {
            var task = _tasks[0];
            task.InitTask();
            while (!task.IsDone)
            {
                task.ProgressTask();
                yield return null;
            }
            if (task.IsDone)
            {
                task.Dispose();
                _tasks.RemoveAt(0);
                if (_tasks.Count == 0)
                {
                    this.StopTask();
                    yield return null;
                }
                else
                {
                    yield return download();
                }
            }
        }
    }
}
