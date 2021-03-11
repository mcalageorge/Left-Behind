using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BoxAI2 : MonoBehaviour
{
    public AudioSource boxSound;

    public Text boxText;

    public string reply = "";

    public bool reselect = true;

    public UnityEngine.EventSystems.EventSystem myEvent;

    void Start()
    {
        tree = this.gameObject.GetComponent<DialogueTree>();
    }

    void ResetBoxText()
    {
        boxText.text = "";
        
    }

    public bool afterbossSpeech = true;

    bool hotkeyd = false;

    bool currentlyReading = false;
    bool lineFinished = true;

    bool exitTime = false;

    public float readSpeed = 0.05f;

    public Manager man;
    public Image fakeArena;

    bool currentlyChanging = false;

    DialogueTree tree;

    bool first2 = false;

    public Attacks atacos;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0))
            hotkeyd = true;

        if (afterbossSpeech == true)
        {
            
            if (man.smallSize == true && currentlyChanging == false)
            {
                atacos.round++;
                man.allowed_to_change = true;
                currentlyChanging = true;
                buton.DisableAll();
            }
            Debug.Log("entered afterSpeech");
            if (currentlyReading == false && lineFinished == true && exitTime == false && man.bigSize == true)
            {
                if (first2 == false) { reply = "* ??? blocks your way.9"; first2 = true; }

                myEvent.SetSelectedGameObject(null);
                ResetBoxText();
                fakeArena.gameObject.SetActive(true);


                boxText.gameObject.SetActive(true);

                InvokeRepeating("ReadAloud", 0.01f, readSpeed);
                currentlyReading = true;
                lineFinished = false;



                Debug.Log("Started reading");
            }
            else if (currentlyReading = true && lineFinished == false && hotkeyd == true && exitTime == false)
            {
                CancelInvoke("ReadAloud");
                currentlyReading = false;
                lineFinished = true;
                SkipLine();
                reply = "";
                print("Stopped reading: currentlyReading = " + currentlyReading + ", lineFinished = " + lineFinished);
            }
            else if (exitTime == true && hotkeyd == true && reselect == true)
            {
                Debug.Log("stop BEING RETARDED");



                //   StartCoroutine("HideFakeArena");
                currentlyReading = false;
                lineFinished = true;
                exitTime = false;
                afterbossSpeech = false;

                //shows Buttons that the main menu is active, this is used for application refocus
                buton.inMenu = true;
                Debug.Log("box2 application focus: " + buton.inMenu);

                StartCoroutine(SetNavigationForPrimaryButt());

                // StartCoroutine("SelectFightDelay");

                currentlyChanging = false;
                j = 0;
                buton.allowedToBack = true;
                Debug.Log("Exited");

            }
        }

        hotkeyd = false;
    }
    bool first = true;

    public Buttons buton;

    IEnumerator HideFakeArena()
    {
        yield return new WaitForSeconds(0.01f);
        boxText.gameObject.SetActive(false);
        fakeArena.gameObject.SetActive(false);
    }

    public int[] blockedButtons = { 0, 0 };

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
            SetNavigationForPrimaryButt();

            buton.allowedToBack = true;
        }
    }

    void SkipLine()
    {
        string help = reply;

        boxText.text = help.Replace('9', ' ');
        j = 0;

        exitTime = true;
    }

    public Button[] primaryButtons;

    public string blockedButton;

    IEnumerator SetNavigationForPrimaryButt()
    {
        

        if (blockedButton == "item" && blockedButtons[1] == 0)
        {
            Debug.Log("UNblocking item");

            blockedButton = null;
            StartCoroutine(buton.BlockItem());

            Navigation nav1 = primaryButtons[0].navigation;
            nav1.selectOnLeft = primaryButtons[3];
            nav1.selectOnRight = primaryButtons[1];
            primaryButtons[0].navigation = nav1;

            Navigation nav2 = primaryButtons[1].GetComponent<Button>().navigation;
            nav2.selectOnLeft = primaryButtons[0];
            nav2.selectOnRight = primaryButtons[2];
            primaryButtons[1].navigation = nav2;

            Navigation nav3 = primaryButtons[2].GetComponent<Button>().navigation;
            nav3.selectOnLeft = primaryButtons[1];
            nav3.selectOnRight = primaryButtons[3];
            primaryButtons[2].navigation = nav3;

            Navigation nav4 = primaryButtons[3].GetComponent<Button>().navigation;
            nav4.selectOnLeft = primaryButtons[2];
            nav4.selectOnRight = primaryButtons[0];
            primaryButtons[3].navigation = nav4;

            yield return new WaitForSeconds(0.01f);

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(primaryButtons[0].gameObject);


        }

        else if(blockedButton == "fight" && blockedButtons[0] == 0)
        {
            Debug.Log("unblocking fight");

            blockedButton = null;
            StartCoroutine(buton.BlockFight());

            Navigation nav1 = primaryButtons[0].navigation;
            nav1.selectOnLeft = primaryButtons[3];
            nav1.selectOnRight = primaryButtons[1];
            primaryButtons[0].navigation = nav1;

            Navigation nav2 = primaryButtons[1].GetComponent<Button>().navigation;
            nav2.selectOnLeft = primaryButtons[0];
            nav2.selectOnRight = primaryButtons[2];
            primaryButtons[1].navigation = nav2;

            Navigation nav3 = primaryButtons[2].GetComponent<Button>().navigation;
            nav3.selectOnLeft = primaryButtons[1];
            nav3.selectOnRight = primaryButtons[3];
            primaryButtons[2].navigation = nav3;

            Navigation nav4 = primaryButtons[3].GetComponent<Button>().navigation;
            nav4.selectOnLeft = primaryButtons[2];
            nav4.selectOnRight = primaryButtons[0];
            primaryButtons[3].navigation = nav4;

            yield return new WaitForSeconds(0.01f);

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(primaryButtons[0].gameObject);
        }

        else if (blockedButtons[1] == 1)
        {
            StartCoroutine(buton.BlockItem());


            Debug.Log("we blocking item");

            Navigation nav1 = primaryButtons[0].GetComponent<Button>().navigation;
            nav1.selectOnLeft = primaryButtons[3].GetComponent<Button>();
            nav1.selectOnRight = primaryButtons[1].GetComponent<Button>();
            primaryButtons[0].navigation = nav1;

            Navigation nav2 = primaryButtons[1].GetComponent<Button>().navigation;
            nav2.selectOnLeft = primaryButtons[0].GetComponent<Button>();
            nav2.selectOnRight = primaryButtons[3].GetComponent<Button>();
            primaryButtons[1].navigation = nav2;

            Navigation nav3 = primaryButtons[2].GetComponent<Button>().navigation;
            nav3.selectOnLeft = null;
            nav3.selectOnRight = null;
            primaryButtons[2].navigation = nav3;

            Navigation nav4 = primaryButtons[3].GetComponent<Button>().navigation;
            nav4.selectOnLeft = primaryButtons[1].GetComponent<Button>();
            nav4.selectOnRight = primaryButtons[0].GetComponent<Button>();
            primaryButtons[3].navigation = nav4;

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(primaryButtons[0].gameObject);
            
            
        }

        else if (blockedButtons[0] == 1)
        {
            StartCoroutine(buton.BlockFight());

            Debug.Log("we blocking fight");

            Navigation nav1 = primaryButtons[0].GetComponent<Button>().navigation;
            nav1.selectOnLeft = null;
            nav1.selectOnRight = null;
            primaryButtons[0].navigation = nav1;

            Navigation nav2 = primaryButtons[1].GetComponent<Button>().navigation;
            nav2.selectOnLeft = primaryButtons[3].GetComponent<Button>();
            nav2.selectOnRight = primaryButtons[2].GetComponent<Button>();
            primaryButtons[1].navigation = nav2;

            Navigation nav3 = primaryButtons[2].GetComponent<Button>().navigation;
            nav3.selectOnLeft = primaryButtons[1].GetComponent<Button>();
            nav3.selectOnRight = primaryButtons[3].GetComponent<Button>();
            primaryButtons[2].navigation = nav3;

            Navigation nav4 = primaryButtons[3].GetComponent<Button>().navigation;
            nav4.selectOnLeft = primaryButtons[2].GetComponent<Button>();
            nav4.selectOnRight = primaryButtons[1].GetComponent<Button>();
            primaryButtons[3].navigation = nav4;

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(primaryButtons[1].gameObject);
        }

        else
        {
            Debug.Log("UnblockingEverything");
            Navigation nav1 = primaryButtons[0].navigation;
            nav1.selectOnLeft = primaryButtons[3];
            nav1.selectOnRight = primaryButtons[1];
            primaryButtons[0].navigation = nav1;

            Navigation nav2 = primaryButtons[1].GetComponent<Button>().navigation;
            nav2.selectOnLeft = primaryButtons[0];
            nav2.selectOnRight = primaryButtons[2];
            primaryButtons[1].navigation = nav2;

            Navigation nav3 = primaryButtons[2].GetComponent<Button>().navigation;
            nav3.selectOnLeft = primaryButtons[1];
            nav3.selectOnRight = primaryButtons[3];
            primaryButtons[2].navigation = nav3;

            Navigation nav4 = primaryButtons[3].GetComponent<Button>().navigation;
            nav4.selectOnLeft = primaryButtons[2];
            nav4.selectOnRight = primaryButtons[0];
            primaryButtons[3].navigation = nav4;

            yield return new WaitForSeconds(0.01f);

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(primaryButtons[0].gameObject);
        }

    }
}
