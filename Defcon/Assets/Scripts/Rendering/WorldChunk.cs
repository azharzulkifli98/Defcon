using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object handles rendering of the board for the player to see
/// it is called by the worldrender object for each of the tiles and handles loading prefabs and materials into the scene
/// </summary>
[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class WorldChunk : MonoBehaviour
{
    /// The size of the tiles renderers create
    const float tileSize = 1;

    /// The renderer itself, which displays the mesh in the game world
    new MeshRenderer renderer;

    /// <summary>
    /// Common access point for all objects that need the mesh of this object
    /// </summary>
    MeshFilter filter;

    /// <summary>
    /// Renders the world from [minX, maxX) and [minY, maxY)
    /// </summary>
    /// <param name="minX"> The lowest X value that is rendered</param>
    /// <param name="maxX"> The lowest X value that isn't rendered after minX</param>
    /// <param name="minY"> The lowest Y value that is rendered</param>
    /// <param name="maxY"> The lowest Y value that isn't rendered after minY</param>
    public void Prime(Board board, WorldRenderer.WorldRenderMode mode, int minX, int maxX, int minY, int maxY)
    {
        renderer = GetComponent<MeshRenderer>();
        filter = GetComponent<MeshFilter>();

        TriangleMap map = new TriangleMap();

        for(int i = minX; i < maxX; i++)
        {
            for(int j = minY; j < maxY; j++)
            {
                if (board.GetTile(i,j).GetDiscovered() || mode == WorldRenderer.WorldRenderMode.Discovered)
                {
                    RenderKnownTile(map, board.GetTile(i,j));
                }
                else
                {
                    RenderUnknownTile(map, board.GetTile(i,j));
                }
            }
        }

        filter.mesh = map.GenerateMesh();

        renderer.sharedMaterial = Resources.Load<Material>("Materials/Ground");
    }

    public void RenderUnknownTile(TriangleMap map, BoardTile tile)
    {
        Vector3 minPoint = new Vector3(tileSize * tile.GetX() - .5f, 0, tileSize * tile.GetY() - .5f);
        Vector3 maxPoint = new Vector3(tileSize * tile.GetX() + .5f, 0, tileSize * tile.GetY() + .5f);
        int submesh = 0;

        Vector3 center = new Vector3(tileSize * tile.GetX(), 0, tileSize * tile.GetY());

        Vector3 topLeft = center + Vector3.forward * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 topRight = center + Vector3.forward * tileSize / 2 + Vector3.right * tileSize / 2;
        Vector3 bottomLeft = center + Vector3.back * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 bottomRight = center + Vector3.back * tileSize / 2 + Vector3.right * tileSize / 2;


        map.RegisterTriangle(topLeft, topRight, bottomLeft, submesh);
        map.RegisterTriangle(topRight, bottomRight, bottomLeft, submesh);

        GameObject hiddenTile = new GameObject();

        hiddenTile.transform.parent = transform;

        CloudRenderer renderer = hiddenTile.AddComponent<CloudRenderer>();

        renderer.Prime(transform.TransformPoint(minPoint), transform.TransformPoint(maxPoint));
    }

    /// <summary>
    /// Renders an already known tile
    /// </summary>
    /// <param name="map"></param>
    /// <param name="tile"></param>
    public void RenderKnownTile(TriangleMap map, BoardTile tile)
    {
        int submesh = 0;

        Vector3 center = new Vector3(tileSize * tile.GetX(), 0, tileSize * tile.GetY());

        Vector3 topLeft = center + Vector3.forward * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 topRight = center + Vector3.forward * tileSize / 2 + Vector3.right * tileSize / 2;
        Vector3 bottomLeft = center + Vector3.back * tileSize / 2 + Vector3.left * tileSize / 2;
        Vector3 bottomRight = center + Vector3.back * tileSize / 2 + Vector3.right * tileSize / 2;

        // setting up the size of each tile for rendering
        map.RegisterTriangle(topLeft, topRight, bottomLeft, submesh);
        map.RegisterTriangle(topRight, bottomRight, bottomLeft, submesh);


        // here is where we decide what the appearance of the structure should be: silo, city, ruined city, etc
        if (tile.GetStruct() != null)
        {
            if (tile.GetStruct().GetID() == "Missile_Silo")
            {
                GameObject prefab = tile.GetStruct().IsDestroyed() ? Resources.Load<GameObject>("Prefabs/Ruined Silo") : Resources.Load<GameObject>("Prefabs/Silo");
                Instantiate(prefab, transform).transform.position = transform.TransformPoint(center);
            }
            if (tile.GetStruct().GetID() == "City")
            {
                GameObject prefab = tile.GetStruct().IsDestroyed() ? Resources.Load<GameObject>("Prefabs/Ruined City") : Resources.Load<GameObject>("Prefabs/City");
                Instantiate(prefab, transform).transform.position = transform.TransformPoint(center);
            }
        }
    }
}
