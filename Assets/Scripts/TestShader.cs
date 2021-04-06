using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer renderer = this.GetComponent<MeshRenderer>();
        if (renderer != null) 
        {
            Material material = renderer.material;
            if (material != null)
            {
                material.SetColor("_Color", Color.black);

                Shader shader = material.shader;
                if (shader != null)
                {
                    int index = shader.FindPropertyIndex("_Color");
                    Debug.Log("Property Index " + index.ToString());
                }
            }
        } 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
