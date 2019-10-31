﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Foundation.Geometry
{
    /// <summary>
    /// 形状
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Shape : MonoBehaviour
    {
        public float width = 1;
        public float height = 1;

        private void Start()
        {
            var mf = this.GetComponent<MeshFilter>();

            var mesh = new Mesh();
            mf.mesh = mesh;

            var vertices = new Vector3[4]
            {
                new Vector3(0, 0, 0),
                new Vector3(width, 0, 0),
                new Vector3(0, height, 0),
                new Vector3(width, height, 0)
            };
            mesh.vertices = vertices;

            var tris = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
            mesh.triangles = tris;

            var normals = new Vector3[4]
            {
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward
            };
            mesh.normals = normals;

            var uv = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(0, 1),
                new Vector2(1, 1)
            };
            mesh.uv = uv;

        }
    }
}
