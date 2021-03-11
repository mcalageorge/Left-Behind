using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{
    public GameObject fight;

    public GameObject[] act;

    public GameObject actd;

    public GameObject[] item;

    public GameObject[] mercy;

    public GameObject[] parentButtons;

    string selected;

    public UnityEngine.EventSystems.EventSystem myEvent;

    public GameObject[] reference_Buttons;

    public bool actII;

    public SelectedButton heartNavScript;

    public GameObject fakeArenaWithType;

    void Start()
    {
        actII = true;
        GetPlaceholderPos();
        GetMapPos();
    }

    Transform[] phPos = new Transform[4];

    private void GetPlaceholderPos()
    {
        for (int k = 0; k < 4; k++)
        {
            phPos[k] = reference_Buttons[k].GetComponent<Transform>();
        }

        int j = 0;
        foreach (Transform i in phPos)
        {
            print("pozitia " + j + " este " + i.position);
            j++;
        }
    }

    Vector3 fight_Map;
    Vector3[] act_Map = new Vector3[8];

    Vector3[] foods_Map = new Vector3[8];
    Vector3[] mercy_Map = new Vector3[2];
    Vector3 actd_Map;

    public GameObject[] maps;

    void GetMapPos()
    {
        fight_Map = maps[0].GetComponent<Transform>().position;

        print("maaaaaaaaaaaaaaps is  " + fight_Map);

        int k = 0;
        for (int i = 1; i <= 8; i++)
        {
            act_Map[k] = maps[i].GetComponent<Transform>().position;
            print("act map " + i + " is: " + act_Map[k]);
            k++;
        }

        int f = 0;
        for (int i = 9; i <= 16; i++)
        {
            foods_Map[f] = maps[i].GetComponent<Transform>().position;
            f++;
        }

        mercy_Map[0] = maps[17].GetComponent<Transform>().position;
        mercy_Map[1] = maps[18].GetComponent<Transform>().position;

        actd_Map = maps[19].GetComponent<Transform>().position;
    }

    public bool allowedToBack = true;

    bool returnToBase = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.JoystickButton1))
            returnToBase = true;

        if(inMenu == true && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ReturnToBaseUI());
        }

        if (returnToBase && allowedToBack == true)
        {


            print("got the message");

            switch (selected)
            {
                case "fight":
                    {
                        DisableFight();
                        heartNavScript.DisableHeart();
                        selected = "out";
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(parentButtons[0]);
                        fakeArenaWithType.SetActive(true);

                        break;
                    }
                case "actI":
                    {
                        DisableActI();
                        heartNavScript.DisableHeart();
                        selected = "out";
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(parentButtons[1]);
                        fakeArenaWithType.SetActive(true);

                        break;
                    }
                case "actII":
                    {
                        DisableActII();
                        heartNavScript.DisableHeart();
                        selected = "actI";
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(actd);

                        break;
                    }
                case "actIII":
                    {
                        DisableActIII();
                        heartNavScript.DisableHeart();
                        selected = "actI";
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(actd);

                        break;
                    }
                case "item":
                    {

                        DisableItem();
                        selected = "out";
                        heartNavScript.DisableHeart();
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(parentButtons[2]);
                        foodCounter.gameObject.SetActive(false);
                        fakeArenaWithType.SetActive(true);
                        foodCounter.text = "PAGE 1";

                        break;
                    }
                case "mercy":
                    {

                        DisableMercy();
                        selected = "out";
                        heartNavScript.DisableHeart();
                        myEvent.SetSelectedGameObject(null);
                        myEvent.SetSelectedGameObject(parentButtons[3]);
                        fakeArenaWithType.SetActive(true);

                        break;
                    }

                default:
                    {
                        Debug.Log("We out");

                        break;
                    }
            }
            Debug.Log(selected + " oingo boingo");
        }
        returnToBase = false;
    }

    public GameObject chooseEnemyButton;

    public void EnableFight()
    {

        Disable4Hearts();
        fakeArenaWithType.SetActive(false);

        myEvent.SetSelectedGameObject(null);
        myEvent.SetSelectedGameObject(fight);

        PlayHover();
        selected = "fight";
        Debug.Log("selected is " + selected);
        foodCounter.gameObject.SetActive(false);

        fight.GetComponent<Transform>().position = phPos[0].position;
    }

    public void EnableActI()
    {
        Disable4Hearts();
        fakeArenaWithType.SetActive(false);

        actd.gameObject.GetComponent<Transform>().position = phPos[0].position;

        myEvent.SetSelectedGameObject(null);
        myEvent.SetSelectedGameObject(actd);

        foodCounter.gameObject.SetActive(false);

        PlayHover();
        selected = "actI";
        Debug.Log("selected is " + selected);
    }

    public void EnableActII()
    {
        if (actII == true)
        {


            actd.gameObject.GetComponent<Transform>().position = actd_Map;
            for (int i = 0; i < 4; i++)
            {
                act[i].gameObject.SetActive(true);
                act[i].gameObject.GetComponent<Transform>().position = phPos[i].position;
            }

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(act[0]);

            PlayHover();
            selected = "actII";
        }
        else EnableActIII();
    }

    public void EnableActIII()
    {

        actd.gameObject.GetComponent<Transform>().position = actd_Map;
        int k = 0;
        for (int i = 4; i < 8; i++)
        {
            act[i].gameObject.SetActive(true);
            act[i].gameObject.GetComponent<Transform>().position = phPos[k].position;
            k++;
        }

        myEvent.SetSelectedGameObject(null);
        myEvent.SetSelectedGameObject(act[4]);

        PlayHover();
        selected = "actIII";
    }

    public void EnableMercy()
    {
        Disable4Hearts();
        fakeArenaWithType.SetActive(false);

        for (int i = 0; i < 2; i++)
        {
            mercy[i].gameObject.GetComponent<Transform>().position = phPos[i].position;
        }

        myEvent.SetSelectedGameObject(null);
        myEvent.SetSelectedGameObject(mercy[0]);

        PlayHover();
        selected = "mercy";
    }

    public void DisableMercy()
    {
        for (int i = 0; i < 2; i++)
        {
            mercy[i].gameObject.GetComponent<Transform>().position = mercy_Map[i];
        }
    }


    public void EnableItem()
    {
        bool isActiveAnyItem = false;
        for (int j = 0; j < 8; j++)
        {
            if (item[j].gameObject.activeSelf == true)
            {
                isActiveAnyItem = true;
                break;
            }
        }
        if (isActiveAnyItem == true)
        {
            foodCounter.gameObject.SetActive(false);
            



            Disable4Hearts();
            fakeArenaWithType.SetActive(false);
            SortItems();
            for (int i = 0; i < 4; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = phPos[i].position;
            }
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(item[0]);

            for (int i = 4; i < 8; i++)
            {
                if (item[i].activeSelf == true)
                    foodCounter.gameObject.SetActive(true);
            }

            PlayHover();
            selected = "item";
        }
        else
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(parentButtons[2].gameObject);
        }
    }

    public void EnableItemSet1()
    {
        if (item[0].activeSelf == true)
        {
            for (int i = 4; i < 8; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = foods_Map[i];
            }

            for (int i = 0; i < 4; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = phPos[i].position;
            }
            StartCoroutine("WaitSel1");
        }
        PlayHover();
        selected = "item";
    }

    public void EnableItemSet2()
    {
        if (item[4].activeSelf == true)
        {
            for (int i = 0; i < 4; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = foods_Map[i];
            }

            int k = 0;
            for (int i = 4; i < 8; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = phPos[k].position;
                k++;
            }

            StartCoroutine("WaitSel0");
        }
        PlayHover();
        selected = "item";
    }
    public void EnableItemSet3()
    {
        if (item[0].activeSelf == true)
        {
            for (int i = 4; i < 8; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = foods_Map[i];
            }

            for (int i = 0; i < 4; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = phPos[i].position;
            }
            StartCoroutine("WaitSel11");
            PlayHover();
            selected = "item";
        }

    }
    public void EnableItemSet4()
    {
        if (item[4].activeSelf == true)
        {
            for (int i = 0; i < 4; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = foods_Map[i];
            }

            int k = 0;
            for (int i = 4; i < 8; i++)
            {
                item[i].gameObject.GetComponent<Transform>().position = phPos[k].position;
                k++;
            }

            StartCoroutine("WaitSel00");
            PlayHover();
            selected = "item";
        }

    }

    IEnumerator WaitSel0()
    {
        yield return new WaitForSeconds(0.01f);
        if (item[4].gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(item[4]);
            foodCounter.text = "PAGE 2";
        }
    }
    IEnumerator WaitSel00()
    {
        yield return new WaitForSeconds(0.01f);
        if (item[5].gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(item[5]);
            foodCounter.text = "PAGE 2";
        }
    }
    IEnumerator WaitSel1()
    {
        yield return new WaitForSeconds(0.01f);
        if (item[2].gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(item[2]);
            foodCounter.text = "PAGE 1";
        }
    }
    IEnumerator WaitSel11()
    {
        yield return new WaitForSeconds(0.01f);
        if (item[3].gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(item[3]);
            foodCounter.text = "PAGE 1";
        }
    }

    GameObject previousFood;


    void SortItems()
    {
        for (int z = 0; z < 8; z++)
        {
                for (int i = 0; i < 7; i++)
                {
                    if (item[i].activeSelf == false)
                    {
                        GameObject aux = item[i];
                        for (int j = i + 1; j < 8; j++)
                        {
                            item[i] = item[j];
                            i++;
                        }
                        print("pppppppppp");
                        item[7] = aux;
                    }
                }
        }
        SetItemNavigation();
    }

    public GameObject[] prevNext;

    void SetItemNavigation()
    {
        for (int i = 0; i < 8; i++)
        {
            Navigation nav = item[i].GetComponent<Button>().navigation;

            //reset nav to0 on all directions

            nav.selectOnUp = null;
            nav.selectOnRight = null;
            nav.selectOnLeft = null;
            nav.selectOnDown = null;

            if (i == 0 || i == 4)
            {
                nav.selectOnDown = item[i + 1].GetComponent<Button>();
                nav.selectOnRight = item[i + 2].GetComponent<Button>();

                if (i == 4)
                {
                    nav.selectOnLeft = prevNext[0].GetComponent<Button>();
                }
            }
            else if (i == 1 || i == 5)
            {
                nav.selectOnUp = item[i - 1].GetComponent<Button>();
                nav.selectOnRight = item[i + 2].GetComponent<Button>();

                if (i == 5)
                    nav.selectOnLeft = prevNext[2].GetComponent<Button>();
            }
            else if (i == 2 || i == 6)
            {
                nav.selectOnLeft = item[i - 2].GetComponent<Button>();
                nav.selectOnDown = item[i + 1].GetComponent<Button>();

                if (i == 2 && item[4].activeSelf == true)
                    nav.selectOnRight = prevNext[1].GetComponent<Button>();
            }
            else if (i == 3 || i == 7)
            {
                nav.selectOnLeft = item[i - 2].GetComponent<Button>();
                nav.selectOnUp = item[i - 1].GetComponent<Button>();

                if (i == 3 && item[5].activeSelf == true)
                    nav.selectOnRight = prevNext[3].GetComponent<Button>();
                else if (i == 3 && item[4].activeSelf == true)
                    nav.selectOnRight = prevNext[1].GetComponent<Button>();
            }
            item[i].GetComponent<Button>().navigation = nav;

        }
    }

    public void DisableAll()
    {
        DisableFight();

        DisableActII();
        DisableActIII();
        DisableActI();
        DisableItem();
        DisableMercy();
        myEvent.SetSelectedGameObject(null);
        selected = "out";
    }

    public void DisableItem()
    {
        for (int i = 0; i < 8; i++)
        {
            item[i].gameObject.GetComponent<Transform>().position = foods_Map[i];
        }
    }

    void DisableFight()
    {
        print("fight map is " + fight_Map);
        fight.GetComponent<Transform>().position = fight_Map;

    }
    void DisableActI()
    {
        actd.GetComponent<Transform>().position = actd_Map;

        selected = "actI";
    }
    void DisableActII()
    {
        for (int i = 0; i < 4; i++)
        {
            act[i].gameObject.GetComponent<Transform>().position = act_Map[i];
        }
        EnableActI();
        selected = "actII";
    }
    void DisableActIII()
    {

        for (int i = 4; i < 8; i++)
        {
            act[i].gameObject.GetComponent<Transform>().position = act_Map[i];

        }
        EnableActI();
        selected = "actII";
    }
    public void PlayHover()
    {
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(this.gameObject.GetComponent<AudioSource>().clip, 0.3f);
    }

    public GameObject[] foods;
    public Text foodCounter;

    public void DisableF()
    {
        DisableFight();
        heartNavScript.DisableHeart();
        selected = "out";
        myEvent.SetSelectedGameObject(null);
    }

    public GameObject[] fourHearts;

    public void EnableOneH()
    {
        Disable4Hearts();
        fourHearts[0].SetActive(true);
    }
    public void EnableTwoH()
    {
        Disable4Hearts();
        fourHearts[1].SetActive(true);
    }
    public void EnableThreeH()
    {
        Disable4Hearts();
        fourHearts[2].SetActive(true);
    }
    public void EnableFourH()
    {
        Disable4Hearts();
        fourHearts[3].SetActive(true);
    }
    void Disable4Hearts()
    {
        for (int i = 0; i < 4; i++)
        {
            fourHearts[i].SetActive(false);
        }
    }

    public GameObject blockAttack;
    public GameObject blockItem;
    public AudioClip blockSound;



    public BoxAI2 box2;

    public IEnumerator BlockFight()
    {
        if (box2.blockedButtons[0] == 1)
        {
            Debug.Log("we blocking fight");
            blockAttack.SetActive(true);
            gameObject.GetComponent<AudioSource>().PlayOneShot(blockSound, 0.7f);
            box2.blockedButtons[0] = 1;

            while (true)
            {
                Vector3 testPos = blockAttack.gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (testPos.x >= 0 && testPos.y >= 0)
                {
                    break;
                }
                else
                {
                    blockAttack.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(testPos.x + 0.1f, testPos.y + 0.1F, 0);
                }
                yield return new WaitForSeconds(0.025f);
            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(blockSound, 0.7f);
            while (true)
            {
                Vector3 testPos = blockAttack.gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (testPos.x <= -3.17f && testPos.y <= -3.06)
                {
                    break;
                }
                else
                {
                    blockAttack.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(testPos.x - 0.1f, testPos.y - 0.1f, 0);
                }
                yield return new WaitForSeconds(0.05f);
            }
            blockAttack.SetActive(false);

            box2.blockedButtons[0] = 0;
        }
    }



    public IEnumerator BlockItem()
    {
        if (box2.blockedButtons[1] == 1)
        {
            blockItem.SetActive(true);
            gameObject.GetComponent<AudioSource>().PlayOneShot(blockSound, 0.7f);
            box2.blockedButtons[1] = 1;

            while (true)
            {
                Vector3 testPos = blockItem.gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (testPos.y >= 0)
                {
                    break;
                }
                else
                {
                    blockItem.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(1.81f, testPos.y + 0.1f, 0);
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(blockSound, 0.7f);
            while (true)
            {
                Vector3 testPos = blockItem.gameObject.GetComponent<RectTransform>().anchoredPosition;

                if (testPos.y <= -4.65f)
                {
                    break;
                }
                else
                {
                    blockItem.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(1.81f, testPos.y - 0.1f, 0);
                }
                yield return new WaitForSeconds(0.05f);
            }
            blockItem.SetActive(false);
            box2.blockedButtons[1] = 0;
        }
    }

    public AudioClip healSound;

    public bool inMenu = false;

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        if (inMenu == true)
        {
            StartCoroutine(ReturnToBaseUI());
        }
        print("The application focus is currently " + hasFocus);
    } 

    IEnumerator ReturnToBaseUI()
    {
        returnToBase = true;
        yield return new WaitForSeconds(0.01f);
        returnToBase = true;

        if (box2.blockedButtons[0] == 0)
        {

            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(parentButtons[0]);
        }
        else
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(parentButtons[1]);
        }
    }

}
