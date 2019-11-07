using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.SDK.Project
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
        Finish,
    }

    public delegate void FinishWorkFunc(IWorkSlot slot);

    public interface IWorkSlot
    {
        /// <summary>
        /// 数据
        /// </summary>
        object Data { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        WorkState State { get; }
        /// <summary>
        /// 所属工作流
        /// </summary>
        Workflow Group{ get; set; }
        /// <summary>
        /// 下一步
        /// </summary>
        void MoveNext();
        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <returns></returns>
        void DoEvent();
        /// <summary>
        /// 结束
        /// </summary>
        void Finish();
    }

    public abstract class WorkSlot : IWorkSlot
    {
        /// <summary>
        /// 状态
        /// </summary>
        private WorkState _state;
        /// <summary>
        /// 状态
        /// </summary>
        private Workflow _group;
        /// <summary>
        /// 数据
        /// </summary>
        private object _data;
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
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public Workflow Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();
        /// <summary>
        /// 执行事件
        /// </summary>
        /// <returns></returns>
        public abstract void DoEvent();
        /// <summary>
        /// 结束
        /// </summary>
        public abstract void Finish();

        public void MoveTo(WorkState state)
        {
            State = state;
            this.MoveNext();
        }

        public void MoveNext()
        {
            switch (State)
            {
                case WorkState.None:
                    State = WorkState.Start;
                    this.Init();
                    break;
                case WorkState.Start:
                    State = WorkState.Update;
                    this.DoEvent();
                    break;
                case WorkState.Update:
                    State = WorkState.End;
                    this.Finish();
                    break;
                case WorkState.End:
                    State = WorkState.Finish;
                    if (_group != null)
                    {
                        _group.RemoveWorkSlot(this);
                        _group.MoveNext(Data);
                    }
                    break;
            }
        }
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

        public Workflow()
        {
            _events = new List<IWorkSlot>();
        }

        public void AddWorkSlot(IWorkSlot e)
        {
            if (e == null)
            {
                return;
            }
            _events.Add(e);
            e.Group = this;
        }

        public void RemoveWorkSlot(IWorkSlot e)
        {
            if (e == null)
            {
                return;
            }

            _events.Remove(e);
        }

        public void MoveNext(object data)
        {
            if (_events.Count == 0)
            {
                return;
            }

            var e = _events[0];
            e.Data = data;
            e.MoveNext();
        }   
    }
}
