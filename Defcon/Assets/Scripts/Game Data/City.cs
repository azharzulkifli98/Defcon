using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object used to keep track of city locations on the board and their population value
/// </summary>
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
