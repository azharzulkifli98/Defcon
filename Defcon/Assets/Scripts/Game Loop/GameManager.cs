using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this object handles all the actions that take place over the game
/// 
/// before game starts
///     Start -> StartGame -> set_player_board -> set_silos -> PlayerReady
/// during game
///     PlayerReady -> StartTurn 
///     StartTurn -> make_decision -> YieldTurn -> StartTurn/NextRound
///     NextRound -> EndGame
/// </summary>
public class GameManager : MonoBehaviour
{
    private static Board b1;
    private static Board b2;
    private static Player p1;
    private static Player p2;

    private static bool p1_ready;
    private static bool p2_ready;

    private void Awake()
    {
        StartGame(new UserPlayer(), new Board(), new SimpleAI(), new Board());
    }

    public static void StartGame(Player p1, Board b1, Player p2, Board b2)
    {
        Board.OnWorldUpdate += WorldRenderManager.UpdateRender;


        //No player are ready
        p1_ready = false;
        p2_ready = false;
        //Possibly useless
        GameManager.p1 = p1;
        GameManager.b1 = b1;
        GameManager.p2 = p2;
        GameManager.b2 = b2;

        WorldRenderManager.RenderUser(b1);
        WorldRenderManager.RenderEnemy(b2);

        //Set the boards
        p1.set_player_board(b1);
        p2.set_player_board(b2);
        //Call for choosing silos
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
        //Next turn basically
        if (player == p1)
        {
            b2.GetMissileManager().UpdateLaunches();

            foreach (MissileManager.MissileLaunch launch in b2.GetMissileManager().GetLandingMissiles())
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



    // TODO: Impliment score keeping system, Determine winner using previous board
    // Score tabulation must be based on the initial board configurations
    static void EndGame()
    {
        // Score keeping system: uses a percentage system instead of a number of kills
        int killsPlayer1 = p1.playerBoard.GetInitialPopulation() - p1.playerBoard.GetTotalPopulation();
        int killsPlayer2 = p2.playerBoard.GetInitialPopulation() - p2.playerBoard.GetTotalPopulation();

        float percentPopPlayer1 = ((float)killsPlayer1) / p1.playerBoard.GetInitialPopulation();
        float percentPopPlayer2 = ((float)killsPlayer2) / p2.playerBoard.GetInitialPopulation();

        if (percentPopPlayer1 > percentPopPlayer2)
        {
            EndScreen.message = "PLAYER 1 HAS WON\n";
            Debug.Log("Player 1 has won");
            Debug.Log(percentPopPlayer1);
        }
        else
        {
            EndScreen.message = "PLAYER 2 HAS WON\n";
            Debug.Log("Player 2 has won");
            Debug.Log(percentPopPlayer2);
        }

        EndScreen.message += "PLAYER 1 STATISTICS:\n"
                           + $"  KILLS: {killsPlayer1,6}\n"
                           + $"         {percentPopPlayer1,8:P2}\n"
                           + "PLAYER 2 STATISTICS:\n"
                           + $"  KILLS: {killsPlayer2,6}\n"
                           + $"         {percentPopPlayer2,8:P2}\n";

        EndScreen.Load();
    }
}
