using UnityEngine;
using System.Collections;

namespace AI.PCG
{

    public class QuadGenerator : MonoBehaviour
    {
        MeshFilter meshFilter;
        Mesh mesh;

        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            mesh = new Mesh();
            mesh.name = "The best quad in the world";

        }

        public float width = 1f;
        public float height = 1f;

        void Update()
        {
            // Create the vertex list
            Vector3[] vertices = new Vector3[4];
            vertices[0] = new Vector3(-width / 2, -height / 2, 0);
            vertices[1] = new Vector3(-width / 2, height / 2, 0);
            vertices[2] = new Vector3(width / 2, height / 2, 0);
            vertices[3] = new Vector3(width / 2, -height / 2, 0);
            mesh.vertices = vertices;

            // Create the triangles list
            int[] triangles = new int[6];

            // triangle 1
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            // triangle 2
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            mesh.triangles = triangles;

            // Create UVs
            Vector2[] uvs = new Vector2[4];
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0, 1);
            uvs[2] = new Vector2(1, 1);
            uvs[3] = new Vector2(1, 0);
            mesh.uv = uvs;


            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            meshFilter.mesh = mesh;

            GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }

}