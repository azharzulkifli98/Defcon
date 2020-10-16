using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSilo : Structure
{
    int missile_count;


    public MissileSilo(int given_x, int given_y)
    {
        this.x = given_x;
        this.y = given_y;
    }

    public override string GetID()
    {
        return "Missile_Silo";
    }

    // get number of missiles in the silo
    public int GetRemainingMissiles()
    {
        return missile_count;
    }

    // Decrement missile count and return true if missile_count > 0
    // otherwise, return false and do nothing
    public bool Fire_Missile()
    {
        throw new System.NotImplementedException();
    }
}