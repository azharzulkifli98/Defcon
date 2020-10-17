using System.Collections;
using System.Collections.Generic;

public class SimpleAI : Player
{
    public override void make_decision()   ///The override denotes that this function is an implementation of an abstract function
    {
        //check all tiles on personal board for available silos
        List<MissileSilo> allSilos = this.playerBoard.GetAllSilos();
        // ***Q: Can I assume all missles are NOT destroyed?***

        if (allSilos.Count == 0){
            // No missiles, end decision with no firing to do!
            this.end_decision();
            return;
        }

        // Does target default to 0,0 ? -Azhar
        BoardTile target = enemyBoard.GetTile(0, 0);

        BoardTile target_one = enemyBoard.GetTile(0,0);
        //check all tiles on enemy board for highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target_one.GetPopulation() < enemyBoard.GetTile(i,j).GetPopulation())
                    target_one = enemyBoard.GetTile(i,j);
            }
        }
        
        BoardTile target_two = enemyBoard.GetTile(0,0);
        if(target_one == target_two){
            target_two = enemyBoard.GetTile(0,1);
        }
        //check all tiles on enemy board for second-highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target.GetPopulation() < enemyBoard.GetTile(i,j).GetPopulation())
                    if (target_one != enemyBoard.GetTile(i,j))
                        target = enemyBoard.GetTile(i,j);
            }
        }

        BoardTile target_three = enemyBoard.GetTile(0,0);
        //***this logic needs cleaned up***
        if(target_one == target_three || target_two == target_three){
            target_three = enemyBoard.GetTile(0,2);
        }
        //check all tiles on enemy board for third highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target.GetPopulation() < enemyBoard.GetTile(i,j).GetPopulation())
                    if(target_one != enemyBoard.GetTile(i,j))
                        target = enemyBoard.GetTile(i,j);
            }
        }
    
        //FIRE Missiles through the missile manager
        //Not sure how to do this?

        this.end_decision();
        return;
    }

    public override void end_decision()
    {
        throw new System.NotImplementedException();
    }

    public override void set_silos(Board board)
    {
        board.SetMissileSilo(0, 0);
        board.SetMissileSilo(0, 1);
        board.SetMissileSilo(0, 2);
        board.SetMissileSilo(0, 3);
        board.SetMissileSilo(0, 4);
    }
}