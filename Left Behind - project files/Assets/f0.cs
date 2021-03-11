using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f0 : MonoBehaviour
{
    public BoxAI box;
    public BoxAI2 box2;
    public BossAI boss;
    public DialogueTree tree;

    string[] answer2 = new string[] { "Wait, is that my sister's cooking?9", "Surely that couldn't be.9", "She wouldn't help the likes of you.9", "9" };

    public void Heal00()
    {
        HP hp = GameObject.FindWithTag("Player").GetComponent<HP>();
        GameObject q = GameObject.Find("UI manager");
        q.GetComponent<AudioSource>().PlayOneShot(q.GetComponent<Buttons>().healSound);
        q = null;
        hp.health += hp.healing[0];



        string flavorText = "* You ate the Spider Donut.\n* Delicious.\n* Your HP was maxed out!9";

        if (box.donutEaten == 0) { boss.text = new string[] { "That donut... Where did you get it?9", "It seems so familiar.9", "...9", "9" }; box.donutEaten++; }
        else boss.text = new string[] { "Wait, is that my sister's cooking?9", "Surely that couldn't be.9", "She wouldn't help the likes of you.9", "9" };


        switch (tree.gameStage)
        {
            case "genocideLock3":
                {
                    boss.text = tree.CalculateReply("Food");
                    break;
                }
            case "neutralLock2":
                {
                    boss.text = tree.CalculateReply("Food");
                    break;
                }
            default:
                {
                    box2.reply = boxos2;
                    break;
                }
        }

        box.reply = flavorText;
        box.special_interaction = true;
        box.allowed_to_type = true;

        if (hp.health > 20)
        {
            hp.health = 20;
        }
        hp.bar.value = hp.health;

        hp.hpDisplay.text = (hp.health + "/20");
        GameObject.FindWithTag("f1").SetActive(false);
    }
    string boxos2 = "* The donut's taste still lingers in your mouth.\n* ...Mmmm delicious.9";
}
