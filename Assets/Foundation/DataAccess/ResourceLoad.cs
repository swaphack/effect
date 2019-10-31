using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Foundation.DataAccess
{
    /// <summary>
    /// 资源加载
    /// </summary>
    public abstract class ResourceLoad : MonoBehaviour
    {
        /// <summary>
        /// 资源加载事件处理
        /// </summary>
        /// <param name="item"></param>
        public delegate void ResourceItemDelegate(IFileItem item);
        /// <summary>
        /// 任务
        /// </summary>
        public abstract class Task
        {
            /// <summary>
            /// 资源加载回调
            /// </summary>
            protected ResourceItemDelegate _callback;
            /// <summary>
            /// 资源项
            /// </summary>
            protected IFileItem _item;

            /// <summary>
            /// 资源加载回调
            /// </summary>
            public ResourceItemDelegate Callback
            {
                get
                {
                    return _callback;
                }
            }

            /// <summary>
            /// 资源项
            /// </summary>
            public IFileItem Item
            {
                get
                {
                    return _item;
                }
            }

            /// <summary>
            /// 资源项
            /// </summary>
            public T GetItem<T>() where T : IFileItem
            {
                return (T)_item;
            }

            /// <summary>
            /// 初始化
            /// </summary>
            public abstract void Init();
            /// <summary>
            /// 运行
            /// </summary>
            /// <returns></returns>
            public abstract IEnumerator Run();
            /// <summary>
            /// 执行回调
            /// </summary>
            public virtual void DoCallback()
            {
                _callback?.Invoke(_item);
            }
            /// <summary>
            /// 结束
            /// </summary>
            public abstract void End();
        }

        /// <summary>
        /// 任务
        /// </summary>
        private List<Task> _tasks = new List<Task>();
        /// <summary>
        /// 是否开始任务
        /// </summary>
        private bool _bStartTask;

        void StartTask()
        {
            if (_bStartTask == true)
            {
                return;
            }
            _bStartTask = true;
            this.StartCoroutine(LoadRes());
        }

        void StopTask()
        {
            this.StopCoroutine(LoadRes());
            _bStartTask = false;
        }

        IEnumerator LoadRes()
        {
            var task = _tasks[0];
            task.Init();
            yield return task.Run();
            task.DoCallback();
            task.End();
            _tasks.RemoveAt(0);
            if (_tasks.Count == 0)
            {
                this.StopTask();
                yield return null;
            }
            else
            {
                yield return LoadRes();
            }
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
            this.StartTask();
        }
    }
}
