using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a2_2 : MonoBehaviour
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

        inqu.INT = -1;
        inqu.STRING = new string[] { "* You try to ACT but an unknown force prevents you from doing so." };

        geno.INT = -1;
        geno.STRING = new string[] { "* Your provocations have no effect.\n* She already is fighting at her maximum potential.9", "* You try to infuriate her further.\n* She doesn't even flinch.9", "She already is fighting at her maximum potential.\n* You can see her eyes burning.9" };
        paci.INT = -1;
        paci.STRING = new string[] { "* You point out that she is too slow for you.\n* Her attacks are now harder.", "* You try to rile her up.\n* Her attacks are now harder.9", "* She already is fighting at her best.\n* You can see her eyes burning.9" };
        neut.INT = -1;
        neut.STRING = new string[] { "* You begin to annoy her with pointless chatter.\n* Her attacks are now harder.9", "* You try to anger her even more...\n* Her attacks are now even more difficult.9", "* Your provocations don't affect her anymore.\n* You can see her eyes burning.9" };
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
    saying inqu;
    saying geno;
    saying paci;
    saying neut;

    public string[] box_Dia1;

    int currentLine = 0;

   /* string CalculateBox1(string[] dia)
    {
        int nrLines = dia.Length;

        if (currentLine < nrLines - 1)
        {

            string help = dia[currentLine];
            currentLine++;
            return help;
        }
        else return dia[nrLines - 1];
    }*/

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

    public Attacks atac;

    public void OnClickA2_2()
    {
        float localEnrage = atac.enrage;

        localEnrage += 0.1f;

        if (localEnrage > 0.4f) localEnrage = 0.4f;

        atac.enrage = localEnrage;

        string[] aux = tree.CalculateReply("Enrage");

        box.reply = CalculateReply();//CalculateBox1(box_Dia1);

        for (int i = 0; i < aux.Length; i++)
        {
            bos.text[i] = aux[i];
        }

        box.allowed_to_type = true;
        box.special_interaction = true;
    }
}
