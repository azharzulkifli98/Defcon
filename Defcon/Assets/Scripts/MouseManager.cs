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
            convert_point_to_tile(round_point(point));
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

        int offset1_x = Mathf.RoundToInt(offset.x);
        int offset2_y = Mathf.RoundToInt(offset.z);

        if(x + offset1_x > 0 && x + offset1_x < board.GetWidth())
        {
            Debug.Log("WOOP");
        }

        return null;
    }
}
