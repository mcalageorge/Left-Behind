using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a1 : MonoBehaviour
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

    public void OnClickA1()
    {
        string[] aux = tree.CalculateReply("Talk");

        //box.reply = "BRUH, YO THE PIZZA HERE"; //CalculateBox1(box_Dia1);

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
                    sayINT++;
                    if (sayINT == saying.Length) sayINT--;
                    return saying[sayINT];
        
    }
    
    int sayINT = -1;
    string[] saying = new string[] {"* You attempt to begin a conversation, with no luck.9", "* You attempt to begin a conversation.\n* It seems they aren't listening to you anymore.9" };

}
