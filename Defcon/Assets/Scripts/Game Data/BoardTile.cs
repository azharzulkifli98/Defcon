using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Perform all functionality related to a direct hit (missile hits this target directly). Anthony's document will specify specifics
    public void OnDirectHit()
    {
        population = 0;
        if (structure != null)
        {
            structure.Destroy();
        }
        
    }

    // Perform all funcitonality related to a glancing hit (missile hits adjacent tile). Anthony's document will specify specifics
    public void OnIndirectHit()
    {
        population = population / 2;
    }

    // Structure needs getter
    public Structure getStruct()
    {
        return structure;
    }
}
