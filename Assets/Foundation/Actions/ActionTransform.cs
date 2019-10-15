using UnityEngine;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 空间矩阵动作
    /// </summary>
    public class ActionTransform : ActionInterval
    {
        public Transform Transform 
        {
            get 
            {
                if (Target == null)
                {
                    return null;
                }
                return Target.transform;
            }
        }
    }

    /// <summary>
    /// 位移增量
    /// </summary>
    public class MoveBy : ActionTransform
    {
        private Vector3 _different;
        private Vector3 _source;


        public Vector3 Different
        {
            get
            {
                return _different;
            }
            set
            {
                _different = value;
            }
        }

        public Vector3 Source
        {
            get
            {
                return _source;
            }
            protected set
            {
                _source = value;
            }
        }

        protected override void DoStep(float percent)
        {
            if (Transform == null)
            {
                return;
            }
            Transform.localPosition = Source + Different * percent;
            if (Mathf.Approximately(percent, 1.0f))
            {
                Transform.localPosition = Source + Different;
            }
        }

        protected override void DoWithTarget()
        {
            Source = Transform.localPosition;
        }
        public static MoveBy Create(float time, Vector3 diffPos)
        {
            var action = new MoveBy();
            action.TotalTime = time;
            action.Different = diffPos;
            return action;
        }
    }

    /// <summary>
    /// 移动到目标
    /// </summary>
    public class MoveTo : MoveBy
    {
        private Vector3 _destination;
       

        public Vector3 Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }        

        protected override void DoWithTarget()
        {
            base.DoWithTarget();
            Different = Destination - Source;
        }

        public static new MoveTo Create(float time, Vector3 destPos)
        {
            var action = new MoveTo();
            action.TotalTime = time;
            action.Destination = destPos;
            return action;
        }
    }

    /// <summary>
    /// 缩放增量
    /// </summary>
    public class ScaleBy : ActionTransform
    {
        private Vector3 _different;
        private Vector3 _source;

        public Vector3 Different
        {
            get
            {
                return _different;
            }
            set
            {
                _different = value;
            }
        }

        public Vector3 Source
        {
            get
            {
                return _source;
            }
            protected set
            {
                _source = value;
            }
        }

        protected override void DoStep(float percent)
        {
            if (Transform == null)
            {
                return;
            }
            Transform.localScale = Source + Different * percent;
            if (Mathf.Approximately(percent, 1.0f))
            {
                Transform.localPosition = Source + Different;
            }
        }

        protected override void DoWithTarget()
        {
            Source = Transform.localPosition;
        }
        public static ScaleBy Create(float time, Vector3 diffScale)
        {
            var action = new ScaleBy();
            action.TotalTime = time;
            action.Different = diffScale;
            return action;
        }
    }

    /// <summary>
    /// 缩放到目标
    /// </summary>
    public class ScaleTo : ScaleBy
    {
        private Vector3 _destination;
        

        public Vector3 Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }

        protected override void DoWithTarget()
        {
            base.DoWithTarget();
            Different = Destination - Source;
        }

        public static new ScaleTo Create(float time, Vector3 destScale)
        {
            var action = new ScaleTo();
            action.TotalTime = time;
            action.Destination = destScale;
            return action;
        }
    }

    /// <summary>
    /// 旋转增量
    /// </summary>
    public class RotateBy : ActionTransform
    {
        private Vector3 _different;
        private Vector3 _source;

        public Vector3 Different
        {
            get
            {
                return _different;
            }
            set
            {
                _different = value;
            }
        }

        public Vector3 Source
        {
            get
            {
                return _source;
            }
            protected set
            {
                _source = value;
            }
        }

        protected override void DoWithTarget()
        {
            Source = Transform.localEulerAngles;
        }

        protected override void DoStep(float percent)
        {
            if (Transform == null)
            {
                return;
            }
            Transform.localEulerAngles = Source + Different * percent;
            if (Mathf.Approximately(percent, 1.0f))
            {
                Transform.localEulerAngles = Source + Different;
            }
        }

        public static RotateBy Create(float time, Vector3 diffRotation)
        {
            var action = new RotateBy();
            action.TotalTime = time;
            action.Different = diffRotation;
            return action;
        }
    }

    /// <summary>
    /// 旋转到目标
    /// </summary>
    public class RotateTo : RotateBy
    {
        private Vector3 _destination;
        

        public Vector3 Destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }        

        protected override void DoWithTarget()
        {
            base.DoWithTarget();
            Different = Destination - Source;
        }


        public static new RotateTo Create(float time, Vector3 destRotation)
        {
            var action = new RotateTo();
            action.TotalTime = time;
            action.Destination = destRotation;
            return action;
        }
    }
}
