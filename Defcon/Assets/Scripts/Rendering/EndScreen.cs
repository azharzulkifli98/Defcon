using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public static string message;
    public static string EndSceneName = "EndScene";
    public string ContinueScene = "MenuScene";

    public static void Load()
    {
        SceneManager.LoadScene(EndSceneName);
    }

    [SerializeField]
    Text text = null;
    bool isDisplaying;


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
        if (message == null)
            message = text.text;
        text.text = "_";
        textTime = 0;
        textIndex = 0;
        blinkTime = 0;
        blinkOn = true;
        isDisplaying = true;
    }

    private void Update()
    {
        if (!isDisplaying)
            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene(ContinueScene);


        bool doUpdate = false;
        textTime += Time.deltaTime;
        blinkTime += Time.deltaTime;
        if (textTime > textSpeed)
        {
            textTime -= textSpeed;
            if (textIndex < message.Length)
            {
                textIndex++;
                doUpdate = true;
            }
            else
            {
                isDisplaying = false;
            }
        }
        if (blinkTime > blinkSpeed)
        {
            blinkTime -= blinkSpeed;
            blinkOn = !blinkOn;
            doUpdate = true;
        }
        if (doUpdate)
        {
            text.text = message.Substring(0, textIndex);
            if (blinkOn)
                text.text += "_";
        }
    }
}
