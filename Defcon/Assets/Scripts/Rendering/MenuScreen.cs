using System;
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
    bool isDisplaying;
    int consoleState;
    Action callback;


    [SerializeField]
    Text text = null;


    [SerializeField]
    [Tooltip("How many seconds to wait between displaying characters")]
    float textSpeed = 0.05f;
    float textTime;
    int textIndex;

    [SerializeField]
    [Tooltip("How many seconds between cursor blinks")]
    float blinkSpeed = 0.5f;
    float blinkTime;
    bool blinkOn;

    private void Start()
    {
        textBuffer = "SHALL WE PLAY A GAME?\n - DEFCON\n - EXIT\n> ";
        text.text = "_";
        textTime = 0;
        textIndex = 0;
        blinkTime = 0;
        blinkOn = true;
        isDisplaying = true;
        consoleState = 0;
        callback = null;
    }

    private string Submit(string input)
    {
        if (input.ToUpper() == "EXIT")
        {
            callback = delegate { Debug.Log("Quitting..."); Application.Quit(); };
            return "GOODBYE.";
        }
        switch (consoleState)
        {
            case 0:
                switch (input.ToUpper())
                {
                    case "DEFCON":
                        callback = delegate { consoleState = 1; };
                        return "WILL YOU BE PLAYING?\n - YES\n - NO";
                    default:
                        return "I DON'T KNOW THAT GAME.";
                }
            case 1:
                switch (input.ToUpper())
                {
                    case "YES":
                        callback = delegate { consoleState = 2; };
                        return "HOW SMART IS YOUR OPPONENT?\n - SIMPLE\n - SMART";
                    case "NO":
                        callback = delegate { consoleState = 3; };
                        return "WHAT WOULD YOU LIKE TO WATCH?\n - SIMPLE VS SIMPLE\n - SIMPLE VS SMART\n - SMART VS SMART";
                    default:
                        return "ARE YOU PLAYING OR NOT?";
                };
            case 2:
                switch (input.ToUpper())
                {
                    case "SIMPLE":
                        callback = delegate { 
                            GameManager.setPlayer1(new UserPlayer());
                            GameManager.setPlayer2(new SimpleAI());
                            SceneManager.LoadScene(BeginScene); 
                            };
                        return "I'LL GO EASY ON YOU.";
                    case "SMART":
                        callback = delegate { 
                            GameManager.setPlayer1(new UserPlayer());
                            GameManager.setPlayer2(new SmartAI());
                            SceneManager.LoadScene(BeginScene);  
                            };
                        return "LETS PLAY.";
                    default:
                        return "IS YOUR OPPONENT SIMPLE OR SMART?";
                }
            case 3:
                switch (input.ToUpper())
                {
                    case "SIMPLE VS SIMPLE":
                        callback = delegate { 
                            GameManager.setPlayer1(new SimpleAI());
                            GameManager.setPlayer2(new SimpleAI());
                            SceneManager.LoadScene(BeginScene); 
                            };
                        return "LET'S WATCH THE SHOW.";
                    case "SIMPLE VS SMART":
                        callback = delegate { 
                            GameManager.setPlayer1(new SimpleAI());
                            GameManager.setPlayer2(new SmartAI());
                            SceneManager.LoadScene(BeginScene); 
                            };
                        return "I THINK YOU CAN PREDICT THIS ONE.";
                    case "SMART VS SMART":
                        callback = delegate { 
                            GameManager.setPlayer1(new SmartAI());
                            GameManager.setPlayer2(new SmartAI());
                            SceneManager.LoadScene(BeginScene);  
                            };
                        return "SMART CHOICE.";
                    default:
                        return "WHAT WOULD YOU LIKE TO WATCH?";
                }
            default:
                consoleState = 0;
                return "INTERNAL ERROR.\nSHALL WE PLAY A GAME?\n - DEFCON\n - EXIT";
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
                if (callback != null)
                {
                    callback();
                    callback = null;
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

}
