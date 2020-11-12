using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Missile Manager registeres and remembers missiles that are fired at a board
/// Author: Ethan Galloway
/// </summary>
public class MissileManager //Missile manager inherits from nothing
{
    /// <summary>
    /// The number of turns until the missile lands
    /// </summary>
    const int MISSILE_LIFE_TIME = 1;

    /// <summary>
    /// The count value when a missile has been detected
    /// </summary>
    const int DETECTED_COUNT = 1;

    /// <summary>
    /// Stores data about the timing and targeted location of each missile launch 
    /// </summary>
    public class MissileLaunch
    {
        public int x;

        public int y;

        public int missileCounter;

        public MissileLaunch(int x, int y)
        {
            this.x = x;
            this.y = y;
            missileCounter = MISSILE_LIFE_TIME;
        }
    }

    /// <summary>
    /// The list is initialized to an empty list at the start of the code running
    /// </summary>
    List<MissileLaunch> missileLaunches = new List<MissileLaunch>();

    /// <summary>
    /// Registers a new missile launch on this MissileManager's board, to land
    /// MISSILE_LIFE_TIME turns from now
    /// </summary>
    /// <param name="x">The x coordinate of the tile we land on</param>
    /// <param name="y">The y coordinate of the tile we land on</param>
    public void RegisterMissile(int x, int y)
    {
        missileLaunches.Add(new MissileLaunch(x, y));
    }

    /// <summary>
    /// Updates the missile launches. Should be called when the targeted player's turn begins
    /// </summary>
    public void UpdateLaunches()
    {
        for (int i = 0; i < missileLaunches.Count; i++)
        {
            missileLaunches[i].missileCounter--;

            if (missileLaunches[i].missileCounter < 0)
            {
                missileLaunches.Remove(missileLaunches[i]);
                i--;
            }
        }
    }

    /// <summary>
    /// Returns all missiles landing this turn
    /// </summary>
    public List<MissileLaunch> GetLandingMissiles()
    {
        List<MissileLaunch> landing = new List<MissileLaunch>();

        foreach (MissileLaunch launch in missileLaunches)
        {
            if (launch.missileCounter == 0)
            {
                landing.Add(launch);
            }
        }

        return landing;
    }

    /// <summary>
    /// Returns all missiles detected in this turn
    /// </summary>
    public List<MissileLaunch> GetMissilesInDetection() 
    {
        List<MissileLaunch> detected = new List<MissileLaunch>();

        foreach (MissleLaunch launch in missileLaunches)
        {
            if (launch.missileCounter == DETECTED_COUNT)
            {
                detected.Add(launch);
            }
        }

        return detected;
    }

    public bool NoMissiles()
    {
        return missileLaunches.Count == 0;
    }
}


