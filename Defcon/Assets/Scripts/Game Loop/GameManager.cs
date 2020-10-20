﻿using System.Collections;
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
            Debug.Log("player1 turn start");
            player.make_decision(b1, b2);
        }
        else
        {
            Debug.Log("player2 turn start");
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
            Debug.Log("player1 yield");
            // MissileManager.UpdateLaunches()
            // MissileManager.GetLandingMissiles()
            StartTurn(p2);
            NextRound();   
        }
        else if (player == p2)
        {
            Debug.Log("player2 yield");
            StartTurn(p1);
            NextRound();
        }
    }

    public static void NextRound()
    {
        // TODO check for errors
        
        // End the game when a player is out of missiles
        if (b2.GetAllSilos().All(silo => !silo.Can_Fire_Missile()))
            EndGame(p1);
        if (b1.GetAllSilos().All(silo => !silo.Can_Fire_Missile()))
            EndGame(p2);
    }

    static void EndGame(Player winner){
        SceneManager.LoadScene(EndSceneName);
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
