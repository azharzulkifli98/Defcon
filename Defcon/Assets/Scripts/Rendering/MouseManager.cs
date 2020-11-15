using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    /// <summary>
    /// The currently assigned board for this MouseManager
    /// </summary>
    Board board;

    /// <summary>
    /// The offset that this world is using
    /// </summary>
    Vector3 offset;

    public void Prime(Board board, Vector3 offset)
    {
        this.board = board;
        this.offset = offset;
    }

    /// <summary>
    /// Raycasts the mouse onto the world, to help us determine if it is necessary to call OnTileSelect
    /// </summary>
    public void Update()
    {
        if (board == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        Vector3 position;

        float dist;

        if (ground.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            position = round_point(point);

            if (convert_point_to_tile(position) != null)
            {
                if (OnTileHover != null)
                {
                    OnTileHover(convert_point_to_tile(position));
                }
                if (Input.GetMouseButtonDown(0) && OnTileSelect != null)
                {
                    OnTileSelect(convert_point_to_tile(position));
                }
            }
        }
    }

    /// <summary>
    /// Rounds the passed point to the nearest full integer x,y,z
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public Vector3 round_point(Vector3 point)
    {
        return new Vector3(Mathf.Round(point.x), 0, Mathf.Round(point.z));
    }

    /// <summary>
    /// uses round_point() to get the tile that the mouse is hovering over
    /// returns a tile or null if it the mouse isn't in range
    /// </summary>
    public BoardTile convert_point_to_tile(Vector3 point)
    {
        int x = Mathf.RoundToInt(point.x);
        int y = Mathf.RoundToInt(point.z);

        int offset_x = Mathf.RoundToInt(offset.x);
        int offset_y = Mathf.RoundToInt(offset.z);

        int tile_x = -1;
        int tile_y = -1;

        if(x - offset_x >= 0 && x - offset_x < board.GetWidth())
        {
            tile_x = x - offset_x;
        }
        if(y - offset_y >= 0 && y - offset_y < board.GetHeight())
        {
            tile_y = y - offset_y;
        }

        if(tile_y >= 0 && tile_x >= 0)
        {
            return board.GetTile(tile_x, tile_y);
        }

        return null;
    }

    /* EXPLANATION:
    * If I have a function of the form void FUNCTION_NAME(BoardTile ARGUMENTNAME),
    * then I can add it to the OnTileSelect event by:
    * 
    * somewhere in code, writing the following:
    * MouseManager.OnTileSelect += FUNCTION_NAME;
    * 
    * If you do this, you MUST, in the FUNCTION_NAME's declaration, include the statement
    * 
    * MouseManager.OnTileSelect -= FUNCTION_NAME
    * 
    * When the user selects a tile, any function that has been +='ed but not -='ed will be called
    */
    public delegate void onTileEvent(BoardTile selected);
    public event onTileEvent OnTileSelect;

    /// <summary>
    /// Called when the mouse hovers over a given tile
    /// </summary>
    public event onTileEvent OnTileHover;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(offset, 1f);
    }
}
