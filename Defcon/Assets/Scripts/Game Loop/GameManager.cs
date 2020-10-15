using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private Board b1;
    private Board b2;
    private Player p1;
    private Player p2;



    public void StartGame(Player p1,Board b1,Player p2, Board b2)
    {

        //Generate boards and populations (boardmanager)-constructor
        //Start game will call building choice for each player
        //Function will then yield to main gameplay loop
    }

    public void StartTurn(Player player)
    {
        //find player board (render)
        //p1.makedecision()
        //find if action can be taken (not then call yield)
        //Prompt action
    }


    public void YieldTurn(Player player)
    {
        //Return end decision to prompt action

    }
    public void NextRound()
    {
        //checks for errors
        //checks for endgame conditions
        // call EndGame() if condition is met
    }



    /* Instead of prepping functions, I actually think this will be easier
     * for you guys with a walkthrough. Here we go!
     * 
     * First, you will need at least 4 member variables. 2 players and 2 boards.
     * 
     * You need a StartGame(Player p1, Board b1, Player p2, Board b2) function that starts your game loop.
     * 
     * StartTurn(Player player)
     * {
     *      find player's board
     *      find player's opponents board
     *      
     *      call the MissileManager on the player's board, and increment the turn
     *      call player.StartTurn(playerBoard, enemyBoard)
     * }
     * 
     * YieldTurn(Player player)
     * {
     *      check if there is still a game to play. Does either player have moves? Is a turn limit over?
     *      
     *      If there is a game to play, call StartTurn(other_player)
     * }
     * 
     * 
     * I'd advise a lot of helper functions and a ton of documentation
     */
}


// int main()
// {
//     //Create game manager object
//     //start game
//     //loop
//     //start turn{
//         //find/display player board
//         //if action not taken (return)
//         //prompt ()
//         //}
//     //Same but for P2

//     //end loop if(xyz)

// }