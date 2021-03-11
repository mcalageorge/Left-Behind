using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PInteraction : MonoBehaviour
{

    bool currentlyReading = false;
    public bool hotkeyd;
    public bool wait = false;

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)) && wait == false)
            hotkeyd = true;

        if (read != null)
        {
            read.CheckLimit();
            if (currentlyReading == false && read.lineFinished == true && hotkeyd == true && read.exitTime == false)
            {
                read.ResetScreen();
                read.SetDialogueBoxLocation();
                InvokeRepeating("ReadingMethod", 0.01f, read.readSpeed);
                currentlyReading = true;

                Debug.Log("Started reading");
            }
            else if (currentlyReading = true && read.lineFinished == false && hotkeyd == true && read.exitTime == false)
            {
                Cancel_Invoke();
                currentlyReading = false;
                read.lineFinished = true;
                SkippingMethod();
                print("Stopped reading: currentlyReading = " + currentlyReading + ", lineFinished = " + read.lineFinished);
            }
            else if (read.exitTime && hotkeyd == true)
            {
                read.ReleaseImageAndText();


            }
        }
        if (sansi != null)
        {
            sansi.CheckLimit();
            if (currentlyReading == false && sansi.lineFinished == true && hotkeyd == true && sansi.exitTime == false)
            {
                sansi.gameObject.GetComponent<SansMind>().ChangeEmotion();
                sansi.ResetScreen();
                sansi.SetDialogueBoxLocation();
                InvokeRepeating("SansiMethod", 0.01f, sansi.readSpeed);
                currentlyReading = true;

                Debug.Log("Started reading");
            }
            else if (currentlyReading = true && sansi.lineFinished == false && hotkeyd == true && sansi.exitTime == false)
            {
                Cancel_Sansi();
                currentlyReading = false;
                sansi.lineFinished = true;
                SansiSkippingMethod();
                print("Stopped reading: currentlyReading = " + currentlyReading + ", lineFinished = " + sansi.lineFinished);
            }
            else if (sansi.exitTime && hotkeyd == true)
            {
                sansi.ReleaseImageAndText();


            }
        }
        if (dialogue && hotkeyd)
        {

            if (dialogue.talking == false)
            {
                dialogue.EnableDialogueScreen();
                dialogue.talking = true;
            }
        }
        if(chita != null && hotkeyd == true)
        {
            chita.gameObject.GetComponent<AudioSource>().PlayOneShot(chita.gameObject.GetComponent<AudioSource>().clip, 0.5f);
        }
        
        hotkeyd = false;
    }

    public void Cancel_Invoke()
    {
        CancelInvoke("ReadingMethod");
    }

    public void Cancel_Sansi()
    {
        CancelInvoke("SansiMethod");
    }

    

    //This simply calls the function from the other class to read one letter aloud (just one) or to skip a line

    void ReadingMethod()
    {
        read.ReadAloud();
    }

    void SansiMethod()
    {
        sansi.ReadAloud();
    }
    void SansiSkippingMethod()
    {
        sansi.SkipLine();
    }

   

    void SkippingMethod()
    {
        read.SkipLine();
    }

    //When a trigger hits, it gets recorded in interactable and its linked Interacting class gets recorded in spotting

    Interacting read;

    Interacting_FaceMovement sansi = null;

    public ExitScript dialogue;

    Talk talking;

    GameObject chita;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "interactable")
        {
            read = collision.gameObject.GetComponent<Interacting>();
            Debug.Log(read.gameObject.name + " name is");
        }
        if (collision.tag == "interactableface")
        {
            sansi = collision.gameObject.GetComponent<Interacting_FaceMovement>();
            sansi.ShortenFont();
        }
        if (collision.tag == "dialogue")
        {
            Debug.Log("im here");
            dialogue = collision.gameObject.GetComponent<ExitScript>();
        }
        if (collision.tag == "talking00")
        {
            Debug.Log("im here");
            chita = collision.gameObject;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (read)
            read = null;

        if (dialogue)
            dialogue = null;

        if (sansi)
        {
            sansi.EnlargeFont();
            sansi = null;
        }

        if (chita)
            chita = null;

    }
}
