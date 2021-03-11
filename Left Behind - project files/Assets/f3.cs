using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f3 : MonoBehaviour
{
    public BoxAI box;
    public BoxAI2 box2;
    public BossAI boss;
    public DialogueTree tree;


    public void Heal03()
    {
        HP hp = GameObject.FindWithTag("Player").GetComponent<HP>();

        hp.health += hp.healing[3];

        GameObject q = GameObject.Find("UI manager");
        q.GetComponent<AudioSource>().PlayOneShot(q.GetComponent<Buttons>().healSound);
        q = null;

        string flavorText = "* You ate the Croquet Roll.\n* You are not sure how you got this.\n* You recovered 15 HP!9";


       
        boss.text = tree.CalculateReply("Food");

        if(tree.gameStage != "genocideLock3" && tree.gameStage != "neutralLock2")
        box2.reply = "* You remininsce about the roll you've just ate.\n* Delightful.";

        box.reply = flavorText;
        box.special_interaction = true;
        box.allowed_to_type = true;

        if (hp.health > 20)
        {
            hp.health = 20;
        }
        hp.bar.value = hp.health;
        hp.hpDisplay.text = (hp.health + "/20");
        GameObject.FindWithTag("f4").SetActive(false);
    }
}
