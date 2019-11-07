using UnityEngine;

namespace Game.Foundation.Actions
{
    /// <summary>
    /// 动作接口
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// 作用对象
        /// </summary>
        Component Target { get; }
        /// <summary>
        /// 是否正在播放
        /// </summary>
        bool IsPlaying { get; }
        /// <summary>
        /// 是否完成
        /// </summary>
        bool IsDone { get; }
        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();
        /// <summary>
        /// 恢复
        /// </summary>
        void Resume();
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="dt"></param>
        void Update(float dt);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="target"></param>
        void InitWithTarget(Component target);
        /// <summary>
        /// 重置
        /// </summary>
        void Reset();
    }
}
