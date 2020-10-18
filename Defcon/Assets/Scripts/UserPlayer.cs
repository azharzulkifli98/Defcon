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

        UserDisplay.DisplayToPlayer("Select missile silo Location");

        PrepForSiloSelection();
    }

    public void PrepForSiloSelection()
    {
        MouseManager.OnTileSelect += SiloSelectionResponse;

    }
    
    /* TODO:
     * Place a silo on the tile called, if possible
     * If the player has enough silos, you're done
     * Otherwise, insert MouseManager.OnTileSelect -= SiloSelectionResponse and call ready_up();
     */
    public void SiloSelectionResponse(BoardTile tile)
    {
        int x;
        int y;
        x=tile.GetX();
        y=tile.GetY();
        if (tile.GetStruct() == null)
        {
            this.playerBoard.SetMissileSilo(x,y);
            UserDisplay.DisplayToPlayer("Silo Placed");
        }
        else
        {
            UserDisplay.DisplayToPlayer("Silo already exists in this location");
        }




    }
    
    public void PrepForMissileSelection()
    {
        MouseManager.OnTileSelect += RegisterMissile;
    }

    /* Fire the missile by registering to the opponents Board's Missile manager. If more fields/helper functions are needed,
     * create them
     * Also, make sure to unsubscribe (-=) once enough missiles have been fired
     */
    public void RegisterMissile(BoardTile tile)
    {
        
    }
}
