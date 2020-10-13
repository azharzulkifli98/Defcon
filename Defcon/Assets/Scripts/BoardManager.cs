using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Data for the entire board
    private int numCities;
    private int numSilos;
    private int numRadars;
    private int currPop;
    private int initPop;
    private GameObject[] cityLocations;
    private Gameobject[] siloLocations;
    private GameObject[] radarLocations;
    private int[,] popDistribution; // needs to be 10 x 10

    // Start is called before the first frame update
    void Start()
    {
        // build cities and set populations
    }

    // Update is called once per frame
    void Update()
    {
        // none
    }

    // gives the board population
    int getPopulation()
    {
        return currPop;
    }

    void setPopulation(int newPop)
    {
        currPop = newPop;
    }

    int[,] getPopulationDistribution()
    {
        return popDistribution;
    }

    void setPopulationDistributiion(int[,] newDistribution)
    {
        popDistribution = newDistribution;
    }
}
