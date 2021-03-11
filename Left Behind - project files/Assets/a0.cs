using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a0 : MonoBehaviour
{
    public GameObject boss;

    private BoxAI box;
    private BossAI bos;

    private DialogueTree tree;

    void Start()
    {
        box = boss.GetComponent<BoxAI>();
        bos = boss.GetComponent<BossAI>();
        tree = boss.GetComponent<DialogueTree>();
    }

    //public string[] box_Dia1;

    int currentLine = 0;

    string CalculateBox1(string[] dia)
    {
        int nrLines = dia.Length;

        if (currentLine < nrLines - 1)
        {

            string help = dia[currentLine];
            currentLine++;
            return help;
        }
        else return dia[nrLines - 1];
    }

    public void OnClickA0()
    {
         string[] aux = tree.CalculateReply("Check");

        //Debug.Log("aux is" + aux[0]);

        //bos.reply = tree.CalculateReply("Check");
        
        //CalculateBox1(box_Dia1);

        for (int i = 0; i < aux.Length; i++)
        {       
            bos.text[i] = aux[i];
        }

        box.reply = CalculateReply();

        box.allowed_to_type = true;
        box.special_interaction = true;

    }

    string CalculateReply()
    {
        switch (tree.gameStage)
        {
            case "nothing":
                {
                    return onCheckPhase1;
                }
            case "stage1":
                {
                    return onCheckPhase1;
                }
            case "pacifistLock":
                {
                    return "qqqq9";

                }
            case "pacifistLock2":
                {
                    return "qqqq9";
                }
            case "genocideLock":
                {
                    return "qqqq9";
                }
            case "genocideLock2":
                {
                    return "qqqq9";
                }
            case "genocideLock3":
                {
                    return "qqqq9";
                }
            case "neutralLock":
                {
                    return "qqqq9";

                }
            case "neutralLock2":
                {
                    return "qqqq9";
                }
            default:
                {
                    Debug.Log("there is no way this is happening");
                    return "how did we even get here";
                }
        }
    }

    string onCheckPhase1 = "* ??? - ??? ATK ??? DEF\n* Ominous figure wearing a cloak bearing the delta rune symbol.";
}
