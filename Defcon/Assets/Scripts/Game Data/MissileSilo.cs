using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSilo : Structure
{
    int missile_count;

    Vector3 location;

    public MissileSilo(int given_x, int given_y, Vector3 location)
    {
        x = given_x;
        y = given_y;


        this.location = location;

        missile_count = 6;
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

    public bool Can_Fire_Missile()
    {
        return missile_count > 0 && !IsDestroyed();
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

    public Vector3 GetLocation()
    {
        return location;
    }
}