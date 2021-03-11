using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SansMind : MonoBehaviour
{
    bool pressed_Z;

    Vector3 sansPos;
    SpriteRenderer lookRotation;

    public Sprite sansToLeft;
    public Sprite sansToFront;

    void Start()
    {
        sansPos = this.gameObject.GetComponent<Transform>().position;
        lookRotation = this.gameObject.GetComponent<SpriteRenderer>();

      
    }

    int nPZ = 0;

    void Update()
    {
        //the 2 below if and 1 else rotate sans to player

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            pressed_Z = true;
            nPZ++;
        }
        else pressed_Z = false;

        /*if (help != null && pressed_Z == true)
        {
            playerPos = help.GetComponent<Transform>().position;

            if (playerPos.y >= 0.32f) lookRotation.sprite = sansToLeft;
            else lookRotation.sprite = sansToFront;
        }*/
    }

    //the below variable tells us from what face to start the conversation

    int dialogue_b = 0;
    Vector3 playerPos;
    GameObject help;

    public GameObject sansHead;
    public Sprite[] sansEmotions;
    public int[] emotionOrder;

    public int o = 0;


    public Interacting inter;

    int limit = 0;

    public void ChangeEmotion()
    {
        if (o == this.GetComponent<Interacting_FaceMovement>().dia_breakers[this.GetComponent<Interacting_FaceMovement>().dia_breakers.Length - 1])
        {
            o = this.GetComponent<Interacting_FaceMovement>().dia_breakers[this.GetComponent<Interacting_FaceMovement>().dia_breakers.Length - 2];
        }


             sansHead.gameObject.GetComponent<Image>().sprite = sansEmotions[emotionOrder[o]];
             o++;
    }

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            Vector3 playerPos;
            help = player.gameObject;
            sansHead.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D player)
    {
        sansHead.SetActive(false);
    }


}
