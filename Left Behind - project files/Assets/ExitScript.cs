using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ExitScript : MonoBehaviour
{
    bool collidor = false;

    Interacting o;

    int x = 0;

    public GameObject dialogue;

    bool done = false;
    bool exitQuestion;
    bool exitAnswer = false;

    public Text text;


    GameObject player;
    public GameObject black;

    public UnityEngine.EventSystems.EventSystem myEvent;

    bool enabled = true;

    bool skipped = false;

    int selected = 3;

    public int metin2;

    void Start()
    {
        o = this.gameObject.GetComponent<Interacting>();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if(butt2.gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt2);
            selected = 1;
        }
    }


    void Update()
    {
        x = o.i;

        if (Input.GetMouseButtonDown(0) && butt2.gameObject.activeSelf == true)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt2);
            selected = 1;
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && talking == true && text.text.Length >= 5) { skipped = true; }

        

        if (talking == true && text.text.Length >= 5 && Input.GetAxisRaw("Horizontal") < 0 && selected != 0)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt1);
            selected = 0;
        }
        else if (talking == true && text.text.Length >= 5 && Input.GetAxisRaw("Horizontal") > 0 && selected != 1)
        {
            myEvent.SetSelectedGameObject(null);
            myEvent.SetSelectedGameObject(butt2);
            selected = 1;
        }

        if (dialogue.gameObject.activeSelf == true)
            exitQuestion = true;
        else
            exitQuestion = false;
            exitQuestion = false;

        if (x == metin2 && done == false)
        {
            exitAnswer = true;
            done = true;
        }

        if (enabled == true && collidor == true && exitQuestion == false && exitAnswer == true && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0)))
        {
            Destroy(gameObject.GetComponent<Interacting>());
            player.gameObject.GetComponent<PInteraction>().dialogue = this.gameObject.GetComponent<ExitScript>();
            enabled = false;
            exitAnswer = false;
            Debug.Log("WE CAN DESTROY THE IT");
            gameObject.tag = "dialogue";
        }
    }

    public void EnableDialogueScreen()
    {
        dialogue.SetActive(true);
        text.gameObject.SetActive(true);
        dialogue.GetComponent<RectTransform>().localPosition = new Vector3(0, 195, 0);

        StartCoroutine(TextFlow());

        if (player != null)
        {
            player.gameObject.GetComponent<PMovement>().enabled = false;
            player.gameObject.GetComponent<PInteraction>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        black.gameObject.SetActive(false);
        player = collider.gameObject;
        collidor = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //black.gameObject.SetActive(true);
        player = null;
        collidor = false;
        butt1.gameObject.SetActive(false);
        butt2.gameObject.SetActive(false);
        
    }

    public GameObject badEnding;

    public void OnYes() {
        text.text = "";
        talking = false;
        badEnding.gameObject.SetActive(true);
        dialogue.SetActive(false);
        text.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public GameObject butt1;
    public GameObject butt2;

    public void OnNo() {

        text.text = "";
        talking = false;
        Debug.Log("we pressed no");
        skipped = false;
        player.gameObject.GetComponent<PMovement>().enabled = true;
        player.gameObject.GetComponent<PInteraction>().enabled = true;
        player.gameObject.GetComponent<PMovement>().speed = 0.1f;



        dialogue.SetActive(false);
        text.gameObject.SetActive(false);
        butt1.SetActive(false);
        butt2.SetActive(false);
        
    }

    bool YESsel = false;

    public Sprite YesSelected;
    public Sprite YesDeselected;

    public Sprite NoSelected;
    public Sprite NoDeselected;

    public AudioClip bop;

    public void SelectedYES()
    {
        butt1.GetComponent<Image>().sprite = YesSelected;
        this.GetComponent<AudioSource>().PlayOneShot(bop, 1f);
    }

    public void DeselectedYES()
    {
        butt1.GetComponent<Image>().sprite = YesDeselected;
    }

    public void SelectedNO()
    {
        butt2.GetComponent<Image>().sprite = NoSelected;
        this.GetComponent<AudioSource>().PlayOneShot(bop, 0.7f);
    }

    public void DeselectedNO()
    {
        butt2.GetComponent<Image>().sprite = NoDeselected;
    }

    bool textedLine = false;
    bool textedMessage = false;

    public float readSpeed;
    public string[] replies;

    int i = 0;
    int j = 0;

    IEnumerator TextFlow()
    {
        text.text = "";
        while (textedLine == false)
            {     

            yield return new WaitForSeconds(readSpeed);

            if (j == replies[i].Length)
            {
                textedLine = true;
                i = 0;
                j = 0;
                ShowButtons();
                
                textedLine = false;
               
                break;
                
            }
            
            text.text += (replies[i])[j];
            if((replies[i])[j] != ' ')
            this.GetComponent<AudioSource>().PlayOneShot(bop, 1f);
            j++;
            if (skipped == true) { text.text = ""; text.text = replies[i]; i = 0; j = 0; ShowButtons(); gameObject.tag = "dialogue"; textedLine = false; break; }
            }      
    }

    void ShowButtons()
    {
        butt1.gameObject.SetActive(true);
        butt2.gameObject.SetActive(true);

        myEvent.SetSelectedGameObject(null);
        myEvent.SetSelectedGameObject(butt2);
    }

    public bool talking = false;
}
