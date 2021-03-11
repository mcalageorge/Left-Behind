using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BadEndingScript : MonoBehaviour
{
    public GameObject player;
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
    public AudioClip[] clips;

    //array of lines of the character
    public string[] replies;

    void Start()
    {
        ResetAll4Dialogues();
        line_started = false;


        pm = player.gameObject.GetComponent<PMovement>();
        pi = player.gameObject.GetComponent<PInteraction>();
    }


    //this tells you if a line is being written out on the screen
    bool line_started = false;

    bool already = false;

    //this wil ensure that the first line of dialogue is automatically written out on the screen

    int started = -1;

    public AudioSource aud;

    void Update()
    {
        if (i > replies.Length) SceneManager.LoadScene(0);
        switch (started)
        {
            case -1:
                {
                    if(doPlay == true)
                    {
                        aud.clip = null;
                        this.GetComponent<AudioSource>().clip = song;
                        this.GetComponent<AudioSource>().Play();
                    }

                    background.GetComponent<CanvasGroup>().alpha = 0;
                    background.gameObject.SetActive(true);
                    pm.enabled = false;
                    pi.enabled = false;

                    StartCoroutine(FadedIn());
                    break;
                }

            case 0:
                {
                        
                        InvokeRepeating("ReadLines", 0.01f, 0.1f);
                        line_started = true;
                        started = 1;
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
                    CancelInvoke("ReadLines");
                    if (once == false)
                    {
                        once = false;
                        StartCoroutine(GoToMenu());
                        Debug.Log("tp");
                    }
                   // SceneManager.LoadScene(0);
                    break;
                }
        }
    }

    bool once = false;

    IEnumerator GoToMenu()
    {
        for(int i = 0; i < 4; i++)
        {
            text[i].text = " ";
        }
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    char letter = ' ';

    public Font gaster_font;
    public AudioClip gaster_voice;
    public AudioClip gaster_song;
    bool gaster = false;

    public bool doPlay = false;
    public AudioClip song;

    public void ReadLines()
    {
     //   char letter = ' ';
        // 
        if (i >= replies.Length) started = 3;
        //
        else
            letter = (replies[i])[j];

        if(letter == '1')
        {
            text[0].font = gaster_font;
            text[1].font = gaster_font;
            text[2].font = gaster_font;
            text[3].font = gaster_font;

            gaster = true;

            this.GetComponent<AudioSource>().clip = gaster_song;
            this.GetComponent<AudioSource>().Play();

            j++;
            letter = (replies[i])[j];
        }

        Debug.Log(x + " is x");
        Debug.Log(i + " is i");
        Debug.Log(j + " is j");

        if (letter != '9')
            text[x].text += letter;
        if (letter != ' ')
            this.GetComponent<AudioSource>().PlayOneShot(GiveMeVoice(gaster), 1f);
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

    AudioClip GiveMeVoice(bool isGasterTime)
    {
        if(isGasterTime == false)
        {
            return clips[0];
        }
        else
        {
            int ran = Random.Range(2, 7);
            return clips[ran];
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

    IEnumerator FadedIn()
    {
        started = -2;
        while (background.GetComponent<CanvasGroup>().alpha < 1)
        {
            this.GetComponent<AudioSource>().volume += 0.05f;
            background.GetComponent<CanvasGroup>().alpha += 0.05f;
            yield return new WaitForSeconds(0.1f);
            if (background.GetComponent<CanvasGroup>().alpha == 1)
            {
                started = 0;
                break;
            }
        }
    }
}
