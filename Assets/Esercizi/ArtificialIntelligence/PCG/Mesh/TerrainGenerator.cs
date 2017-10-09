using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI.PCG
{

    public class TerrainGenerator : MonoBehaviour
    {
        MeshFilter meshFilter;
        Mesh mesh;

        void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            mesh = new Mesh();
            mesh.name = "The best TERRAIN in the world";

            meshFilter.mesh = mesh;
            GetComponent<MeshCollider>().sharedMesh = mesh;
        }

        public int nX = 10;
        public int nY = 10;
        public float quadSize = 1f;
        public float frequency = 1f;

        private List<Vector3> allVertices = new List<Vector3>();
        private List<int> allTriangles = new List<int>();
        private List<Vector2> allUVs = new List<Vector2>();

        void BuildQuad(Vector3 origin, float size, Vector3 normal)
        {
            int firstIndex = allVertices.Count;

            // Create the vertex list
            Vector3[] vertices = new Vector3[4];
            vertices[0] = origin + new Vector3(-size / 2, -size / 2, 0);
            vertices[1] = origin + new Vector3(-size / 2, size / 2, 0);
            vertices[2] = origin + new Vector3(size / 2, size / 2, 0);
            vertices[3] = origin + new Vector3(size / 2, -size / 2, 0);
            allVertices.AddRange(vertices);

            // Create the triangles list
            int[] triangles = new int[6];

            // triangle 1
            triangles[0] = firstIndex + 0;
            triangles[1] = firstIndex + 1;
            triangles[2] = firstIndex + 2;

            // triangle 2
            triangles[3] = firstIndex + 0;
            triangles[4] = firstIndex + 2;
            triangles[5] = firstIndex + 3;

            allTriangles.AddRange(triangles);

            // Create UVs
            float l = nY * size + 1;

            Vector2[] uvs = new Vector2[4];
            uvs[0] = (Vector2)origin / l + new Vector2(0, 0);
            uvs[1] = (Vector2)origin / l + new Vector2(0, 1 / l);
            uvs[2] = (Vector2)origin / l + new Vector2(1/ l, 1 / l);
            uvs[3] = (Vector2)origin / l + new Vector2(1 / l, 0);
            allUVs.AddRange(uvs);
        }

        void CompleteMesh()
        {
            mesh.vertices = allVertices.ToArray();
            mesh.triangles = allTriangles.ToArray();
            mesh.uv = allUVs.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }

        void ResetMesh()
        {
            allVertices.Clear();
            allTriangles.Clear();
            allUVs.Clear();
        }

        public Color lowColor = Color.white;
        public Color highColor = Color.white;

        void Update()
        {
            ResetMesh();
            for (int x = 0; x < nX; x++)
            {
                for (int y = 0; y < nY; y++)
                {
                    var xx = (x / (float)nX - 0.5f) * 2f;
                    var yy = (y / (float)nY - 0.5f) * 2f;

                    var v = xx * xx + yy * yy;

                    v += Time.time * 1f;

                    Vector3 origin;
                    origin.x = x * quadSize;
                    origin.y = y * quadSize;
                    origin.z = 0;
                    /*
                     Mathf.Repeat(v + 1 * Mathf.PerlinNoise(1 * Time.time, 0), 1f);
                    lowColor.r = Mathf.PerlinNoise(1 * Time.time, 0);
                    highColor.g = Mathf.PerlinNoise(1 * Time.time, 0);
                    */
                    origin.z = 
                        2* Mathf.PerlinNoise(
                        frequency * (x + 10*Time.time) / ((float)nX*Time.time),
                        frequency * (y + 10 * Time.time) / (float)nY
                        );

                    BuildQuad(origin, quadSize, Vector3.forward);
                }
            }
            CompleteMesh();
        }


    }

}