using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
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

    string[] lines = { "* The exit is blocked by a web too thick to move through.\n* You are trapped.9" };

    public void OnClickFlee()
    {
        string[] aux = tree.CalculateReply("Flee");

        box.reply = "* The exit is blocked by a web too thick to move through.\n* You are trapped.9";

       /* for (int i = 0; i < aux.Length; i++)
        {
            bos.text[i] = aux[i];
        }*/
        bos.text = aux;
        box.allowed_to_type = true;
        box.special_interaction = true;
    }
}
