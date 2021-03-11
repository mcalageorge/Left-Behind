using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f2 : MonoBehaviour
{
    public BoxAI box;
    public BoxAI2 box2;
    public BossAI boss;
    public DialogueTree tree;

    public void Heal02()
    {
        HP hp = GameObject.FindWithTag("Player").GetComponent<HP>();

        hp.health += hp.healing[2];

        GameObject q = GameObject.Find("UI manager");
        q.GetComponent<AudioSource>().PlayOneShot(q.GetComponent<Buttons>().healSound);
        q = null;

        string flavorText = "* You ate the Hot Dog.\n* An actual hot dog made by Sans.\n* Your HP was maxed out!9";

        boss.text = tree.CalculateReply("Food");

        if (tree.gameStage != "genocideLock3" && tree.gameStage != "neutralLock2")
            box2.reply = "* You're still not sure the Hot Dog is actual meat.9";

        box.reply = flavorText;
        box.special_interaction = true;
        box.allowed_to_type = true;

        if (hp.health > 20)
        {
            hp.health = 20;
        }
        hp.bar.value = hp.health;
        hp.hpDisplay.text = (hp.health + "/20");
        GameObject.FindWithTag("f3").SetActive(false);
    }
}
