using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldChunk : MonoBehaviour
{
    MeshCollider collider;

    MeshRenderer renderer;

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
        for(int i = minX; i < maxX; i++)
        {
            for(int j = minY; j < maxY; j++)
            {
                RenderTile(i, j);
            }
        }
    }

    public void RenderTile(int x, int y)
    {

    }
}
