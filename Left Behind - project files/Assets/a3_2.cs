using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a3_2 : MonoBehaviour
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

        one.INT = -1;
        one.STRING = new string[] { "* You make small talk to find some answers.\n* You are ignored.9", "* It seems you need her in a complacent state before you can ask her anything.9" };
        inqu.INT = -1;
        inqu.STRING = new string[] { "* You ask her about her past.9" };

        paci.INT = -1;
        paci.STRING = new string[] { "* You inquire her on who she really is.9", "* You ask her what she is doing here.9", "* You ask about the scientist.9", "* You push forward for the truth.9" };

        take.INT = -1;
        take.STRING = new string[] { "* You try to ACT but an unknown force prevents you from doing so.9" };


    }

    struct saying
    {
        public int INT;
        public string[] STRING;
    };

    string GiveSaying(string[] saying, ref int integer)
    {
        integer++;
        if (integer == saying.Length) integer--;
        return saying[integer];
    }

    saying one;
    saying inqu;
    saying paci;
    saying take;


    string CalculateReply()
    {
        switch (tree.gameStage)
        {
            case "nothing":
                {
                    return "qqqq9";
                }
            case "stage1":
                {
                    return GiveSaying(one.STRING, ref one.INT);
                }
            case "pacifistLock":
                {
                    return GiveSaying(one.STRING, ref one.INT);
                }
            case "pacifistLock2":
                {
                    return GiveSaying(paci.STRING, ref paci.INT);
                }
            case "genocideLock":
                {
                    return GiveSaying(one.STRING, ref one.INT);
                }
            case "genocideLock2":
                {
                    return GiveSaying(one.STRING, ref one.INT);
                }
            case "genocideLock3":
                {
                    return GiveSaying(take.STRING, ref take.INT);
                }
            case "neutralLock":
                {
                    return GiveSaying(one.STRING, ref one.INT);
                }
            case "neutralLock2":
                {
                    return GiveSaying(take.STRING, ref take.INT);
                }
            default:
                {
                    Debug.Log("there is no way this is happening");
                    return "how did we even get here";
                }
        }
    }


    public string[] box_Dia1;

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

    public void OnClickA3_2()
    {

        string[] aux = tree.CalculateReply("Inquire");

        box.reply = CalculateReply(); //CalculateBox1(box_Dia1);

        /* for (int i = 0; i < aux.Length; i++)
         {
             bos.text[i] = aux[i];
         }*/
        bos.text = aux;

        box.allowed_to_type = true;
        box.special_interaction = true;
    }
}
