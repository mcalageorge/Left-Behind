using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Intro : MonoBehaviour
{
    public GameObject butt1;
    public GameObject butt2;
    public GameObject butt3;

    string selected = "none";

    public GameObject tutorial;

    public GameObject credits;

    public GameObject flavor;

    public UnityEngine.EventSystems.EventSystem myEvent;

    bool isClickable = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Coroutine());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClickable == true)
        {
            if(PlayerPrefs.GetInt("saved") == 1)
            {
                myEvent.SetSelectedGameObject(null);
                myEvent.SetSelectedGameObject(butt1);
                selected = "continue";
            }
            else
            {
                myEvent.SetSelectedGameObject(null);
                myEvent.SetSelectedGameObject(butt2);
            }
        }
    }

    public CanvasGroup door;
    IEnumerator FadeDoorInOut()
    {
        while(door.alpha < 1)
        {
            door.alpha += 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(2f);
        while (true)
        {
            while (door.alpha > 0.3f)
            {
                door.alpha -= 0.01f;
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(2f);
            while(door.alpha < 1f)
            {
                door.alpha += 0.01f;
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    public AudioSource source;
    public AudioClip music;
    public AudioClip boom;

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(boom, 0.7f);
        yield return new WaitForSeconds(2f);
        credits.gameObject.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<AudioSource>().clip = music;

        int ran = Random.Range(1, 31);
        if(ran == 30)
        {
            this.gameObject.GetComponent<AudioSource>().clip = reversed_music;
        }
        
        this.gameObject.GetComponent<AudioSource>().Play();


        if (PlayerPrefs.GetInt("saved") == 1) butt1.gameObject.SetActive(true);

        flavor.gameObject.SetActive(true);

        butt2.gameObject.SetActive(true);
        butt3.gameObject.SetActive(true);

        StartCoroutine(FadeDoorInOut());

        if (PlayerPrefs.GetInt("saved") == 1)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt1);
            selected = "continue";
        }
        else
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt2);
        }
        isClickable = true;
        //yield return new WaitForSeconds(1f);

        StartCoroutine(FadeTutorialIn());
    }

    IEnumerator FadeTutorialIn()
    {
        CanvasGroup cg = tutorial.gameObject.GetComponent<CanvasGroup>();
        
        for(int i = 0; i <= 100; i++)
        {
            cg.alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SelectContinue()
    {
        //  myEvent.SetSelectedGameObject(null);

        if (selected != "continue")
        {
         //   myEvent.SetSelectedGameObject(butt1);
            selected = "continue";

            WhitenAllButtons();

            butt1.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255f, 255f, 0f, 255f);
        }
    }
    public void SelectNew()
    {
        //   myEvent.SetSelectedGameObject(null);

        if (selected != "new")
        {
           // myEvent.SetSelectedGameObject(butt2);
            selected = "new";
            


            WhitenAllButtons();

            butt2.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255f, 255f, 0f, 255f);
        }
    }
    public void SelectExit()
    {
        //   myEvent.SetSelectedGameObject(null);
        if (selected != "exit")
        {
          //  myEvent.SetSelectedGameObject(butt3);
            selected = "exit";

            WhitenAllButtons();

            butt3.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255f, 255f, 0f, 255f);
        }
    }

    void WhitenAllButtons()
    {
        butt1.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        butt2.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        butt3.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
    }

    public AudioClip reversed_music;
}
