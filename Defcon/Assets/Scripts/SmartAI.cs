using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartAI : Player
{
    // General idea is to target spot with highest point gain

    // make decide whether it wants to :
    // fire a missile at a population
    // fire a missile at a structure
    // fire a missile to intercept
    // picks based off the highest calculated point value
    public override void make_decision(Board playerBoard, Board enemyBoard)   
    {
        // holds all silos available to player
        List<MissileSilo> allSilos = playerBoard.GetAllSilos();

        // keeps a list of which tiles have been hit
        HashSet<BoardTile> hitTiles = new HashSet<BoardTile>(); 

        // stores enemy target tile, defaulted to 0,0
        BoardTile target = enemyBoard.GetTile(0, 0);

        // shoot a missile from each available silo, based on highest population
        for(int k = 0; k < allSilos.Count; k++){
            if(allSilos[k].Fire_Missile())
            {
                for(int i = 0; i < 10; i++){
                    for(int j = 0; j < 10; j++){
                        if ((target.GetPopulation() < enemyBoard.GetTile(i,j).GetPopulation()) && !hitTiles.Contains(target))
                            target = enemyBoard.GetTile(i,j);
                    }
                }
                // this keeps track of which target is hit
                hitTiles.Add(target);
                enemyBoard.GetMissileManager().RegisterMissile(target.GetX(), target.GetY());
            }
        }
        end_decision();
        return;
    }


    // sets silos to be in range of cities
    public override void set_silos()
    {
        int placed = 0;
        while (placed < 3)
        {
            // avoid placing on cities or radars
            if (this.playerBoard.GetTile(0, j).GetStruct() == null)
            {
                this.playerBoard.SetMissileSilo(0, j);
                placed++;
            }
            j++;
            // might be good to consider where they are least likely to be targeted
        }
        ready_up();
    }
}
