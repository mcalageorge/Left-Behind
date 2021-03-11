using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ending : MonoBehaviour
{
    //array of texts
    public Text[] text;

    //counter for the text array
    int x = 0;

    //counter for the lines array
    int i = 0;

    //counter for the character inside array
    int j = 0;

    //array of sounds
    public AudioClip[] clips;

    //array of lines of the character
    public string[] replies;

    void Start()
    {
        ResetAll4Dialogues();
        line_started = false;
    }


    //this tells you if a line is being written out on the screen
    bool line_started = false;

    bool already = false;

    //this wil ensure that the first line of dialogue is automatically written out on the screen

    int started = 0;


    void Update()
    {
            switch (started)
            {
                case 0:
                    {
                        if (started == 0)
                        {
                            InvokeRepeating("ReadLines", 0.01f, 0.1f);
                            line_started = true;
                            started = 1;

                        }
                        break;
                    }
                case 1:
                    {
                        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)) && IsInvoking("ReadLines") == true)
                        {
                            SkipLine();

                        }

                        else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)) && IsInvoking("ReadLines") == false)
                        {
                            line_started = true;
                            InvokeRepeating("ReadLines", 0.01f, 0.1f);

                            if (x == 0) { ResetAll4Dialogues(); already = true; }

                        }
                        break;
                    }
                case 3:
                    {

                        FadeOut();
                        break;
                    }
            }
    }

    bool played = false;

    public void ReadLines()
    {
        char letter = ' ';
        // 
        if (i >= replies.Length) started = 3;
        //


        else
            letter = (replies[i])[j];

        Debug.Log(x + " is x");
        Debug.Log(i + " is i");
        Debug.Log(j + " is j");



        if (letter != '9')
            text[x].text += letter;
        if (letter != ' ')
            this.GetComponent<AudioSource>().PlayOneShot(clips[0], 1f);
        j++;

        if (letter == '9')
        {

            j = 0;
            i++;
            if (x < 3)
                x++;
            else x = 0;
            CancelInvoke("ReadLines");
        }

    }

    void ResetAll4Dialogues()
    {
        foreach (Text line in text)
        {
            line.text = "";
        }
        already = false;
    }

    public void SkipLine()
    {
        string help = replies[i];

        text[x].text = help.Replace('9', ' ');
        i++;
        if (x == 3) x = 0;
        else x++;
        j = 0;
        already = false;
        CancelInvoke("ReadLines");
    }

    public Image background;

    void FadeOut()
    {
        background.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / 3;
        if (background.GetComponent<CanvasGroup>().alpha == 0)
        {
            ResetAll4Dialogues();

            background.gameObject.SetActive(false);

            this.gameObject.SetActive(false);
            SceneManager.LoadScene("menu");

        }
    }
}


