using System.Collections.Generic;
using UnityEngine;

public abstract class Player //abstract denotes that this class has abstract functions and cannot be instantiated as a complete object+
{
    public Board playerBoard;

    /// <summary>
    ///complete actions to signal beginning of turn
    /// </summary>
    public abstract void make_decision(Board playerBoard, Board enemyBoard);  //abstract denotes that the function need not be implemented

    /// <summary>
    /// complete actions to signal end of turn
    /// </summary>
    public void end_decision()
    {
        
        GameManager.YieldTurn(this);
    }
    public void set_player_board(Board playerBoard)
    {
        this.playerBoard=playerBoard;
    }


    /// <summary>
    /// Player sets their own silos on the board
    /// </summary>
    /// <param name="board"></param>
    public abstract void set_silos();

    public void ready_up()
    {
        GameManager.PlayerReady(this);
    }
}