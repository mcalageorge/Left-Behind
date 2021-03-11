using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interacting : MonoBehaviour
{
    //The text object that gets its contents changed

    public Text text_on_screen;

    public void ResetScreen()
    {
        text_on_screen.text = "";
    }

    //The dialogue box where the text object is located, it is used just to move it to a specified location, outlined in the inspector of the object itself

    public Image diaBox;
   
    //source of audio, it is located on the same object
    
    AudioSource source;

    //

    public bool retainSpeed = false;

    PInteraction player;

    PMovement player_toStop;

    void Start() 
    {
        //setting the audio source
        source = this.gameObject.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").gameObject.GetComponent<PInteraction>();
        player_toStop = GameObject.FindWithTag("Player").gameObject.GetComponent<PMovement>();
    }

    //This tells us where the dialogue box to be positioned during a dialogue

    public Vector2 imagePos;
    
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

    int order = 0;

    //The duration between 2 letters being displayed, set it in the inspector as well

    public float readSpeed;

    //true = line is finished and another one can start, false = line is not finished yet and another cant start

     public bool lineFinished = true;

    public Font gaster_font;
    public Font normal_font;

   public void ReadAloud()
   {
        char letter = (text[i])[j];

        if (letter != '9')
        {
            if ((text[i])[0] != '*') { text_on_screen.font = gaster_font; text_on_screen.fontSize = 35; }
            else { text_on_screen.font = normal_font; text_on_screen.fontSize = 40; }
        
            lineFinished = false;
            text_on_screen.text += letter;
            if(letter != ' ')
            source.PlayOneShot(source.clip, 0.7f);
            j++;
            print("i is: " + i + ", j is: " + j);
        }
      
        else
        {
            SkipLine();
            player.Cancel_Invoke();
            
        }
   }

    //Hides the dialogue box

    public void ReleaseImageAndText()
    {
        player_toStop.speed = 0.1f;

        text_on_screen.text = "";

        exitTime = false;

        //the below 3 lines make sure that if the amount of dia_breakers does not go below 0 if there is just 1 dia_breaker 

        if(dia_breakers.Length > 1)
        i = dia_breakers[limitOfResponse - 1];

        else i = 0;

        diaBox.gameObject.SetActive(false);
        text_on_screen.gameObject.SetActive(false);

        if (retainSpeed == false) Debug.Log("give normal speed");
        else player_toStop.speed = 0.05f;

    }

    //If a line is ongoing and stil being read aloud, we can call this function to add the rest of the line in its entirety to the dia box

    public void SkipLine()
    {
        string help = text[i];

        text_on_screen.text = help.Replace('9', ' ');
        i++;
        j = 0;

        lineFinished = true;
    }

    //Enables the dialogue box and the text object so they are visible on screen

    void EnableDialogueBox()
    {
        player_toStop.speed = 0f;
        diaBox.gameObject.SetActive(true);
        text_on_screen.gameObject.SetActive(true);
    }

    //Sets the dialogue location, set the values for the particular object in inspector

    public void SetDialogueBoxLocation()
    {
        diaBox.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(imagePos.x, imagePos.y);
        EnableDialogueBox();
    }

    //Checks if the next limit for the dialogue has been achieved

   public void CheckLimit()
    {
        Debug.Log("check limit i = " + i);

        if(i == dia_breakers[limitOfResponse])
        {
            if(limitOfResponse < (dia_breakers.Length - 1))
            limitOfResponse++;
            exitTime = true;
        }
    }




}
