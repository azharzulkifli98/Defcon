using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartAI : Player
{
    // General idea is to target spot with highest point gain
    // right now it just what the simple AI did

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
                        if (target.GetDiscovered())
                        {
                            if ((target.GetPopulation() < enemyBoard.GetTile(i, j).GetPopulation()) && !hitTiles.Contains(target))
                                target = enemyBoard.GetTile(i, j);
                        }
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


    // sets silos to be in range for each city
    public override void set_silos()
    {
        // need to set 3 missile silos
        for (int k = 0; k < 3; k++)
        {
            City c = this.playerBoard.GetAllCities()[k];
            
            for (int i = c.GetX() - 2; i < 3; i += 2)
            {
                for (int j = c.GetY() - 2; j < 3; j += 2)
                {
                    if (i >= 0 && i < this.playerBoard.GetWidth() && 
                        j >= 0 && j < this.playerBoard.GetHeight() && 
                        this.playerBoard.GetTile(i, j).GetStruct() == null) 
                    {
                        // found acceptable tile
                        this.playerBoard.SetMissileSilo(i, j);
                    }
                }
            }
        }
        ready_up();
    }
}
