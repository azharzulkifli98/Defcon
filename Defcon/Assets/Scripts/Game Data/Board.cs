using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    BoardTile[,] tiles = null;

    // with add up when tiles[,] is filled
    int total_population = 0;

    // deals with missiles aimed at board
    MissileManager missileManager;

    List<City> AllCities = new List<City>();

    List<MissileSilo> AllSilos = new List<MissileSilo>();

    // Default the tiles to a 10 x 10 array of tiles, with pseudorandom distribution
    public Board()
    {
        // Define the missile manager
        missileManager = new MissileManager();

        tiles = new BoardTile[10, 10];

        // need locations to be distinct
        System.Random r = new System.Random(UnityEngine.Random.Range(int.MinValue, int.MaxValue)) ;
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        bool dupes = true;
        while (dupes)
        {
            dupes = false;
            c1 = r.Next(0, 99);
            c2 = r.Next(0, 99);
            c3 = r.Next(0, 99);
            if (c1 == c2 || c2 == c3 || c1 == c3)
            {
                dupes = true;
            }
        }

        int pop;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // checks each city individually, might need list for 5 cities
                int spot = (i * 10) + j;
                if (spot == c1 || spot == c2 || spot == c3)
                {
                    City c = new City(i, j);
                    AllCities.Add(c);
                    pop = r.Next(1, 9);
                    tiles[i, j] = new BoardTile(i, j, pop, c);
                    total_population = total_population + pop;
                }
                else
                {
                    pop = r.Next(1, 9);
                    tiles[i, j] = new BoardTile(i, j, pop);
                    total_population = total_population + pop;
                }
            }
        }
    }


    // return the specified board tile
    public BoardTile GetTile(int given_x, int given_y)
    {
        return tiles[given_x, given_y];
    }

    // place a silo on tile location
    public void SetMissileSilo(int given_x, int given_y)
    {
        MissileSilo m = new MissileSilo(given_x, given_y);
        AllSilos.Add(m);
        tiles[given_x, given_y].SetStruct(m);
    }


    public MissileManager GetMissileManager()
    {
        return missileManager;
    }


    // Get All MissileSilos, add them to list
    public List<MissileSilo> GetAllSilos()
    {
        return AllSilos;
    }

    // Get All Cities, add them to list
    public List<City> GetAllCities()
    {
        return AllCities;
    }

    /// <summary>
    /// Returns the X dimension of this board
    /// </summary>
    /// <returns></returns>
    public int GetWidth()
    {
        return tiles.GetLength(0);
    }

    /// <summary>
    /// Returns the Y dimension of this board
    /// </summary>
    /// <returns></returns>
    public int GetHeight()
    {
        return tiles.GetLength(1);
    }

    public void Impact(int x, int y)
    {
        if(0 <= x && x < GetWidth() && y >= 0 && y < GetHeight())
        {
            tiles[x, y].OnDirectHit();                   
        }

        if (0 <= x + 1 && x + 1 < GetWidth() && y - 1 >= 0 && y - 1 < GetHeight())
        {
            tiles[x + 1, y - 1].OnIndirectHit();
        }
        if (0 <= x + 1 && x + 1 < GetWidth() && y >= 0 && y < GetHeight())
        {
            tiles[x + 1, y].OnIndirectHit();
        }
        if (0 <= x + 1 && x + 1 < GetWidth() && y + 1 >= 0 && y + 1< GetHeight())
        {
            tiles[x + 1, y + 1].OnIndirectHit();
        }

        if (0 <= x && x < GetWidth() && y - 1 >= 0 && y - 1 < GetHeight())
        {
            tiles[x, y - 1].OnIndirectHit();
        }
        if (0 <= x && x < GetWidth() && y + 1 >= 0 && y + 1 < GetHeight())
        {
            tiles[x, y + 1].OnIndirectHit();
        }

        if (0 <= x - 1 && x - 1 < GetWidth() && y - 1 >= 0 && y - 1 < GetHeight())
        {
            tiles[x - 1, y - 1].OnIndirectHit();
        }
        if (0 <= x - 1 && x - 1 < GetWidth() && y >= 0 && y < GetHeight())
        {
            tiles[x - 1, y].OnIndirectHit();
        }
        if (0 <= x - 1 && x - 1 < GetWidth() && y + 1 >= 0 && y + 1 < GetHeight())
        {
            tiles[x - 1, y + 1].OnIndirectHit();
        }
    }
}
