using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    static Board board;
    static Vector3 offset;

    [SerializeField]
    Transform highlighter;

    public static void Prime(Board b1, Vector3 offset1)
    {
        MouseManager.board = b1;
        MouseManager.offset = offset1;
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);

        float dist;

        if(ground.Raycast(ray, out dist))
        {
            Vector3 point = ray.GetPoint(dist);
            highlighter.position = round_point(point);
        }

        if(Input.GetMouseButtonDown(0) && convert_point_to_tile(highlighter.position) != null && OnTileSelect != null)
        {
            OnTileSelect(convert_point_to_tile(highlighter.position));
        }
    }

    public Vector3 round_point(Vector3 point)
    {
        return new Vector3(Mathf.Round(point.x), 0, Mathf.Round(point.z));
    }

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
    public delegate void onTileSelect(BoardTile selected);
    public static event onTileSelect OnTileSelect;
}
