using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Object stored in Board.cs keeps track of each tile and its values
/// Also handles population hit calculations
/// </summary>
public class BoardTile
{
    int x;
    int y;

    int population;
    Structure structure;


    // Require specification of x, y, pop, and structure. Structure may be null. x, y, pop are nonnegative
    public BoardTile(int given_x, int given_y, int given_pop = 1, Structure given_struct = null)
    {
        this.x = given_x;
        this.y = given_y;
        this.population = given_pop;
        this.structure = given_struct;
    }

    // for AI and UI
    public int GetPopulation()
    {
        return population;
    }

    // Perform all functionality related to a direct hit (missile hits this target directly)
    public void OnDirectHit()
    {
        population = 0;
        if (structure != null)
        {
            structure.Destroy();
        }
    }

    // Perform all funcitonality related to a glancing hit (missile hits adjacent tile)
    public void OnIndirectHit()
    {
        population /= 2;
    }

    // Structure needs getter
    public Structure GetStruct()
    {
        return structure;
    }

    // Needed for adding Missile Silos
    public bool SetStruct(Structure given_struct)
    {
        if (this.structure == null)
        {
            this.structure = given_struct;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }
}
