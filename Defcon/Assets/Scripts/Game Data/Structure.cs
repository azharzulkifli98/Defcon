﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure
{
    bool is_destroyed = false;

    protected int x;
    protected int y;

    /// <summary>
    /// Returns a string to tell us the type of a structure
    /// </summary>
    /// <returns></returns>
    public abstract string GetID();

    // does this structure still exist
    public bool IsDestroyed()
    {
        return is_destroyed;
    }

    // set structure status to destroyed
    public void Destroy()
    {
        Debug.Log("The structure " + GetID() + " was destroyed.");
        is_destroyed = true;
    }
}
