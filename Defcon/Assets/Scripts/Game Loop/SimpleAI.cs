using System.Collections;
using System.Collections.Generic;

public class SimpleAI : Player
{
    /// <summary>
    ///make decision override for SimpleAI
    /// </summary>
    public override void make_decision(Board playerBoard, Board enemyBoard)   
    {
        // holds all silos available to player
        List<MissileSilo> allSilos = playerBoard.GetAllSilos();

        // keeps a list of which tiles have been hit
        HashSet<BoardTile> hitTiles = new HashSet<BoardTile>(); 

        // stores enemy target tile, defaulted to 0,0
        BoardTile target = enemyBoard.GetTile(0, 0);

        // shoot a missile from each available silo, random
        System.Random r = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue));
        for (int k = 0; k < allSilos.Count; k++){
            if(allSilos[k].Fire_Missile())
            {
                // make it target a random tile each time
                target = enemyBoard.GetTile(r.Next(0, 9), r.Next(0, 9));
                hitTiles.Add(target);
                enemyBoard.GetMissileManager().RegisterMissile(target.GetX(), target.GetY());
            }
        }
        end_decision();
        return;
    }

    // random setting of silos
    public override void set_silos()
    {
        int placed = 0;
        int j = 0;
        while (placed < 3)
        {
            // avoid placing on cities or radars
            if (this.playerBoard.GetTile(0, j).GetStruct() == null)
            {
                this.playerBoard.SetMissileSilo(0, j);
                placed++;
            }
            j++;
        }
        ready_up();
    }
}