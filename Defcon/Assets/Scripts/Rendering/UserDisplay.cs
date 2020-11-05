using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDisplay : MonoBehaviour
{
    [SerializeField]
    Text text = null;

    public delegate void display(string to_display);
    public static event display Display;

    private void Awake()
    {
        Display += DisplayText;
    }

    public void DisplayText(string text)
    {
        this.text.text = text;
    }

    public static void DisplayToPlayer(string text)
    {
        if(Display != null)
        {
            Display(text);
        }
    }
}
