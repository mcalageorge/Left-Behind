using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f3_2 : MonoBehaviour
{
    public BoxAI box;
    public BoxAI2 box2;
    public BossAI boss;
    public DialogueTree tree;

    public void Heal032()
    {
        HP hp = GameObject.FindWithTag("Player").GetComponent<HP>();

        hp.health += hp.healing[7];

        GameObject q = GameObject.Find("UI manager");
        q.GetComponent<AudioSource>().PlayOneShot(q.GetComponent<Buttons>().healSound);
        q = null;



        string flavorText = "* You consume the leg.\n* Gross but highly nutritious.\n* Your HP was maxed out!9";
        if (tree.gameStage != "genocideLock3" && tree.gameStage != "pacifistLock2" && tree.gameStage != "neutralLock2")
        {
            if (box.legEaten == 0)
                boss.text = new string[] { "...9", "Did you just...9", "...9", "Eat my dead leg?9", "...9", "9" };
            else if (box.legEaten == 1)
                boss.text = new string[] { "...9", "What is wrong with you?9", "I'll have you know it takes quite some time for one of those to grow back.9", "9" };
            else if (box.legEaten == 2)
                boss.text = new string[] { "...9", "Disgusting...9", "I have no words for you.9", "9" };
            else
                boss.text = new string[] { "I really liked that arm...9", "I liked all those limbs you've eaten...9", "9", "9" };
        }
        else boss.text = tree.CalculateReply("Food");

            box.legEaten++;


            int ran = Random.Range(0, 2);
            if (ran == 0)
                box2.reply = "* You feel empty inside.9";
            else
                box2.reply = "* You can still feel it twitching inside your belly.9";


            box.reply = flavorText;
            box.special_interaction = true;
            box.allowed_to_type = true;

            if (hp.health > 20)
            {
                hp.health = 20;
            }
            hp.bar.value = hp.health;
            hp.hpDisplay.text = (hp.health + "/20");
            GameObject.FindWithTag("f8").SetActive(false);
        }
    }
