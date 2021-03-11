using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    static bool being_used = false;

    public Vector3 teleportPosition;
    public GameObject blackFade;
    public bool doPlay;
    public bool doStop;

    // Start is called before the first frame update

    PMovement playerMovement;

    GameObject help;

    public int song;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (being_used == false)
        {
            being_used = true;
            blackFade = GameObject.FindWithTag("black");
            blackFade.GetComponent<CanvasGroup>().alpha = 0f;
            playerMovement = player.gameObject.GetComponent<PMovement>();
            help = player.gameObject;
            StartCoroutine("FadeInOut");
            if (doPlay == true)
            {
                AudioClips_L1 q = GameObject.FindWithTag("Music").GetComponent<AudioClips_L1>();
                try
                {
                    StartCoroutine(q.FadeAndSwitch(q.audioClips[song]));
                    
                }
                catch
                {
                    Debug.Log("No song object found");
                }
            }
            else if (doStop == true)
            {
                try
                {
                    AudioSource q = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
                    q.Stop();
                }
                catch
                {
                    Debug.Log("Couldnt find music to stop");
                }
            }
        }
    }

    //this might be the most scuffed thing i have ever produced but hey, it WORKS

    IEnumerator FadeInOut()
    {
        playerMovement.speed = 0f;
     //   playerMovement.enabled = !playerMovement.enabled;
        bool done = false;

     //   this checks to see if the alpha of the black GameObject is transparent, if it is it will darken it then check after incrementing it if it is == 1
     //   it will go back to 0 adn release the plalyer after teleporting him
        if(blackFade.GetComponent<CanvasGroup>().alpha < 1)
            while (blackFade.GetComponent<CanvasGroup>().alpha < 1 && done == false)
               {
                Debug.Log("alpha is " + blackFade.GetComponent<CanvasGroup>().alpha);
                blackFade.GetComponent<CanvasGroup>().alpha += 0.1f;

                yield return new WaitForSeconds(0.075f);

                if (blackFade.GetComponent<CanvasGroup>().alpha == 1) {
                    Transform playerPosition = help.gameObject.GetComponent<Transform>();
                    playerPosition.position = teleportPosition;
                    while (blackFade.GetComponent<CanvasGroup>().alpha > 0)
                    {
                        Debug.Log("alpha is " + blackFade.GetComponent<CanvasGroup>().alpha);
                        blackFade.GetComponent<CanvasGroup>().alpha -= 0.05f;

                        yield return new WaitForSeconds(0.05f);

                        if (blackFade.GetComponent<CanvasGroup>().alpha == 0)
                        {
                            done = true;
                            break;
                        }
                    }

                }
            }
        done = false;
        //playerMovement.enabled = !playerMovement.enabled;
        if(doPlay == true && song == 1)
        playerMovement.speed = 0.05f;
        else
        playerMovement.speed = 0.1f;
        being_used = false;
    }



}
