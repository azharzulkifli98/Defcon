using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMap
{
    /// <summary>
    /// A dictionary containing all the points registered to the triangle map so far
    /// </summary>
    Dictionary<Vector3, int> indexDictionary;

    /// <summary>
    /// A list of all the submeshes in the TriangleMap, which
    /// are each individually stored as a list of indexes of points
    /// </summary>
    List<List<int>> submeshes;

    /// <summary>
    /// Initializes a default TriangleMap
    /// </summary>
    public TriangleMap()
    {
        indexDictionary = new Dictionary<Vector3, int>();
        submeshes = new List<List<int>>();
    }

    /// <summary>
    /// Adds the point to the given submesh. 
    /// </summary>
    /// <param name="point"></param>
    /// <param name="submesh"></param>
    private void AddToSubMesh(Vector3 point, int submesh)
    {
        while(submesh >= submeshes.Count)
        {
            submeshes.Add(new List<int>());
        }
        if (indexDictionary.ContainsKey(point))
        {
            submeshes[submesh].Add(indexDictionary[point]);
        }
        else
        {
            indexDictionary.Add(point, indexDictionary.Count);
            submeshes[submesh].Add(indexDictionary.Count - 1);
        }
    }

    /// <summary>
    /// Registers a triangle, where a,b,c are traversed in order in a clockwise direction
    /// </summary>
    /// <param name="a">First point by clockwise traversal</param>
    /// <param name="b">Second point by clockwise traversal</param>
    /// <param name="c">Third point by clockwise traversal</param>
    public void RegisterTriangle(Vector3 a, Vector3 b, Vector3 c, int submesh = 0)
    {
        AddToSubMesh(a, submesh);
        AddToSubMesh(b, submesh);
        AddToSubMesh(c, submesh);
    }

    /// <summary>
    /// Generates a mesh from the data contained in the traingle map
    /// </summary>
    /// <returns></returns>
    public Mesh GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[indexDictionary.Count];

        foreach(Vector3 vec in indexDictionary.Keys)
        {
            vertices[indexDictionary[vec]] = vec;
        }

        List<Vector3> vertList = new List<Vector3>(0);

        for (int i = 0; i < vertices.Length; i++)
        {
            vertList.Add(vertices[i]);
        }
        mesh.SetVertices(vertList);

        mesh.subMeshCount = submeshes.Count;

        for (int i = 0; i < submeshes.Count; i++)
        {
            mesh.SetTriangles(submeshes[i], i);
        }

        mesh.RecalculateNormals();

        return mesh;
    }
}