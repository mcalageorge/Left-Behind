using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a0_2 : MonoBehaviour
{
    public GameObject boss;

    private BoxAI box;
    private BossAI bos;

    private DialogueTree tree;

    

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

    public void OnClickA0_2()
    {
        string[] aux = tree.CalculateReply("Check");

        //box.reply = "CHECKED YOUR ASS I DID"; //CalculateBox1(box_Dia1);

        // for (int i = 0; i < aux.Length; i++)
        //  {
        //   bos.text[i] = aux[i];
        // }
        bos.text = aux;
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
                    return "qqqq9";
                }
            case "stage1":
                {
                    return GiveSaying(neut.STRING, ref neut.INT);
                }
            case "pacifistLock":
                {
                    return GiveSaying(paci.STRING, ref paci.INT);
                }
            case "pacifistLock2":
                {

                    return GiveSaying(inqu.STRING, ref inqu.INT);
                }
            case "genocideLock":
                {
                    return GiveSaying(geno.STRING, ref geno.INT);
                }
            case "genocideLock2":
                {
                    return GiveSaying(geno.STRING, ref geno.INT);
                }
            case "genocideLock3":
                {
                    return GiveSaying(inqu.STRING, ref inqu.INT);
                }
            case "neutralLock":
                {
                    return GiveSaying(neut.STRING, ref neut.INT);

                }
            case "neutralLock2":
                {
                    return GiveSaying(inqu.STRING, ref inqu.INT);
                }
            default:
                {
                    Debug.Log("there is no way this is happening");
                    return "how did we even get here";
                }
        }

    }

    saying geno;
    saying paci;
    saying neut;

    saying inqu;

    void Start()
    {
        box = boss.GetComponent<BoxAI>();
        bos = boss.GetComponent<BossAI>();
        tree = boss.GetComponent<DialogueTree>();

        geno.INT = -1;
        geno.STRING = new string[] { "* Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* Her attacks are relentless.9" };
        paci.INT = -1;
        paci.STRING = new string[] { "* Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* She is clearly holding back on you.9" };
        neut.INT = -1;
        neut.STRING = new string[] { "* Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* She really is trying to kill you.9" };

        inqu.INT = -1;
        inqu.STRING = new string[] { "* You try to ACT but an unknown force prevents you from doing so." };

    }

    string GiveSaying(string[] saying, ref int integer)
    {
        integer++;
        if (integer == saying.Length) integer--;
        return saying[integer]; 
    }

    struct saying
    {
       public int INT;
       public string[] STRING;
    };

   /* int genINT = -1;
    string[] checkGenocide = new string[] { "Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* Her attacks are relentless, but her arms are very frail.9" };
    int neuINT = -1;
    string[] checkNeutral = new string[] { "Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* Having recently been awakened, she isn't fighting at her best.9" };
    int pacINT = -1;
    string[] checkPacifist = new string[] { "Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* She really is trying to kill you.9" };*/

}
