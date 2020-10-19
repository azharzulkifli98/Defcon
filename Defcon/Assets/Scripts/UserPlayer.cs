using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    int silo;

    public override void make_decision(Board playerBoard, Board enemyBoard)
    {
        MouseManager.Prime(enemyBoard, Vector3.zero);

        WorldRenderer.Render(enemyBoard, Vector3.zero);

        UserDisplay.DisplayToPlayer("You're looking at your opponents board. Select a target to hit");

        PrepForMissileSelection();
    }

    public override void set_silos()
    {
        MouseManager.Prime(this.playerBoard, new Vector3(-5, 0, -10));

        WorldRenderer.Render(this.playerBoard, new Vector3(-5, 0, -10));

        UserDisplay.DisplayToPlayer("Select missile silo Location");
        //Sets number of silos allowed to be placed
        silo=3;

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

     //Assignment Variable for number of silo to place
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
            silo --;
            WorldRenderer.Render(this.playerBoard, Vector3.zero);
            MouseManager.Prime(this.playerBoard, Vector3.zero);
        }
        else
        {
            UserDisplay.DisplayToPlayer("Silo already exists in this location");
        }

        if(silo<=0)
        {
            MouseManager.OnTileSelect -= SiloSelectionResponse;
            ready_up();
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
