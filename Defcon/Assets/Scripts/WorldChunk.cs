using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class WorldChunk : MonoBehaviour
{
    /// <summary>
    /// The size of the tiles renderers create
    /// </summary>
    const float tileSize = 1;

    /// <summary>
    /// A MeshCollider, to be used for creation of a collision mesh we can cast rays to to provide players with a population count tooltip
    /// </summary>
    new MeshCollider collider;

    /// <summary>
    /// The renderer itself, which displays the mesh in the game world
    /// </summary>
    new MeshRenderer renderer;

    MeshFilter filter;

    //TODO: Add additional "Board" argument
    /// <summary>
    /// Renders the world from [minX, maxX) and [minY, maxY)
    /// </summary>
    /// <param name="minX"> The lowest X value that is rendered</param>
    /// <param name="maxX"> The lowest X value that isn't rendered after minX</param>
    /// <param name="minY"> The lowest Y value that is rendered</param>
    /// <param name="maxY"> The lowest Y value that isn't rendered after minY</param>
    public void Prime(int minX, int maxX, int minY, int maxY)
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();

        TriangleMap map = new TriangleMap();

        for(int i = minX; i < maxX; i++)
        {
            for(int j = minY; j < maxY; j++)
            {
                RenderTile(map, i, j);
            }
        }

        filter.mesh = map.GenerateMesh();
    }

    public void RenderTile(TriangleMap map, int x, int y)
    {
        int submesh = 0;
        if(Random.Range(0, 5) > 1)
        {
            submesh = 1;
        }

        Vector3 center = new Vector3(tileSize * x, 0, tileSize * y);

        Vector3 topLeft = center + Vector3.forward * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 topRight = center + Vector3.forward * tileSize / 2 + Vector3.right * tileSize / 2;
        Vector3 bottomLeft = center + Vector3.back * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 bottomRight = center + Vector3.back * tileSize / 2 + Vector3.right * tileSize / 2;

        map.RegisterTriangle(topLeft, topRight, bottomLeft, submesh);
        map.RegisterTriangle(topRight, bottomRight, bottomLeft, submesh);
    }

    private void Start()
    {
        Prime(0, 10, 0, 10);
    }
}
