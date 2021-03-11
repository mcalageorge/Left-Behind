using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxAI : MonoBehaviour
{
    public AudioSource boxSound;

    public Text boxText;

    public string reply;

    public UnityEngine.EventSystems.EventSystem myEvent;

    void ResetBoxText()
    {
        boxText.text = "";
    }

    public bool allowed_to_type = false;
    bool hotkeyd = false;

    public bool afterAttack = false;

    bool currentlyReading = false;
    bool lineFinished = true;

    bool exitTime = false;

    public float readSpeed = 0.05f;

    public Manager man;
    public Image fakeArena;

    Attacks bossAttacks;

    public Buttons butt;

    bool began = false;

    void Start()
    {
        bossAttacks = this.gameObject.GetComponent<Attacks>();
    }

    public int legEaten = 0;
    public int donutEaten = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0))
            hotkeyd = true;

        if (allowed_to_type == true)
        {
            butt.allowedToBack = false;
            if (currentlyReading == false && lineFinished == true && (hotkeyd == true || began == false) && exitTime == false)
            {
                butt.inMenu = false;
                Debug.Log("application focus on menu is now " + butt.inMenu);
                Debug.Log("Activated bubble speech");
                myEvent.SetSelectedGameObject(null);
                fakeArena.gameObject.SetActive(true);
                boxText.gameObject.SetActive(true);
                ResetBoxText();
                InvokeRepeating("ReadAloud", 0.01f, readSpeed);
                currentlyReading = true;
                began = true;

                Debug.Log("Started reading");
            }
            else if (currentlyReading = true && lineFinished == false && hotkeyd == true && exitTime == false)
            {
                CancelInvoke("ReadAloud");
                currentlyReading = false;
                lineFinished = true;
                SkipLine();
                print("Stopped reading: currentlyReading = " + currentlyReading + ", lineFinished = " + lineFinished);
            }
            else if (exitTime == true && hotkeyd == true)
            {
                Debug.Log("stop BEING RETARDED");
                    man.allowed_to_change = true;
                    allowed_to_type = false;
                currentlyReading = false;
                lineFinished = true;
                exitTime = false;
                began = false;
                
                j = 0;
                ResetBoxText();
                StartCoroutine("HideFakeArena");

                Debug.Log("Exited");
            }
        }

        hotkeyd = false;
    }

  

    IEnumerator HideFakeArena()
    {
        yield return new WaitForSeconds(0.01f);
        ChooseDestination();
        yield return new WaitForSeconds(1f);
        boxText.gameObject.SetActive(false);
        fakeArena.gameObject.SetActive(false);
    }


    int j = 0;

    public void ReadAloud()
    {
        
           char letter = reply[j];
       

        if (letter != '9')
        {
            lineFinished = false;
            boxText.text += letter;
            if (letter != ' ')
                boxSound.Play();
            j++;
            print("j is: " + j);
        }

        else
        {
            SkipLine();
            CancelInvoke("ReadAloud");

        }
    }

    void SkipLine()
    {
        string help = reply;

        boxText.text = help.Replace('9', ' ');
        j = 0;

        exitTime = true;
    }

    public bool special_interaction;

    public BossAI bos;

    void ChooseDestination()
    {
        if(special_interaction == true)
        {
            special_interaction = false;
            bos.allowed_to_read = true;
            //Enable boss quote and then fight
        }
        else
        {
            bossAttacks.Attack();
        }
    }
}
