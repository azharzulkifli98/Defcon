using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRenderManager
{
    static Board userBoard;

    static Board enemyBoard;

    /// <summary>
    /// Used to render the user's board (closest to camera)
    /// </summary>
    public static WorldRenderer UserBoard = new WorldRenderer(new Vector3(-5, 0, -10), WorldRenderer.WorldRenderMode.Discovered);

    /// <summary>
    /// Used to render the opponent's board (farthest from camera)
    /// </summary>
    public static WorldRenderer EnemyBoard = new WorldRenderer(new Vector3(-5, 0, 5), WorldRenderer.WorldRenderMode.Hidden);

    public static void RenderUser(Board board)
    {
        userBoard = board;
        UserBoard.Render(board);
    }

    public static void RenderEnemy(Board board)
    {
        enemyBoard = board;
        EnemyBoard.Render(board);
    }

    public static void UpdateRender()
    {
        RenderUser(userBoard);
        RenderEnemy(enemyBoard);
    }

    public delegate void onTileHover(BoardTile tile, WorldRenderer.WorldRenderMode mode);
    public static onTileHover OnTileHover;

    public static void WhenUserTileHover(BoardTile tile)
    {
        if(OnTileHover != null)
        {
            OnTileHover(tile, WorldRenderer.WorldRenderMode.Discovered);
        }
    }

    public static void WhenEnemyTileHover(BoardTile tile)
    {
        if (OnTileHover != null)
        {
            OnTileHover(tile, tile.GetDiscovered()?WorldRenderer.WorldRenderMode.Discovered:WorldRenderer.WorldRenderMode.Hidden);
        }
    }

    public static Vector3 GetOffset(Board board)
    {
        if(board == userBoard)
        {
            return new Vector3(-5, 0, -10);
        }
        else
        {
            return new Vector3(-5, 0, 5);
        }
    }
}
