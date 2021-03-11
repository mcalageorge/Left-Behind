using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    public GameObject eye;
    public Buttons butt;


    public void EnableFightInterface()
    {
        eye.gameObject.SetActive(true);
        butt.DisableF();
        butt.inMenu = false;
    }

  /*  public string playerChoice = "out";
    public string currentAction = "out";*/

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.F))
            playerChoice = "attack";
        if (Input.GetKeyDown(KeyCode.G))
            playerChoice = "act";
            */

       /* switch (playerChoice)
        {
            case "attack":
                {
                    Debug.Log("attacked");
                    playerChoice = "out";
                    break;
                }
            case "act":
                {
                    Debug.Log("acted");
                    currentAction = "dialogue";
                    break;
                }

            default:
                {
                    Debug.Log("standing by...");
                    playerChoice = "out";
                    break;
                }
        }*/


    }

}

