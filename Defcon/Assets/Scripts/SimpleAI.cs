using System.Collections;
using System.Collections.Generic;

public class SimpleAI : Player
{
    public override void make_decision(Board playerBoard, Board enemyBoard)   
    {
        //check all tiles on personal board for available silos
        List<MissileSilo> allSilos = playerBoard.GetAllSilos();

        BoardTile target = enemyBoard.GetTile(0, 0);

        // shoot a missile from each available silo, based on highest population
        // Issue --- need to make sure that the same place isn't shot at multiple times -Pamela
        for(int k = 0; k < allSilos.Count; k++){
            // COMPILER ISSUE --- check to make sure silo isn't empty or destroyed
            // RESOLUTION ---- Fire_Missile will do this naturally. If it's false, then we can't fire
            // from that silo. Otherwise, we can. Azhar may still be updating with final functionality
            if(allSilos[k].Fire_Missile())
            {
                for(int i = 0; i < 10; i++){
                    for(int j = 0; j < 10; j++){
                        if (target.GetPopulation() < enemyBoard.GetTile(i,j).GetPopulation())
                            target = enemyBoard.GetTile(i,j);
                    }
                }
                enemyBoard.GetMissileManager().RegisterMissile(target.GetX(), target.GetY());
            }
        }
        end_decision();
        return;
    }

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