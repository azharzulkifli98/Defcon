﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOverlay : MonoBehaviour
{
    [SerializeField]
    Text displayText;

    /// <summary>
    /// Place the necessary information from tile onto displayText, and 
    /// also move the text to the necessary position on the screen
    /// </summary>
    /// <param name="tile"></param>
    public void OnTileHover(BoardTile tile)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// On start, we need to subscribe OnTileHover to MouseManager.OnTileHover();
    /// </summary>
    private void Start()
    {
        
    }
}