using UnityEngine;
using System.Collections;

public class MeshGenerationDemo : MonoBehaviour
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;

    private float cubeLength;
    private float cubeWidth;
    private float cubeHeight;

    private Mesh cubeMesh;

    void Start()
    {
        MakeCube();
    }

    private Vector3[] GetVertices()
    {
        Vector3 vertice_0 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
        Vector3 vertice_1 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, cubeHeight * .5f);
        Vector3 vertice_2 = new Vector3(cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);
        Vector3 vertice_3 = new Vector3(-cubeLength * .5f, -cubeWidth * .5f, -cubeHeight * .5f);

        Vector3 vertice_4 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
        Vector3 vertice_5 = new Vector3(cubeLength * .5f, cubeWidth * .5f, cubeHeight * .5f);
        Vector3 vertice_6 = new Vector3(cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);
        Vector3 vertice_7 = new Vector3(-cubeLength * .5f, cubeWidth * .5f, -cubeHeight * .5f);

        Vector3[] vertices = new Vector3[]
        {					
			vertice_7, vertice_4, vertice_0, vertice_3, // Left Polygon
					
			vertice_4, vertice_5, vertice_1, vertice_0, // Front Polygon
					
			vertice_6, vertice_7, vertice_3, vertice_2, // Back Polygon
					
			vertice_5, vertice_6, vertice_2, vertice_1, // Right Polygon
				
			vertice_7, vertice_6, vertice_5, vertice_4 // Top Polygon
		};

        return vertices;
    }

    private Vector3[] GetNormals()
    {
        Vector3 up = Vector3.up;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {					
			left, left, left, left, // LEFT Side Render
					
			front, front, front, front, // FRONT Side Render
									
			back, back, back, back, // BACK Side Render
					
			right, right, right, right, // RIGTH Side Render

			up, up, up, up // UP Side Render
		};

        return normales;
    }

    private Vector2[] GetUVsMap()
    {
        Vector2 _00_CORDINATES = new Vector2(0f, 0f);
        Vector2 _10_CORDINATES = new Vector2(1f, 0f);
        Vector2 _01_CORDINATES = new Vector2(0f, 1f);
        Vector2 _11_CORDINATES = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
        {					
			_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES, // Left
				
			_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES, // Front					
				
			_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES, // Back
				
			_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES, // Right					
				
			_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES, // Top
        };
        return uvs;
    }

    private int[] GetTriangles()
    {
        int[] triangles = new int[]
        {
		// Cube Left Side Triangles
			3, 1, 0,
            3, 2, 1,			
					
		// Cube Front Side Triangles
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
            3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
					
		// Cube Back Side Triangles
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
            3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
					
		// Cube Rigth Side Triangles
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
            3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
					
		// Cube Top Side Triangles
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
            3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,		
        };

        return triangles;
    }

    private Mesh GetCubeMesh()
    {

        if (GetComponent<MeshFilter>() == null)
        {
            Mesh mesh;
            MeshFilter filter = gameObject.AddComponent<MeshFilter>();
            mesh = filter.mesh;
            mesh.Clear();
            return mesh;
        }
        else
        {
            return gameObject.GetComponent<MeshFilter>().mesh;
        }
    }

    void MakeCube()
    {
        cubeMesh = GetCubeMesh();
        cubeMesh.vertices = GetVertices();
        cubeMesh.normals = GetNormals();
        cubeMesh.uv = GetUVsMap();
        cubeMesh.triangles = GetTriangles();
        cubeMesh.RecalculateBounds();
        cubeMesh.RecalculateNormals();
        cubeMesh.Optimize();
    }


}
