using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.SDK.Project
{
    /// <summary>
    /// 状态
    /// </summary>
    public enum WorkState
    {
        None,
        Start,
        Update,
        End,
    }

    public delegate void FinishWorkFunc(IWorkSlot slot);

    public interface IWorkSlot
    {
        /// <summary>
        /// 数据
        /// </summary>
        Object Data { get; }
        /// <summary>
        /// 状态
        /// </summary>
        WorkState State { get; }
        /// <summary>
        /// 初始化
        /// </summary>
        IEnumerator Init(Object data);
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <returns></returns>
        IEnumerator DoEvent();
        /// <summary>
        /// 结束
        /// </summary>
        IEnumerator Finish();
    }

    public abstract class WorkSlot : IWorkSlot
    {
        /// <summary>
        /// 状态
        /// </summary>
        private WorkState _state;
        /// <summary>
        /// 数据
        /// </summary>
        private Object _data;
        /// <summary>
        /// 状态
        /// </summary>
        public WorkState State 
        {
            get
            {
                return _state;
            }
            protected set
            {
                _state = value;
            }
        }
       
        /// <summary>
        /// 数据
        /// </summary>
        public Object Data
        {
            get
            {
                return _data;
            }
            protected set
            {
                _data = value;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract IEnumerator Init(Object data);
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator DoEvent();
        /// <summary>
        /// 结束
        /// </summary>
        public abstract IEnumerator Finish();
    }

    /// <summary>
    /// 工作流
    /// </summary>
    public class Workflow
    {
        /// <summary>
        /// 事件
        /// </summary>
        private List<IWorkSlot> _events;
        /// <summary>
        /// 数据
        /// </summary>
        private Object _data;

        public Workflow()
        {
            _events = new List<IWorkSlot>();
        }

        public void AddWork(IWorkSlot e)
        {
            if (e == null)
            {
                return;
            }
            _events.Add(e);
        }

        public IEnumerator UpdateWorkflow()
        {
            if (_events.Count == 0)
            {
                yield return null;
            }

            var e = _events[0];
            switch (e.State)
            {
                case WorkState.None: 
                    {
                        yield return e.Init(_data);
                        break;
                    }
                case WorkState.Start:
                    {
                        yield return e.DoEvent();
                        break;
                    }
                case WorkState.Update:
                    {
                        break;
                    }
                case WorkState.End:
                    {
                        yield return e.Finish();
                        _data = e.Data;
                        break;
                    }
                default:
                    yield return null;
                    break;
            }
        }
    }
}
