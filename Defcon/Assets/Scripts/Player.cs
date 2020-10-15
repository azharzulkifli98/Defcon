using UnityEngine;

public abstract class Player //abstract denotes that this class has abstract functions and cannot be instantiated as a complete object+
{
    Board playerBoard = null;

    Board enemyBoard = null;
    
    /// <summary>
    ///complete actions to signal beginning of turn
    /// </summary>
    public abstract void make_decision();  //abstract denotes that the function need not be implemented

    /// <summary>
    /// complete actions to signal end of turn
    /// </summary>
    public abstract void end_decision();

}