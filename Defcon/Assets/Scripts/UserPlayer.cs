using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    public override void make_decision(Board playerBoard, Board enemyBoard)
    {
        
    }

    public override void set_silos(Board board)
    {
        MouseManager.Prime(board, new Vector3(-5, 0, -10));

        WorldRenderer.Render(board, new Vector3(-5, 0, -10));

        UserDisplay.DisplayToPlayer("Select silo locations with the mouse button.");
    }
}
