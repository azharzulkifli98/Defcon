using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSilo : Structure
{
    int missile_count;


    public MissileSilo(int given_x, int given_y)
    {
        x = given_x;
        y = given_y;

        missile_count = 5;
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
	if (missile_count > 0 && !IsDestroyed())
        {
            missile_count--;
            return true;
        }
        else
        {
            return false;
        }
    }
}