using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Networking;
using Assets.Foundation.Managers;

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
            /// http下载
            /// </summary>
            private UnityWebRequest _www;
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
            private bool _init;
            /// <summary>
            /// 请求地址
            /// </summary>
            private string _url;
            /// <summary>
            /// 本地保存地址
            /// </summary>
            private string _local;

            public string url
            {
                get 
                {
                    return _url;
                }
            }
            public string local
            {
                get
                {
                    return _local;
                }
            }


            public UnityWebRequest WWW
            {
                get
                {
                    return _www;
                }
            }

            public bool IsDone
            {
                get
                {
                    if (_www == null)
                    {
                        return false;
                    }

                    if (_www.isNetworkError)
                    {
                        return true;
                    }
                    if (!_www.isDone)
                    {
                        return false;
                    }
                    if (this._position != _www.downloadHandler.data.Length)
                    {
                        return false;
                    }

                    return true;
                }
            }
            

            public Task(string url, string local, Callback callback)
            {
                this._url = url;
                this._local = local;
                this._callback = callback;
                this._www = UnityWebRequest.Get(url);
                this._localFile = new StorageFile(local);
                this._localFile.SeekEnd();

                this._init = false;
                this._position = 0;
                this._www.SetRequestHeader("Range", string.Format("bytes={0}-", this._localFile.Length));
            }

            public void InitTask()
            {
                if (_init == false)
                {
                    _init = true;
                }
                if (_www != null)
                {
                    _www.Send();
                }
            }

            public void ProgressTask()
            {
                long offset = _position;
                long length = _www.downloadHandler.data.Length;
                long count = length - offset;
                _position = length;
                this._localFile.Append(_www.downloadHandler.data, offset, count);
                if (_callback != null)
                {
                    _callback(_www.error, url, _www.downloadProgress);
                }
            }

            public void Dispose()
            {
                if (_www != null)
                {
                    _www.Dispose();
                    _www = null;
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
