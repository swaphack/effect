using UnityEngine;

namespace Assets.Foundation.SimpleEffect
{
    /// <summary>
    /// 方向跟随目标移动
    /// </summary>
    public class FocusTarget : MonoBehaviour
    {
        /// <summary>
        /// 关注的目标
        /// </summary>
        public GameObject Target;

        private void LookAtTarget()
        {
            if (Target == null)
            {
                return;
            }
            this.transform.LookAt(Target.transform);
        }

        void OnEnable()
        {
            LookAtTarget();
        }

        void Update()
        {
            LookAtTarget();
        }
    }
}
