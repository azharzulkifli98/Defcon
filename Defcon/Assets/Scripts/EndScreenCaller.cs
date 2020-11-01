using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EndScreenCaller
{
    static string message;

    public static string GetMessage()
    {
        return message;
    }

    public static void CallEndScreen(string message)
    {
        EndScreenCaller.message = message;

        //TODO: Load Scene with EndScreenRenderer
    }
}
