using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background_info : MonoBehaviour
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

        pm.enabled = false;
    }


    //this tells you if a line is being written out on the screen
    bool line_started = false;

    bool already = false;

    //this wil ensure that the first line of dialogue is automatically written out on the screen

    int started = 0;

    public GameObject trigger;

    bool didFade = false;

    void Update()
    {
        if (PlayerPrefs.GetInt("saved") == 0)
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

        else if (PlayerPrefs.GetInt("saved") == 1 && didFade == false)
        {
            player.GetComponent<Transform>().position = new Vector3(0.05f, -19.3f, -0.119f);
            background.gameObject.SetActive(false);
            trigger.SetActive(false);
            Debug.Log("Fading  to player");
            didFade = true;
            StartCoroutine(ReleasePlayer());
          /*/  player.GetComponent<Transform>().position = new Vector3(0.05f, -19.3f, -0.119f);
            pm.enabled = true;
            Destroy(background.gameObject);
            Destroy(gameObject);
            Destroy(trigger);
            player.gameObject.GetComponent<PMovement>().speed = 0.05f;*/
        }
        
        if(played == false && i == part)
        {
            played = true;
            StartCoroutine(music_player.FadeAndSwitch(music_player.audioClips[0]));
        }

    }

    public Image anotherBlack;

    IEnumerator ReleasePlayer()
    {
        CanvasGroup canGroup = anotherBlack.gameObject.GetComponent<CanvasGroup>();
        canGroup.alpha = 1;
        canGroup.gameObject.SetActive(true);

        float u = 1;

        while(canGroup.alpha > 0)
        {
            Debug.Log("fading black i = " + u);
            u -= 0.05f;
            canGroup.alpha = u;
            yield return new WaitForSeconds(0.1f);
        }

        
        canGroup.alpha = 0;
        pm.enabled = true;
        canGroup.gameObject.SetActive(false);

       // Destroy(background.gameObject);
       // Destroy(trigger);
        player.gameObject.GetComponent<PMovement>().speed = 0.05f;
        Destroy(gameObject);
        
    }

    public AudioClips_L1 music_player;

    public int part;

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
        if(letter !=' ')
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
        foreach(Text line in text)
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
        if(background.GetComponent<CanvasGroup>().alpha == 0)
        {
            pm.enabled = true;
            pi.enabled = true;

            ResetAll4Dialogues();

            background.gameObject.SetActive(false);

            this.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

