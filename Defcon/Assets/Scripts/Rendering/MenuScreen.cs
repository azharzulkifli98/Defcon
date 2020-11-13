using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public static string BeginScene = "SampleScene";

    string textBuffer;
    string inputBuffer;
    bool loadGame;
    bool isDisplaying;

    [SerializeField]
    Text text;


    [SerializeField]
    [Tooltip("How many seconds to wait between displaying characters")]
    float textSpeed = 0.1f;
    float textTime;
    int textIndex;

    [SerializeField]
    [Tooltip("How many seconds between cursor blinks")]
    float blinkSpeed = 0.5f;
    float blinkTime;
    bool blinkOn;

    private void Start()
    {
        textBuffer = "SHALL WE PLAY A GAME?\n"
                   + " - DEFCON\n"
                   + " - EXIT\n"
                   + "> ";
        if (textBuffer == null)
            textBuffer = text.text + "\n> ";
        text.text = "_";
        textTime = 0;
        textIndex = 0;
        blinkTime = 0;
        blinkOn = true;
        isDisplaying = true;
        loadGame = false;
    }

    private string Submit(string input)
    {
        switch (input.ToUpper())
        {
            case "DEFCON":
                loadGame = true;
                return "LETS PLAY.";
            case "EXIT":
                Debug.Log("Exit");
                StartCoroutine(WaitThenQuit());
                return "GOODBYE.";
            default:
                return "I DON'T KNOW THAT GAME.";
        };
    }

    private void Update()
    {
        bool doUpdate = false;

        if (!isDisplaying)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                textBuffer += inputBuffer + '\n';
                textIndex += inputBuffer.Length;
                textBuffer += Submit(inputBuffer) + "\n> ";
                inputBuffer = "";
                isDisplaying = true;
            }
            else if (Input.inputString.Contains("\b"))
            {
                Debug.Log("Backspace");
                if (inputBuffer.Length > 0)
                    inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);
                doUpdate = true;
            }
            else
            {
                inputBuffer += Input.inputString;
                doUpdate = true;
            }
        }

        textTime += Time.deltaTime;
        if (textTime > textSpeed)
        {
            textTime -= textSpeed;
            if (textIndex < textBuffer.Length)
            {
                textIndex++;
                doUpdate = true;
            }
            else
            {
                if (loadGame)
                {
                    Debug.Log("Loading game");
                    SceneManager.LoadScene(BeginScene);
                }
                isDisplaying = false;
            }
        }

        blinkTime += Time.deltaTime;
        if (blinkTime > blinkSpeed)
        {
            blinkTime -= blinkSpeed;
            blinkOn = !blinkOn;
            doUpdate = true;
        }

        if (doUpdate)
        {
            text.text = textBuffer.Substring(0, textIndex) + inputBuffer;
            if (blinkOn)
                text.text += "_";
        }
    }

    public IEnumerator WaitThenQuit()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
        Debug.Log("Application Ended");
    }
}
