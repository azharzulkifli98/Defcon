﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object inherits from Player and lets the player decide what to do using make_decision() and set_silos()
/// also uses enemyboard to handle missiles fired by player
///     make_decision -> PrepForMissleSelection -> RegisterMissile -> end_decision
///     set_silo -> PrepForSiloSelection -> SiloSelectionResponse -> ready_up
/// </summary>
public class UserPlayer : Player
{
    // number of silos player can use
    int silo;

    List<MissileSilo> silos = new List<MissileSilo>();

    int siloIndex = 0;
    
    // board that player will fire missiles at
    Board enemyBoard;


    /// <summary>
    /// called by the Game Manager, first renders enemy board then calls PrepForMissileSelection
    /// which will add a missile the the MissileManager given the tile the player selected
    /// </summary>
    public override void make_decision(Board playerBoard, Board enemyBoard)
    { 
        UserDisplay.DisplayToPlayer("You're looking at your opponents board. Select a target to hit");

        this.enemyBoard = enemyBoard;
        Debug.Log(enemyBoard.GetTotalPopulation());

        WorldRenderManager.EnemyBoard.mouseManager.OnTileSelect += RegisterMissile;
    }

    /// <summary>
    /// also called by the Game Manager, renders the player board then calls PrepForMissileSelection
    /// so that the player can mouse over the tiles and place down silos
    /// </summary>
    public override void set_silos()
    {
        UserDisplay.DisplayToPlayer("Select missile silo Location");
        //Sets number of silos allowed to be placed
        silo=3;

        PrepForSiloSelection();
    }

    // not sure if this can be combined with another function or not -Azhar
    public void PrepForSiloSelection()
    {
        WorldRenderManager.UserBoard.mouseManager.OnTileSelect += SiloSelectionResponse;
    }

     // Assignment Variable for number of silo to place
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
            silos.Add(playerBoard.GetTile(x, y).GetStruct() as MissileSilo);
            silo --;
            //tile.SetDiscover(true);
            //WorldRenderer.Render(this.playerBoard);
        }
        else
        {
            UserDisplay.DisplayToPlayer("Silo already exists in this location");
        }

        if(silo<=0)
        {
            WorldRenderManager.UserBoard.mouseManager.OnTileSelect -= SiloSelectionResponse;
            ready_up();
        }
    }
    
    /* Fire the missile by registering to the opponents Board's Missile manager. If more fields/helper functions are needed,
     * create them
     * Also, make sure to unsubscribe (-=) once enough missiles have been fired
     */
    public void RegisterMissile(BoardTile tile)
    {
        enemyBoard.GetMissileManager().RegisterMissile(tile.GetX(), tile.GetY(), silos[siloIndex]);

        siloIndex++;

        while(siloIndex < silos.Count && !silos[siloIndex].Can_Fire_Missile())
        {
            siloIndex++;
        }

        if (siloIndex >= silos.Count)
        {
            siloIndex = 0;
            WorldRenderManager.EnemyBoard.mouseManager.OnTileSelect -= RegisterMissile;
            end_decision();
        }
    }
}
