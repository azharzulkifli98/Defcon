using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderer
{
    /// <summary>
    /// The maximum width and height of a chunk
    /// </summary>
    const int MAX_DIM = 10;

    /// <summary>
    /// The offset from (0,0,0) that the 
    /// </summary>
    static Vector3 offset = new Vector3(-5, 0, -10);

    /// <summary>
    /// A list of currently instantiated world chunks
    /// </summary>
    static List<WorldChunk> worldChunks = new List<WorldChunk>();

    /// <summary>
    /// Renders the given board at the offset.
    /// TODO: Require an offset and allow the board to be turned around, so we can have two boards face each other
    /// </summary>
    /// <param name="board"></param>
    public static void Render(Board board)
    {
        FlushChunks();

        MouseManager.Prime(board, offset);

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
