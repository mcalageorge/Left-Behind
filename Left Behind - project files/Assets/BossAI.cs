using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    public int bossHP = 100;
    public GameObject HP_Slider;

    public Font windings;
    public Font sansFont;

    void Start()
    {
        //setting the audio source
        source = this.gameObject.GetComponent<AudioSource>();
        //setting the attack reference
        bossAttacks = this.gameObject.GetComponent<Attacks>();
        box = this.gameObject.GetComponent<BoxAI>();
        tree = gameObject.GetComponent<DialogueTree>();
    }

    public void SetHP0()
    {
        bossHP = 1;
    }


    public void UpdateHP(bool dead = false)
    {
        if (dead == true)
        HP_Slider.GetComponent<Slider>().value = 0;
        else 
        HP_Slider.GetComponent<Slider>().value = bossHP;

        Debug.Log("HP IS " + bossHP);
    }

    //The text object that gets its contents changed

    public Text text_on_screen;

    public void ResetScreen()
    {
        text_on_screen.text = "";
    }
    void Reset_text()
    {
        for (int i = 0; i < text.Length; i++)
        {
            text[i] = "9";
        }
    }

    //The dialogue box where the text object is located, it is used just to move it to a specified location, outlined in the inspector of the object itself

    public Image diaBox;

    //source of audio, it is located on the same object

    AudioSource source;

    bool hotkeyd = false;
    public bool allowed_to_read = false;
    bool currentlyReading = false;

    bool ones = false;

    bool startsolo = true;

    Attacks bossAttacks;

    BoxAI box;

    public bool feelsLikeAttacking = true;

    public bool turningToSecondPhase = false;
    bool trm = false;
    public Transfer2Phase2 transform;

    public AudioSource music;
    public AudioClip clip;

    public AudioClip sansVoice;
    public RectTransform bubbleImage;
    public RectTransform bubbleText;
    public Sprite sansBubbleSprite;

    bool asteriks = false;
    public AudioClip laugh;

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            hotkeyd = true;
            //StartCoroutine(BossDeath());
        }
        else hotkeyd = false;

        if (allowed_to_read == true)
        {



            if (currentlyReading == false && (hotkeyd == true || startsolo == true) && exitTime == false)
            {



                o = 0;
                ResetScreen();
                //Debug.Log("problema: " + text[i].Length + " " + text.Length);
                if (text != null && (text[i])[j] == '*')
                {
                    source.PlayOneShot(laugh, 0.7f);
                    asteriks = true;
                    Debug.Log("TEXT IS" + text[i]);
                }

                
               else if ((text[i])[j] == '7') { j++; Debug.Log("Game Quit through inquire ending... GJ"); Application.Quit(); }
                else if ((text[i])[j] == '8') { j++; Debug.Log("gaster talking now");text_on_screen.fontSize = 15; text_on_screen.font = windings; voiceBearer = "gaster";/* music.clip = clip; music.Play();*/ }


                else asteriks = false;

                Debug.Log("i is " + i + " currently is " + currentlyReading + " hotkeyd is " + hotkeyd + " startsolo is " + startsolo + " exiTime is " + exitTime);

                startsolo = false;

                diaBox.gameObject.SetActive(true);
                text_on_screen.gameObject.SetActive(true);


                InvokeRepeating("ReadAloud", 0.01f, readSpeed);

                currentlyReading = true;

                Debug.Log("Started reading in bos");
                if (text[i][0] == '9')
                {
                    CancelInvoke("ReadAloud");
                    ReleaseImageAndText();
                    Reset_text();
                    startsolo = true;
                    allowed_to_read = false;
                    currentlyReading = false;
                    exitTime = false;

                    i = 0;
                    if(voiceBearer == "sans")
                    {
                        StartCoroutine(KillPlayer());
                        //
                        Debug.Log("The Player is dead");
                    }

                    else if (turningToSecondPhase == true && trm == false)
                    {
                        transform.SetSetStage2();
                        trm = true;
                    }
                    else if (feelsLikeAttacking == true)
                    {
                        bossAttacks.Attack();
                    }
                    else if (feelsLikeAttacking == false)
                    {
                        StartCoroutine(bossAttacks.FakeAttack());
                    }
                }
            }
            else if (currentlyReading == true && hotkeyd == true && exitTime == false)
            {
                CancelInvoke("ReadAloud");

                currentlyReading = false;
                box.special_interaction = false;

                SkipLine();

                asteriks = false;

                Debug.Log("am intrat");

                print("Stopped reading: currentlyReading = " + currentlyReading);
            }
        }
    }



    //The string array that will be our replies 

    public string[] text;

    //The int array that knows where separate dialogue are held and the current limit.
    //Example: You can interact 3 times with an interactable, each time being different
    //after the new limit is achieved in order, the new limit becomes the next higher one
    //if the line we are curently at is the next line breaker, extiTime = true

    public int[] dia_breakers;
    int limitOfResponse = 0;
    public bool exitTime = false;

    //The 'i' represents the cell in the text[] array, the 'j' represents the character at which we are at one of the cells in the array
    // text[3] = "puya";
    // Ex: (text[i])[j] => (text[3])[3] = a;

    public int i = 0;
    int j = 0;

    //Takes one letter from the Lines, adds it to the text from the dialogue box and if the letter is 9 and the skip has been pressed, go to a new line

    //The duration between 2 letters being displayed, set it in the inspector as well

    public float readSpeed = 0.1f;

    public AudioClip[] voices;

    int o = 0;

    public string voiceBearer = "spider";

    private AudioClip ChooseSound(string speaker)
    {
        if (speaker == "spider") { return voices[voices.Length - 2]; }

        else if (speaker == "gaster")
        {
            Debug.Log("gaster voice enabled");
            int x = Random.Range(0, voices.Length - 2);
            return voices[x];
        }

        else
        {
            return voices[voices.Length - 1];
        }
    }
    int got = 0;

    public void ReadAloud()
    {
        char letter = (text[i])[j];
        if (letter != '9')
        {

    

            currentlyReading = true;
            text_on_screen.text += letter;
            if (letter != ' ')
            {
                if(asteriks == false)
                source.PlayOneShot(ChooseSound(voiceBearer), 1f);
                o++;
                if (o == voices.Length)
                    o = 0;
            }
            j++;
            print("i is: " + i + ", j is: " + j);
        }
        else
        {

            SkipLine();
            currentlyReading = false;
            CancelInvoke("ReadAloud");
        }
    }

    //Hides the dialogue box

    public void ReleaseImageAndText()
    {
        text_on_screen.text = "";

        exitTime = false;

        //the below 3 lines make sure that if the amount of dia_breakers does not go below 0 if there is just 1 dia_breaker 

        i = 0;

        diaBox.gameObject.SetActive(false);
        text_on_screen.gameObject.SetActive(false);
    }

    //If a line is ongoing and stil being read aloud, we can call this function to add the rest of the line in its entirety to the dia box

    bool speak_and_die = false;

    IEnumerator SpeakDie()
    {
        while (true)
        {
            if (allowed_to_read == false) 
            {
                StartDeathGenocide(false, "WalkGenocideFight");
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SkipLine()
    {
        string help = text[i];
        o = 0;
        i++;
        j = 0;

        string help2 = help.Replace('9', ' ');
        text_on_screen.text = help2.Replace("6", "");

    }

    //Checks if the next limit for the dialogue has been achieved

    public SpriteRenderer[] body_parts;
    public Animator boss_animations;
    public Animation boss_animations2;
    public Blink blink;

    public IEnumerator BossMercy(string where_to_go)
    {
        float initial_alpha = 1f;
        boss_animations2.Stop();
        boss_animations.enabled = false;
        music.Stop();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < body_parts.Length; j++)
            {
                body_parts[j].color = new Color(initial_alpha, initial_alpha, initial_alpha, 1f);
                initial_alpha -= 0.01f;
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);

        
            StartCoroutine(FadeOutGoToNewScene(where_to_go));
         Debug.Log("well, this is awkward");

    }

    public Image black;

    IEnumerator FadeOutGoToNewScene(string map_name)
    {
        black.color = new Color(1f, 1f, 1f, 0f);
        black.gameObject.SetActive(true);

        float start = 0f;

        for (int i = 0; i < 100; i++)
        {
            black.color = new Color(1f, 1f, 1f, start);
            start += 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        Debug.Log("we loading " + map_name);
        SceneManager.LoadScene(map_name);

    }

    public ParticleSystem[] particles;

    public void StartDeathGenocide(bool q, string map)
    {
        
        Debug.Log("map2 is " + map);
        StartCoroutine(BossDeath(q, map));
        
    }

    public void StartDeath(bool q)
    {
        StartCoroutine(BossDeath(q));
    }

    public GameObject parent;

    public AudioClip deathSound;

    public IEnumerator BossDeath(bool sansDeath, string where_to_go = "1")
    {

        Debug.Log("where to go is " + where_to_go);

        yield return new WaitForSeconds(0.4f);

            float initial_alpha = 1f;
            boss_animations2.Stop();
            boss_animations.enabled = false;

        for (int particleCount = 0; particleCount < particles.Length; particleCount++)
        {
            particles[i].Play();
        }
            yield return new WaitForSeconds(0.7f);

        source.PlayOneShot(deathSound, 0.5f);
        for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < body_parts.Length; j++)
                {
                    body_parts[j].color = new Color(1f, 1f, 1f, initial_alpha);
                    Debug.Log("alpha coloor for death is " + initial_alpha);
                    initial_alpha -= 0.01f;
                }
                yield return new WaitForSeconds(0.03f);
            }
        for (int particleCount = 0; particleCount < particles.Length; particleCount++)
        {
            particles[i].Stop();
        }
        if (sansDeath == false)
        {
            music.Stop();
            this.gameObject.GetComponent<BoxAI2>().reselect = false;
            this.gameObject.GetComponent<BoxAI2>().reply = "* You won.\n* You earned 666 EXP and 0 gold.\n* Your LOVE greatly increased.9";
            this.gameObject.GetComponent<BoxAI2>().afterbossSpeech = true;


            yield return new WaitForSeconds(5f);

            Debug.Log("fade to new Scene");
            StartCoroutine(FadeOutGoToNewScene(where_to_go));
        }
        else
        {
            parent.SetActive(false);
            CancelInvoke("ReadAloud");
            ReleaseImageAndText();
            startsolo = true;
            allowed_to_read = false;
            currentlyReading = false;
            exitTime = false;

            i = 0;

            texty.font = sansFont;
            texty.fontSize = 60;
            deathy.ping = deathRay;
            deathy.sansTalking = true;

            Debug.Log("we close the song");
            music.Stop();
            Debug.Log("Sans voice now active"); text_on_screen.font = sansFont; voiceBearer = "sans"; bubbleImage.anchoredPosition = new Vector2(92.51f, 164); bubbleText.anchoredPosition = new Vector2(-9.2f, 0); bubbleText.gameObject.GetComponent<Text>().fontSize = 17;
            bubbleImage.gameObject.GetComponent<Image>().sprite = sansBubbleSprite;

            man.allowed_to_change = true;
            text = stage4sans;

            

            sansComesIn.SetBool("makeEntrance", true);
            yield return new WaitForSeconds(0.3f);
            allowed_to_read = true;

            Debug.Log("boss alowed to read text: " + text);
            
        }

    }

    
    public AudioClip deathRay;
    public Dialogue deathy;
    public Text texty;

    DialogueTree tree;

    public Animator sansComesIn;

    string[] stage4sans =
    {
        "Uh...9", "Hey kid...9",  "I had a feeling this would happen.9", "I knew you still haven't changed.9", "That means we still have a long way to go before we get our happy ending.9",
        "Anyway, this is where your road ends.9", "Hope you had your fun.9", "9"
    };

    public Manager man; 

    IEnumerator KillPlayer()
    {
        
        yield return new WaitForSeconds(1f);
        bossAttacks.sans_kill_player_now = true;
        bossAttacks.Attack();
    }
}




