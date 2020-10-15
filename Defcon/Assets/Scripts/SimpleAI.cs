public class SimpleAI : Player
{
    public override void make_decision()   ///The override denotes that this function is an implementation of an abstract function
    {
        //check all tiles on personal board for available silos
        list<MissileSilos> allSilos = this.playerBoard.GetAllSilos();
        // ***Q: Can I assume all missles are NOT destroyed?***

        if(allSilos.Count == 0){
            // No missiles, end decision with no firing to do!
            this.end_decision();
            return;
        }

        BoardTile target_one = enemyBoard.GetTile(0,0);
        //check all tiles on enemy board for highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target_one.population < enemyBoard.getTile(i,j).population)
                    target_one = enemyBoard.getTile(i,j);
            }
        }
        
        BoardTile target_two = enemyBoard.GetTile(0,0);
        if(target_one == target_two){
            target_two = enemyBoard.getTile(0,1);
        }
        //check all tiles on enemy board for second-highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target.population < enemyBoard.getTile(i,j).population)
                    if(target_one != enemyBoard.getTile(i,j))
                        target = enemyBoard.getTile(i,j);
            }
        }

        BoardTile target_three = enemyBoard.GetTile(0,0);
        //***this logic needs cleaned up***
        if(target_one == target_three || target_two == target_three){
            target_three = enemyBoard.getTile(0,2);
        }
        //check all tiles on enemy board for third highest population
        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                if (target.population < enemyBoard.getTile(i,j).population)
                    if(target_one != enemyBoard.getTile(i,j))
                        target = enemyBoard.getTile(i,j);
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
}