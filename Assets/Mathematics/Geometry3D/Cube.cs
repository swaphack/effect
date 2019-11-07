using UnityEngine;

namespace Game.Mathematics.Geometry3D
{
    /// <summary>
    /// 立方体
    /// 以左下角为原点
    /// </summary>
    public struct Cube
    {
        public Vector3 Orgin { get; private set; }
        public Vector3 Size { get; private set; }


        public Vector3 FrontLeftDown => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(0, 0, 0);
        public Vector3 FrontRightDown => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(Size.x, 0, 0);
        public Vector3 FrontRightUp => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(Size.x, Size.y, 0);
        public Vector3 FrontLeftUp => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(0, Size.y, 0);

        public Vector3 BackLeftDown => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(0, 0, 0);
        public Vector3 BackRightDown => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(Size.x, 0, 0);
        public Vector3 BackRightUp => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(Size.x, Size.y, 0);
        public Vector3 BackLeftUp => new Vector3(Orgin.x, Orgin.y, Orgin.z) + new Vector3(0, Size.y, 0);

        public Cube(Vector3 orgin, Vector3 size)
        {
            Orgin = orgin;
            Size = size;
        }

        public bool Contains(Vector3 point)
        {
            return MathUtility.IsInRange(point, Orgin, Orgin + Size);
        }

        public bool Intersect(Cube cube)
        {
            return false;
        }
    }
}