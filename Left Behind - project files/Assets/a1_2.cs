using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a1_2 : MonoBehaviour
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
        one.STRING = new string[] { "* You try to calm her down.\n* She ignores you, but you can see her expresion change.9", "* You try to apoligise.\n* She tries to ignore you, but gets angry instead.9", "* You try your best to soothe the situation.\n* But you don't get very far.9" };
        geno.INT = -1;
        geno.STRING = new string[] { "* You apologise for hurting her...\n* She doesn't care about anything you say.9", "* You seek to remedy the situation.\n* You are ignored.9" };
        paci.INT = -1;
        paci.STRING = new string[] { "* You try your best to calm her down.\n* She ignores you, but you can see her expression change." , "* You try to empathize with her.\n* She ignores you, but you can start to see visible anger on her face.9" };
        neut.INT = -1;
        neut.STRING = new string[] { "* You attempt to calm her down.\n* She doesn't stop, she isn't listening to you.9", "* You attempt to calm her down.\n* It appears that your words are slowly getting to her.9" };

        inqu.INT = -1;
        inqu.STRING = new string[] {"* You try to ACT but an unknown force prevents you from doing so.9" };

        Debug.Log("string is " + one.STRING[0]);
    }
    struct saying
    {
        public int INT;
        public string[] STRING;
    };

    saying inqu;
    saying one;
    saying geno;
    saying paci;
    saying neut;
    
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
        string[] aux = tree.CalculateReply("Pacify");

        box.reply = CalculateReply();

       /* for (int i = 0; i < aux.Length; i++)
        {
            bos.text[i] = aux[i];
        }*/
        bos.text = aux;

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
                    return GiveSaying(one.STRING, ref one.INT);
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


    string GiveSaying(string[] saying, ref int integer)
    {
        integer++;
        if (integer == saying.Length) integer--;
        Debug.Log("integer is " + integer + " length is " + saying.Length);
        return saying[integer];
    }

    /*string[] pacifyOne = new string[] { "* You try to calm her down.\n* She ignores you, but you can see her expresion change.9", "* You try to apoligise.\n* She ignores you, but you can start to\nsee visible anger on her face.9", "* You try your best to soothe the situation.\n* But you don't get very far.9" };

    string[] pacifyGenocide = new string[] { "* You apologise for hurting her...\n* She doesn't care about anything you say.9", "* You seek to remedy the situation.\n* You are ignored." };
    string pacifyNeutral =  "Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* Having recently been awakened, she isn't fighting at her best.9" ;
    string pacifyPacifist =  "Ebrima - 40 ATK 40 DEF\n* Former royal guard.\n* She really is trying to kill you." ;*/
}
