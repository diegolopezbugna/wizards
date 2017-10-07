using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour {

    [SerializeField]
    private NavMeshSurface navMeshSurface;

    [SerializeField]
    private Terrain terrain;

	// Use this for initialization
	void Start () {

        var vertices2d = new List<Vector2>();
        var alphaMaps = terrain.terrainData.GetAlphamaps(0, 0, terrain.terrainData.alphamapWidth, terrain.terrainData.alphamapHeight);

        for (int x = 0; x < terrain.terrainData.alphamapWidth; x++)
        {
            for (int y = 0; y < terrain.terrainData.alphamapHeight; y++)
            {
                if (alphaMaps[x, y, 1] > alphaMaps[x, y, 0]) // && alphaMaps[x, y, 1] < 1)
                {
                    Debug.LogFormat("[{0},{1}] {2} {3}", x, y, alphaMaps[x, y, 0], alphaMaps[x, y, 1]);
                    vertices2d.Add(new Vector2(x, y));
                }
            }
        }

        // Use the triangulator to get indices for creating triangles
        var triangulator = new Triangulator(vertices2d.ToArray());
        var indices = triangulator.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2d.Count];
        for (int i=0; i<vertices.Length; i++) {
            vertices[i] = new Vector3(vertices2d[i].x, 0, vertices2d[i].y);
        }

        // Create the mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Set up game object with mesh;
        gameObject.AddComponent(typeof(MeshRenderer));
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = mesh;


        navMeshSurface.BuildNavMesh();
	}
	
}
