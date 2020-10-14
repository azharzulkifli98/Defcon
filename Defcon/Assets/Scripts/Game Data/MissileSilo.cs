using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSilo : Structure
{
    int missile_count;

    public override string GetID()
    {
        return "Missile_Silo";
    }

    // Decrement missile count and return true if missile_count > 0
    // otherwise, return false and do nothing
    public bool Fire_Missile()
    {
        throw new System.NotImplementedException();
    }
}