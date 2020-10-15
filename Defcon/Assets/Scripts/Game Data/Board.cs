using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Board
{
    BoardTile[,] tiles;

    MissileManager missileManager;

    list<City> AllCities = new list<City>();

    list<MissileSilo> AllSilos = new list<MissileSilo>();

    // Default the tiles to a 10 x 10 array of tiles, with pseudorandom distribution
    // Default the missileManager
    public Board()
    {
        // gives an inconsistent total population for now
        /*for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                // lets get zappy
                tiles[i, j] = new BoardTile(X, y, z);
            }
        }

        // pick 3 cities
        for (int k = 0; k < 3; k++)
        {
            int randx = Random.Range(0, 9);
            int randy = Random.Range(0, 9);
            AllCities.Add(new City(randx, randy));
        }*/
    }


    // return the specified board tile
    public BoardTile GetTile(int given_x, int given_y)
    {
        return BoardTile[given_x, given_y];
    }


    //************************ TODO
    // GetMissileManager()


    // Get All MissileSilos, add them to list
    public list<MissileSilo> GetAllSilos()
    {
        return AllSilos;
    }

    // Get All Cities, add them to list
    public list<MissileSilo> GetAllSilos()
    {
        return AllCities;
    }
}
