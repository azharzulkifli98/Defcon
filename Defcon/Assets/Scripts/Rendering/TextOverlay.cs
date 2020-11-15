using System.Collections;
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
    public void OnTileHover(BoardTile tile, WorldRenderer.WorldRenderMode mode)
    {
        string text = "Hovering over " + tile.GetX() + " " + tile.GetY();
        if (mode == WorldRenderer.WorldRenderMode.Discovered)
        {
            text += "\nDiscovered";
            text += "\nPopulation:    " + tile.GetPopulation();
            if(tile.GetStruct() != null && tile.GetStruct().GetID() == "Missile_Silo")
            {
                text += "\nMissile Silo";
                text += "\n" + (tile.GetStruct().IsDestroyed() ? "Destroyed" : "Functional");
                if(!tile.GetStruct().IsDestroyed())
                {
                    text += "\n The Silo has " + (tile.GetStruct() as MissileSilo).GetRemainingMissiles() + " missiles left.";
                }
            }
        }
        else
        {
            text += "\nHidden information";
        }
        displayText.text = text;
    }

    /// <summary>
    /// On start, we need to subscribe OnTileHover to MouseManager.OnTileHover();
    /// </summary>
    private void Start()
    {
        WorldRenderManager.OnTileHover += OnTileHover;
    }
}
