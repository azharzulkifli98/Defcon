using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object is used for holding all the data the player needs to know about the board
/// Both players will have one initialized at start of game
/// </summary>
public class Board
{
    BoardTile[,] tiles = null;

    // will add up when tiles[,] is filled
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
        int city1 = 0;
        int city2 = 0;
        int city3 = 0;
        bool overlappingcities = true;
        while (overlappingcities)
        {
            overlappingcities = false;
            city1 = r.Next(0, 99);
            city2 = r.Next(0, 99);
            city3 = r.Next(0, 99);
            if (city1 == city2 || city2 == city3 || city1 == city3)
            {
                overlappingcities = true;
            }
        }

        int population;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // checks each city individually, might need list for 5 cities
                // In order to make cities more enticing, I increased the possible populations
                // Is there any other way to incentivize hitting cities? Like increasing populations 
                // of the tiles around a city (like suburbs) or is that too much?
                int spot = (i * 10) + j;
                if (spot == city1 || spot == city2 || spot == city3)
                {
                    City c = new City(i, j);
                    AllCities.Add(c);
                    population = r.Next(25, 35);
                    tiles[i, j] = new BoardTile(i, j, population, c);
                    tiles[i,j].SetDiscover(true);
                    total_population = total_population + population;
                }
                else
                {
                    population = r.Next(1, 15);
                    tiles[i, j] = new BoardTile(i, j, population);
                    total_population = total_population + population;
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

    // gives population of the whole board
    public int GetTotalPopulation()
    {
        BoardTile temp;
        int population=0;
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                temp=tiles[i,j];
                population+=temp.GetPopulation();

            }
        }
        return population;
    }

    public int GetInitialPopulation()
    {
        BoardTile temp;
        int population=0;
        for(int i=0;i<10;i++)
        {
            for(int j=0;j<10;j++)
            {
                temp=tiles[i,j];
                population+=temp.GetInitPopulation();

            }
        }
        return population;
    }

    // returns a reference to the missile manager object
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
    public int GetWidth()
    {
        return tiles.GetLength(0);
    }

    /// <summary>
    /// Returns the Y dimension of this board
    /// </summary>
    public int GetHeight()
    {
        return tiles.GetLength(1);
    }

    // handles missile hit for direct tile and surrounding tiles
    public void Impact(int x, int y)
    {
        // check all surrounding tiles in loop
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                // is this a valid tile
                if (0 <= i && i < GetWidth() && j >= 0 && j < GetHeight())
                {
                    // is this tile a direct hit
                    if (i == x && j == y)
                    {
                        tiles[i, j].OnDirectHit();
                        tiles[i, j].SetDiscover(true);
                    }
                    else
                    {
                        tiles[i, j].OnIndirectHit();
                        tiles[i, j].SetDiscover(true);
                    }
                    
                }
            }
        }
    }


    public delegate void onWorldUpdate();

    public static event onWorldUpdate OnWorldUpdate;

    public static void UpdateWorld()
    {
        if (OnWorldUpdate != null)
        {
            OnWorldUpdate();
        }
    }
}
