using UnityEngine;

namespace Assets.Foundation.Actions
{
    /// <summary>
    /// 轨迹运动
    /// </summary>
    public class ActionTrack : ActionTransform
    {
        protected virtual Vector3 GetTrackPosition(float percent)
        {
            return Vector3.zero;
        }
        protected override void DoStep(float percent)
        {
            if (Transform == null)
            {
                return;
            }
            Transform.localPosition = this.GetTrackPosition(percent);
        }
    }

    /// <summary>
    /// 贝塞尔曲线
    /// </summary>
    public class Bezier : ActionTrack
    {
        /// <summary>
        /// 曲线参数
        /// </summary>
        public struct BezierParams
        {
            public Vector3 SourcePoint;
            public Vector3 DestinationPoint;
            public Vector3 ControlPoint1;
            public Vector3 ControlPoint2;
        }


        private BezierParams _params;


        public void InitParams(Vector3 srcPoint, Vector3 destPoint, Vector3 ctrPoint1, Vector3 ctrPoint2)
        {
            _params.SourcePoint = srcPoint;
            _params.DestinationPoint = destPoint;
            _params.ControlPoint1 = ctrPoint1;
            _params.ControlPoint2 = ctrPoint2;
        }

        protected override Vector3 GetTrackPosition(float t)
        {
            if (Mathf.Approximately(t, 1.0f))
            {
                t = 1.0f;
            }
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * _params.SourcePoint;
            p += 3 * uu * t * _params.ControlPoint1;
            p += 3 * u * tt * _params.ControlPoint2;
            p += ttt * _params.DestinationPoint;

            return p;
        }

        public static Bezier Create(float time, Vector3 srcPoint, Vector3 destPoint, Vector3 ctrPoint1, Vector3 ctrPoint2)
        {
            var action = new Bezier();
            action.TotalTime = time;
            action.InitParams(srcPoint, destPoint, ctrPoint1, ctrPoint2);
            return action;
        }
    }

    /// <summary>
    /// 圆
    /// </summary>
    public class Circle : ActionTrack
    {
        /// <summary>
        /// 圆参数
        /// </summary>
        public struct CircleParams
        {
            /// <summary>
            /// 圆心坐标
            /// </summary>
            public Vector3 CenterPoint;
            /// <summary>
            /// 所在平面象类
            /// </summary>
            public Vector3 PanelVector;
            /// <summary>
            /// 圆半径
            /// </summary>
            public float Radius;

            public Vector3 u;
            public Vector3 v;
        }

        private CircleParams _params;


        public void InitParams(Vector3 centerPoint, float radius, Vector3 panelVector)
        {
            _params.CenterPoint = centerPoint;
            _params.Radius = radius;
            _params.PanelVector = panelVector;

            Vector3 u = new Vector3(panelVector.y, -panelVector.x, 0);
            float ul = u.magnitude;
            u *= ul;

            Vector3 v = new Vector3(panelVector.x * panelVector.z, panelVector.y * panelVector.z, -Mathf.Pow(panelVector.x, 2) - Mathf.Pow(panelVector.y, 2));
            float vl = v.magnitude;

            _params.u = u;
            _params.v = v;

        }

        protected override Vector3 GetTrackPosition(float t)
        {
            if (Mathf.Approximately(t, 1.0f))
            {
                t = 1.0f;
            }

            float rad = t * 2 * Mathf.PI;

            Vector3 p;
            p.x = _params.CenterPoint.x + _params.Radius * (_params.u.x * Mathf.Cos(rad) + _params.v.x * Mathf.Sin(rad));
            p.y = _params.CenterPoint.y + _params.Radius * (_params.u.y * Mathf.Cos(rad) + _params.v.y * Mathf.Sin(rad));
            p.z = _params.CenterPoint.z + _params.Radius * (_params.u.z * Mathf.Cos(rad) + _params.v.z * Mathf.Sin(rad));

            return p;
        }

        public static Circle Create(float time, Vector3 centerPoint, float radius, Vector3 panelVector)
        {
            var action = new Circle();
            action.TotalTime = time;
            action.InitParams(centerPoint, radius, panelVector);
            return action;
        }
    }
}
