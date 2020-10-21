using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string EndSceneName = "EndScene";
    private static Board b1;
    private static Board b2;
    private static Player p1;
    private static Player p2;

    private static bool p1_ready;
    private static bool p2_ready;

    private static int b1InitPop;
    private static int b2InitPop;




    private void Start()
    {
        StartGame(new SimpleAI(), new Board(), new UserPlayer(), new Board());
    }

    public static void StartGame(Player p1, Board b1, Player p2, Board b2)
    {
        p1_ready = false;
        p2_ready = false;
        GameManager.p1 = p1;
        GameManager.b1 = b1;
        GameManager.p2 = p2;
        GameManager.b2 = b2;
        GameManager.b1InitPop=b1.GetTotalPopulation();
        GameManager.b2InitPop=b2.GetTotalPopulation();
        p1.set_player_board(b1);
        p2.set_player_board(b2);
        p1.set_silos();
        p2.set_silos();
    }

    public static void PlayerReady(Player player)
    {
        if (player == p1)
        {
            p1_ready = true;
        }
        else if (player == p2)
        {
            p2_ready = true;
        }
        if (p1_ready && p2_ready)
        {
            StartTurn(p1);
        }
    }

    public static void StartTurn(Player player)
    {
        //find player board (render)
        if (player == p1)
        {
            player.make_decision(b1, b2);
        }
        else
        {
            player.make_decision(b2, b1);
        }
    }

    /// <summary>
    /// TODO: END GAME INTEGRATION/NEXT ROUND INTEGRATION
    /// </summary>
    /// <param name="player"></param>
    public static void YieldTurn(Player player)
    {
        if (player == p1)
        {
            b2.GetMissileManager().UpdateLaunches();
            
            foreach(MissileManager.MissileLaunch launch in b2.GetMissileManager().GetLandingMissiles())
            {
                b2.Impact(launch.x, launch.y);
            }

            StartTurn(p2);
            NextRound();   
        }
        else if (player == p2)
        {
            b1.GetMissileManager().UpdateLaunches();

            foreach (MissileManager.MissileLaunch launch in b1.GetMissileManager().GetLandingMissiles())
            {
                Debug.Log("Landing at " + launch.x + ", " + launch.y);
                b1.Impact(launch.x, launch.y);
            }
            StartTurn(p1);
            NextRound();
        }
    }

    public static void NextRound()
    {
        // TODO check for errors
        
        // End the game when a player is out of missiles
        //If all silos cannot fire missile and no missile are in the air
        if (b2.GetAllSilos().All(silo => !silo.Can_Fire_Missile()) && b1.GetMissileManager().NoMissiles())
            EndGame();
        if (b1.GetAllSilos().All(silo => !silo.Can_Fire_Missile()) && b2.GetMissileManager().NoMissiles())
            EndGame();
    }



    //TO DO: Impliment score keeping system, Determine winner using previous board
//Score tabulation must be based on the initial board configurations
    static void EndGame(){
        SceneManager.LoadScene(EndSceneName);
        int Killp1= b1InitPop-p1.playerBoard.GetTotalPopulation();
        int Killp2=b2InitPop-p2.playerBoard.GetTotalPopulation();
        if (Killp1>Killp2)
        {
            Debug.Log("Player 1 has won");
            Debug.Log(Killp1);
        }
        else
        {
            Debug.Log("Player 2 has won");
            Debug.Log(Killp2);
        }
        Debug.Log(Killp1);
        
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
