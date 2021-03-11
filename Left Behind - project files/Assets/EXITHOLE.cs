using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXITHOLE : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("wow");
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PMovement>().enabled = false;
            player = col.gameObject;
            pm = player.gameObject.GetComponent<PMovement>();
            pi = player.gameObject.GetComponent<PInteraction>();
            StartCoroutine(FadeIn());
            
            line_started = true;
        }
    }
    GameObject player;
    private PMovement pm;
    private PInteraction pi;

    //array of texts
    public Text[] text;

    //counter for the text array
    int x = 0;

    //counter for the lines array
    int i = 0;

    //counter for the character inside array
    int j = 0;

    //array of sounds
    public AudioClip[] voice;

    //array of lines of the character
    public string[] replies;

    void Start()
    {

        


        

        
    }


    //this tells you if a line is being written out on the screen
    bool line_started = false;

    bool already = false;

    //this wil ensure that the first line of dialogue is automatically written out on the screen

    int started = 1;

    bool beckon = false;

    public Image image;

    void Update()
    {
        if(beckon == true)
            switch (started)
            {

                    case 1:
                            {
                        ResetAll4Dialogues();
                            image.gameObject.SetActive(true);
                            for (int i = 0; i < 4; i++)
                                text[i].gameObject.SetActive(true);
                                    InvokeRepeating("ReadLines", 0.01f, 0.1f);
                                    line_started = true;
                                    started = 1;
                                
                                break;
                            }
                    case 2:
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
                    default:
                            {
                                Debug.Log("out of the loop");
                    break;
                            }
            }

        

    }


    int v = 0;
    bool played = false;

    public void ReadLines()
    {
        char letter = ' ';
        // 
        if (i >= replies[v].Length) started = 2;
        //


       /* else
            letter = (replies[v][i][j]);*/

        Debug.Log(x + " is x");
        Debug.Log(i + " is i");
        Debug.Log(j + " is j");



        if (letter != '9')
            text[x].text += letter;
        if (letter != ' ')
            this.GetComponent<AudioSource>().PlayOneShot(voice[0], 1f);
        j++;

        if (letter == '9')
        {

            j = 0;
            i++;
            if (x < 3)
                x++;
            else x = 0;

            v++;
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
        v++;
        CancelInvoke("ReadLines");
    }

    public Image background;

    IEnumerator FadeIn()
    {
        Debug.Log("we get here");
        while (true)
        {
            background.GetComponent<CanvasGroup>().alpha += 0.05f;
            if (background.GetComponent<CanvasGroup>().alpha == 1f)
            {
                
                beckon = true;
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        
    }
}
