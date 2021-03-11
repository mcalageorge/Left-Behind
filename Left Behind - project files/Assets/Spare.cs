using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spare : MonoBehaviour
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

        sparing = false;
    }


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

    public bool sparing;

    public BoxAI2 box2;

    public void OnClickSpare()
    {

        string[] aux = tree.CalculateReply("Spare");
        Debug.Log("sparing is" + sparing);


        /*   switch (tree.gameStage)
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
                       return GiveSaying(paci.STRING, ref paci.INT);
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
                       return GiveSaying(geno.STRING, ref geno.INT);
                   }
               case "neutralLock":
                   {
                       return GiveSaying(neut.STRING, ref neut.INT);

                   }
               case "neutralLock2":
                   {
                       return GiveSaying(neut.STRING, ref neut.INT);
                   }
               default:
                   {
                       Debug.Log("there is no way this is happening");
                       return "how did we even get here";
                       break;
                   }
           }*/
        bos.text = aux;
        /*for (int i = 0; i < aux.Length; i++)
        {
            bos.text[i] = aux[i];
        }*/

        if (sparing == false)
        {
            box.reply = "* Your attempts at Sparing are denied.9";
            box2.reply = "* Sparing seems to be futile, right now.9";

            box.allowed_to_type = true;
            box.special_interaction = true;
        }
        else
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
