using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a3 : MonoBehaviour
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

    public void OnClickA3()
    {
        string[] aux = tree.CalculateReply("Shout");

        box.reply = "OOP I FORGOT ABOU THIS ONE"; //CalculateBox1(box_Dia1);

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
        string[] saying = new string[] { "* You call out to Sans.9\n* He doesn't seem to be around.9", "* You call out to Sans.\n* Only silence answers you.9" };
}
