using System.Collections.Generic;
using UnityEngine;

public abstract class Player //abstract denotes that this class has abstract functions and cannot be instantiated as a complete object+
{
    protected Board playerBoard = null;

    protected Board enemyBoard = null;
    
    /// <summary>
    ///complete actions to signal beginning of turn
    /// </summary>
    public abstract void make_decision();  //abstract denotes that the function need not be implemented

    /// <summary>
    /// complete actions to signal end of turn
    /// </summary>
    public abstract void end_decision();

    /// <summary>
    /// Player sets their own silos on the board
    /// </summary>
    /// <param name="board"></param>
    public abstract void set_silos(Board board);

    public void ready_up(Player player)
    {

    }
}