using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer
{
    const int MAX_DIM = 10;

    static List<WorldChunk> worldChunks = new List<WorldChunk>();

    public static void Render(Board board, Vector3 offset)
    {
        FlushChunks();

        for (int x = 0; x < board.GetWidth() + MAX_DIM; x += MAX_DIM)
        {
            for (int y = 0; y < board.GetHeight() + MAX_DIM; y += MAX_DIM)
            {
                WorldChunk chunk = new GameObject("Chunk").AddComponent<WorldChunk>();
                chunk.transform.position = offset;
                chunk.Prime(board, x, x + MAX_DIM < board.GetWidth() ? x + MAX_DIM : board.GetWidth(), y, y + MAX_DIM < board.GetHeight() ? y + MAX_DIM : board.GetHeight());
                worldChunks.Add(chunk);
            }
        }
    }

    /// <summary>
    /// Destroys the chunks so that a new world can be rendered
    /// </summary>
    public static void FlushChunks()
    {
        for(int i = 0; i < worldChunks.Count; i++)
        {
            Object.Destroy(worldChunks[i].gameObject);
        }
        worldChunks = new List<WorldChunk>();
    }
}
