public class SimpleAI : Player
{
    public override void make_decision()   ///The override denotes that this function is an implementation of an abstract function
    {
        //check all tiles on personal board for available silos 
        //check all tiles on enemy board for highest population
        //make sure it doesn't fire all three missiles at the same place
        //FIRE Missiles through the missile manager
        //if no missiles left, end_decision
        //if missiles are fired, end_decision
        throw new System.NotImplementedException();
    }

    public override void end_decision()
    {
        //actions for simple AI to start turn
        throw new System.NotImplementedException();
    }
}