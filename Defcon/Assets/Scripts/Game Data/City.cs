using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class City : Structure
{

    public City(int given_x, int given_y)
    {
        this.x = given_x;
        this.y = given_y;
    }

    public override string GetID()
    {
        return "City";
    }
}
