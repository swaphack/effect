
using UnityEngine;
using Game.ComputationalGeometry.Convex;

namespace Game.Test
{
    /// <summary>
    /// 凸包测试
    /// </summary>
    public class TestConvex2D : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 100)]
        private int count = 50;
        [SerializeField]
        [Range(0, 100)]
        private float width = 50;
        [SerializeField]
        [Range(0, 100)]
        private float height = 50;

        private Vector2[] points;

        private Vector2[] convexPoints;

        private void Start()
        {
            Random.InitState((int)System.DateTime.Now.Ticks / 1000);

            points = new Vector2[count];

            for (var i = 0; i < count; i++)
            {
                float x = Random.Range(0, width);
                float y = Random.Range(0, height);

                points[i] = new Vector2(x, y);
            }

            convexPoints = Convex2D.GetConvexHull(points);

            var vertices = new Vector3[convexPoints.Length];
            var normals = new Vector3[convexPoints.Length];
            
            for (var i = 0; i < convexPoints.Length; i++)
            {
                vertices[i] = convexPoints[i];
                normals[i] = - Vector3.forward;
            }

            var triangleCount = convexPoints.Length - 2;
            var triangles = new int[3 * triangleCount];
            for (var i = 0; i < triangleCount; i++)
            {
                triangles[i * 3 + 0] = 0;
                triangles[i * 3 + 1] = i+1;
                triangles[i * 3 + 2] = i+2;
            }

            var meshFilter = this.gameObject.AddComponent<MeshFilter>();
            var meshRender = this.gameObject.AddComponent<MeshRenderer>();
            var mesh = new Mesh();
            meshFilter.mesh = mesh;

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;
        }

        void Update()
        {
            /*
            if (count >= 2)
            {
                for (var i = 0; i < count - 1; i++)
                {
                    Debug.DrawLine(points[i], points[i + 1], Color.green, 2);
                }
            }

            if (convexPoints != null && convexPoints.Length > 2)
            {
                for (var i = 0; i < convexPoints.Length; i++)
                {
                    Debug.DrawLine(convexPoints[i], convexPoints[(i + 1) % convexPoints.Length], Color.red, 2);
                }
            }
            */
        }
    }
}


