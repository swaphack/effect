using Boo.Lang;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(new Vector3(0, 0, 0));
        vertices.Add(new Vector3(1, 0, 0));
        vertices.Add(new Vector3(1, 1, 0));
        vertices.Add(new Vector3(0, 1, 0));

        int[] indices = new int[]{ 0, 2, 1, 0, 3, 2 };

        Mesh mesh = new Mesh();
        mesh.name = "CustomMesh";
        mesh.vertices = vertices.ToArray();
        mesh.triangles = indices;

        this.GetComponent<MeshFilter>().mesh = mesh;
        this.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Custom/TestShader"));

    }

    // Update is called once per frame
    void Update()
    {
    }
}
