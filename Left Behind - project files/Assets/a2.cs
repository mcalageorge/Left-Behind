using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a2 : MonoBehaviour
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


    public void OnClickA2()
    {
        string[] aux = tree.CalculateReply("Approach");

        box.reply = "SAY SOMETHING IM GIVING UP ON YOU";

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
    string[] saying = new string[] { "* You try to get closer...\n* You make very little progress because of the cobwebs in the room.9", "* You try to get closer...\n* The ground begins to shake and you fall backwards.9" };
}
