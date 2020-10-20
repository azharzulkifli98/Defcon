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
    public void Prime(Board board, int minX, int maxX, int minY, int maxY)
    {
        filter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();

        TriangleMap map = new TriangleMap();

        for(int i = minX; i < maxX; i++)
        {
            for(int j = minY; j < maxY; j++)
            {
                RenderTile(map, board.GetTile(i,j));
            }
        }

        filter.mesh = map.GenerateMesh();

        renderer.sharedMaterial = Resources.Load<Material>("Materials/Ground");
    }

    /// <summary>
    /// Renders the tile, including a city or silo if necessary
    /// </summary>
    /// <param name="map"></param>
    /// <param name="tile"></param>
    public void RenderTile(TriangleMap map, BoardTile tile)
    {
        int submesh = 0;

        Vector3 center = new Vector3(tileSize * tile.GetX(), 0, tileSize * tile.GetY());

        Vector3 topLeft = center + Vector3.forward * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 topRight = center + Vector3.forward * tileSize / 2 + Vector3.right * tileSize / 2;
        Vector3 bottomLeft = center + Vector3.back * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 bottomRight = center + Vector3.back * tileSize / 2 + Vector3.right * tileSize / 2;

        map.RegisterTriangle(topLeft, topRight, bottomLeft, submesh);
        map.RegisterTriangle(topRight, bottomRight, bottomLeft, submesh);

        if(tile.GetStruct() != null)
        {
            if (tile.GetStruct().GetID() == "Missile_Silo")
            {
                GameObject prefab = tile.GetStruct().IsDestroyed() ? Resources.Load<GameObject>("Prefabs/Ruined Silo") : Resources.Load<GameObject>("Prefabs/Silo");
                Instantiate(prefab, transform).transform.position = transform.TransformPoint(center);
            }
            if (tile.GetStruct().GetID() == "City")
            {
                GameObject prefab = tile.GetStruct().IsDestroyed()? Resources.Load<GameObject>("Prefabs/Ruined City"): Resources.Load<GameObject>("Prefabs/City");
                Instantiate(prefab, transform).transform.position = transform.TransformPoint(center);
            }
        }
    }
}
