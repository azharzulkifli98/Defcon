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

    private static GameManager singleton;

    private void Start()
    {
        if (singleton != null)
            Destroy(singleton);
        singleton = this;
        StartGame(p1, new Board(), p2, new Board());

    }

    public static void setPlayer1(Player p1)
    {
        GameManager.p1 = p1;
    }

    public static void setPlayer2(Player p2)
    {
        GameManager.p2 = p2;
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

        WorldRenderManager.UserBoard.mouseManager.OnTileHover += WorldRenderManager.WhenUserTileHover;
        WorldRenderManager.EnemyBoard.mouseManager.OnTileHover += WorldRenderManager.WhenEnemyTileHover;

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
            if (b1.CanFire())
                player.make_decision(b1, b2);
            else
                YieldTurn(p1);
        }
        else
        {
            if (b2.CanFire())
                player.make_decision(b2, b1);
            else
                YieldTurn(p2);
        }
    }

    /// <summary>
    /// TODO: END GAME INTEGRATION/NEXT ROUND INTEGRATION
    /// </summary>
    /// <param name="player"></param>
    public static void YieldTurn(Player player)
    {
        if (!NextRound())
            EndGame();
        else
        {
            //Next turn basically
            if (player == p1)
            {
                b2.GetMissileManager().UpdateLaunches();

                foreach (MissileManager.MissileLaunch launch in b2.GetMissileManager().GetLandingMissiles())
                {
                    b2.Impact(launch.x, launch.y);
                }

                Delay(p2);
            }
            else if (player == p2)
            {
                b1.GetMissileManager().UpdateLaunches();

                foreach (MissileManager.MissileLaunch launch in b1.GetMissileManager().GetLandingMissiles())
                {
                    Debug.Log("Landing at " + launch.x + ", " + launch.y);
                    b1.Impact(launch.x, launch.y);
                }
                Delay(p1);
            }
        }
    }


    /// <summary>
    /// Do we play the next round or end the game?
    /// </summary>
    /// <returns></returns>
    public static bool NextRound()
    {
        // TODO check for errors

        // End the game when a player is out of missiles
        // If all silos cannot fire missile and no missile are in the air
        if (!b2.CanFire() && b1.GetMissileManager().NoMissiles()
         && !b1.CanFire() && b2.GetMissileManager().NoMissiles())
            return false;
        return true;
    }

    private static void Delay(Player player)
    {
        singleton.StartCoroutine(singleton.DelayCoroutine(player));
    }

    private IEnumerator DelayCoroutine(Player player)
    {
        yield return new WaitForSeconds(.5f);
        while(MissileAnimator.IsAnimating())
        {
            yield return null;
        }
        StartTurn(player);
    }

    // TODO: Impliment score keeping system, Determine winner using previous board
    // Score tabulation must be based on the initial board configurations
    static void EndGame()
    {
        // Score keeping system: uses a percentage system instead of a number of kills
        int deadP1 = p1.playerBoard.GetInitialPopulation() - p1.playerBoard.GetTotalPopulation();
        int deadP2 = p2.playerBoard.GetInitialPopulation() - p2.playerBoard.GetTotalPopulation();

        float percentKillsP2 = ((float)deadP1) / p1.playerBoard.GetInitialPopulation();
        float percentKillsP1 = ((float)deadP2) / p2.playerBoard.GetInitialPopulation();

        if (percentKillsP2 < percentKillsP1)
        {
            EndScreen.message = "PLAYER 1 HAS WON\n";
            Debug.Log("Player 1 has won");
            Debug.Log(percentKillsP2);
        }
        else
        {
            EndScreen.message = "PLAYER 2 HAS WON\n";
            Debug.Log("Player 2 has won");
            Debug.Log(percentKillsP1);
        }

        EndScreen.message += "PLAYER 1 STATISTICS:\n"
                           + $"  KILLS: {deadP2,6}\n"
                           + $"         {percentKillsP1,8:P2}\n"
                           + "PLAYER 2 STATISTICS:\n"
                           + $"  KILLS: {deadP1,6}\n"
                           + $"         {percentKillsP2,8:P2}\n"
                           + "PRESS ENTER TO CONTINUE";
        EndScreen.Load();
    }
}
