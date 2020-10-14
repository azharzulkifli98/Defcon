﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure
{
    bool is_destroyed;

    int x;

    int y;

    /// <summary>
    /// Returns a string to tell us the type of a structure
    /// </summary>
    /// <returns></returns>
    public abstract string GetID();
}