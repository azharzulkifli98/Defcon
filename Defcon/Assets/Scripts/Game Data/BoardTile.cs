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
    int init_population;
    int population;
    Structure structure;
//Discovery variable
    bool discovered=false;


    // Require specification of x, y, pop, and structure. Structure may be null. x, y, population are nonnegative
    public BoardTile(int given_x, int given_y, int given_pop = 1, Structure given_struct = null)
    {
        this.x = given_x;
        this.y = given_y;
        this.init_population = given_pop;
        this.population = given_pop;
        this.structure = given_struct;
    }

    // used for the AI and UI
    public int GetPopulation()
    {
        return population;
    }

    public int GetInitPopulation()
    {
        return init_population;
    }

    // Perform all functionality related to a direct hit (missile hits this target directly)
    //Shows the discover
    public void OnDirectHit()
    {
        population = 0;
        if (structure != null)
        {
            structure.Destroy();
        }
        this.discovered=true;
        Board.UpdateWorld();
    }

    // Perform all funcitonality related to a glancing hit (missile hits adjacent tile)
    // halfs the population each time and destroys structure when it hits zero
    //Discovers tile
    public void OnIndirectHit()
    {
        population /= 2;
        if (population == 0 && structure != null)
        {
            structure.Destroy();
        }
        this.discovered=true;
        Board.UpdateWorld();
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
            Board.UpdateWorld();
            return true;
        }
        else
        {
            Board.UpdateWorld();
            return false;
        }
    }

    // gives the x location of the tile
    public int GetX()
    {
        return x;
    }

    // gives the y location of the tile
    public int GetY()
    {
        return y;
    }

    //Discovery modifier 
    public void SetDiscover(bool selection)
    {
        this.discovered=selection;

        Board.UpdateWorld();
    }

    /// <summary>
    /// Returns true if the tile has been discovered so far, false otherwise
    /// </summary>
    /// <returns></returns>
    public bool GetDiscovered()
    {
        return this.discovered;
    }

    public override bool Equals(object obj)
    {
        if(obj.GetType() != typeof(BoardTile))
        {
            return false;
        }
        BoardTile tile = obj as BoardTile;
        return this.GetY() == tile.GetY() && this.GetX() == tile.GetX();
    }

    public override int GetHashCode()
    {
        return x * 13 + y * 3;
    }
}
