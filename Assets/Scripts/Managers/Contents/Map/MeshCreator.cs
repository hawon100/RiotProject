using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    void Start()
    {
        CreateMesh();
    }

    void CreateMesh()
    {
        Vector3[] vertices = {
			
		};

		int[] triangles = {
			0, 2, 1, //face front
			0, 3, 2,

			2, 3, 4, //face top
			2, 4, 5,

			1, 2, 5, //face right
			1, 5, 6,

			0, 7, 4, //face left
			0, 4, 3,

			5, 4, 7, //face back
			5, 7, 6,

			0, 6, 7, //face bottom
			0, 1, 6
		};

        Vector2 [] uvs = new Vector2 [vertices.Length]; 

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        Mesh mesh = new();

        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles; 
        mesh.uv = uvs;
        mesh.Optimize();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
    }

    private Vector3[] GetVertices(){
        float cubeLength = transform.localScale.x;
        float cubeWidth = transform.localScale.y;
        float cubeHeight = transform.localScale.z;

		Vector3 v0 = new(cubeLength * -0.5f, cubeWidth * -0.5f, cubeHeight * 0.5f);
		Vector3 v1 = new(cubeLength * 0.5f, cubeWidth * -0.5f, cubeHeight * 0.5f);
        Vector3 v2 = new(cubeLength * 0.5f, cubeWidth * -0.5f, cubeHeight * -0.5f);
        Vector3 v3 = new(cubeLength * -0.5f, cubeWidth * -0.5f, cubeHeight * -0.5f);
		Vector3 v4 = new(cubeLength * -0.5f, cubeWidth * 0.5f, cubeHeight * 0.5f);
		Vector3 v5 = new(cubeLength * 0.5f, cubeWidth * 0.5f, cubeHeight * 0.5f);
		Vector3 v6 = new(cubeLength * 0.5f, cubeWidth * 0.5f, cubeHeight * -0.5f);
		Vector3 v7 = new(cubeLength * -0.5f, cubeWidth * 0.5f, cubeHeight * -0.5f);

        Vector3[] vertices = new Vector3[]
        {
            v0, v1, v2, v3, //bottom

            v7, v4, v0, v3, //left

            v4, v5, v1, v0, //front

            v6, v7, v3, v2, //back

            v5, v6, v2, v1, //right

            v7, v6, v5, v4  //top
        };

        return vertices;
    }
}
