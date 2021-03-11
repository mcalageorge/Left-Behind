using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attacks : MonoBehaviour
{
    public Buttons buttons;

    public int round = 0;

    public BoxAI2 box2;

    public GameObject RockAttackToRight;
    public Vector3[] RockAttackToRight_pos;

    public int rightRockDmg;
    public int fallRockDmg;
    public int followPointRockDmg;
    public int pillarDmg;
    public int phyDmg;

    BoxAI box;

    public SetHeartColor coloring;

    public float enrage = 7;

    void Start()
    {
        box = this.gameObject.GetComponent<BoxAI>();
        box2 = this.gameObject.GetComponent<BoxAI2>();

        //Attack();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(FinalAttack());
        }
    }

    public bool swipe = false;
    public bool six = false;
    public bool fan = false;
    public bool rightAttack = false;
    public bool circleToPlayer = false;
    public bool circleToPoint = false;
    public bool rain = false;
    public bool pillarAttack = false;
    public bool extendo = false;
    public bool piatra = false;

    public AudioClip[] sound_effects;

    IEnumerator FinalAttack()
    {
        StartCoroutine(Six(enrage, 8, 1, true, 2f));

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);
        StartCoroutine(Swipe(3, false, enrage, 0.8f, 0));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        Debug.Log("attack 1 ended");

        
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(RightAttack(5, enrage, 0.95f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("attack 2 ended");

        

        Debug.Log("attack 3 ended");
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Fan(1.8f, 6, 2, enrage, -1, true, 300));
        while (fan == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        Debug.Log("attack 4 ended");

        StartCoroutine(CircleToPlayer(1, 20, enrage, 4.5f));
        while (circleToPlayer == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("attack 5 ended");
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Rain(enrage, 0.1f, 100));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        Debug.Log("attack 6 ended");
        
        StartCoroutine(Pillar(10, 0.6f, enrage, 1.5f, 1.5f));
        while (pillarAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        Debug.Log("attack 7 ended");
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);
        StartCoroutine(Extendo(enrage));
        

        Debug.Log("attack 8 ended");
        StartCoroutine(CaderePiatraFizica(35));
        while (piatra == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("attack 9 ended");

        StartCoroutine(CircleToPoint(1, 10, enrage, 3.6f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("attack 5 ended");

        yield return new WaitForSeconds(2f);

        box2.afterbossSpeech = true;

        Debug.Log("attacks ended time for box2 to commence");
    }

    public Animator cameraShake;

    public void ShakeCamera()
    {
        cameraShake.SetTrigger("shakeTrig");
    }

    public bool kill_player_now = false;

    int g = 1;


    public Text debugText;

    public bool attacked_first = false;
    bool already_attacked = false;

    public bool sans_kill_player_now = false;

    int attack_nr = 1;

    public bool test = false;

    public bool final_attack = false;

    bool finaled = false;

    public void Attack()
    {

        if (test == false)
        {
            if (kill_player_now == true)
            {
                StartCoroutine(OneShot());
            }
            else if (sans_kill_player_now == true)
            {
                StartCoroutine(Bony());
            }
            else if (final_attack == true && finaled == false)
            {
                StartCoroutine(FinalAttack());
                finaled = true;
            }
            else
            {
                if (round < 5 && attacked_first == true && already_attacked == false)
                {
                    StartCoroutine(AttackRetaliation());

                    already_attacked = true;
                    round = 5;
                    enrage = 0.4f;
                }
                else { StartCoroutine("Attack" + attack_nr.ToString()); attack_nr++; Debug.Log("ATTACK NUMBER IS: " + (attack_nr - 1)); }
            }
        }
        else StartCoroutine(TestAttack());
    }

   IEnumerator TestAttack()
    {

        
        StartCoroutine(RightAttack(1, enrage, 6));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        box2.afterbossSpeech = true;
    }

    public IEnumerator FakeAttack()
    {
        Debug.Log("Fake Attack Commence");
        yield return new WaitForSeconds(2f);
        box2.afterbossSpeech = true;
    }

    public IEnumerator AttackRetaliation()
    {
        
        StartCoroutine(RightAttack(6, enrage, 1.2f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Six(enrage, 9f, 2, true, 3f));
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Rain(enrage));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;
    }

    //rightattack Easy
    public IEnumerator Attack1()
    {
        StartCoroutine(RightAttack(5, enrage, 1.2f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);

        box2.afterbossSpeech = true;
    }

    // six easy
    public IEnumerator Attack2()
    {
        StartCoroutine(Six(enrage, 8.7f, 2, false, 1.6f));
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(3f);

        box2.afterbossSpeech = true;
    }

    // rain easy
    public IEnumerator Attack3()
    {
        

        StartCoroutine(CircleToPoint(2, 10, enrage, 2f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2.5f);

        box2.afterbossSpeech = true;
    }

    // right attack and six medium
    public IEnumerator Attack4()
    {
        StartCoroutine(RightAttack(4, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("right attack is done");

        StartCoroutine(Six(enrage, 7.5f, 1, true, 3f));

        
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("six is done");

        yield return new WaitForSeconds(2f);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack5()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        yield return new WaitForSeconds(0.6f);
        
        StartCoroutine(CaderePiatraFizica(35));
        while (piatra == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack6()
    {
        StartCoroutine(CircleToPlayer(2, 10, enrage, 4));
        while (circleToPlayer == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Fan(1.75f, 6, 2, enrage, 1, true, 200));
        while (fan == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);
        box2.afterbossSpeech = true;
    }

    public Animator boss_animations;

    public IEnumerator Attack7()
    {
        StartCoroutine(Cobweb());
        while (cob == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        yield return new WaitForSeconds(2f);

        StartCoroutine(Extendo(enrage));
        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        boss_animations.SetBool("isExtended", false);
        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack8()
    {
        StartCoroutine(CircleToPoint(3, 10, enrage, 1f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);
       
        StartCoroutine(Extendo(enrage));
        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);
        box2.afterbossSpeech = true;
    }

    bool grabbed_at = false;

    public IEnumerator Attack9()
    {
        StartCoroutine(Pillar(13, 0.3f, enrage, 2f, 1.5f));
        while (pillarAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (this.gameObject.GetComponent<DialogueTree>().hits == 4)
        {
            Debug.Log("we get here at");
            box2.blockedButtons[0] = 1;
            box2.blockedButton = "fight";
             grabbed_at = true;
        }
        else
        {
            Debug.Log("we get here it");
            box2.blockedButtons[1] = 1;
            box2.blockedButton = "item";
            grabbed_at = false;
        }


        Debug.Log("pillar attack annd web deployed succesfully");

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack10()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Rain(enrage, 0.15f, 100));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (grabbed_at == true)
            box2.blockedButtons[0] = 0;
        else
            box2.blockedButtons[1] = 0;



        StartCoroutine(Extendo(enrage));
        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack11()
    {
        boss_animations.SetBool("isExtended", false);

        yield return new WaitForSeconds(2f);
        StartCoroutine(MovePlayerDown());
        while (tel == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(4f);

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(Swipe(5, true, enrage, 1f, 1f));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        StartCoroutine(RightAttack(5, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1.5f);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack12()
    {
        StartCoroutine(CircleToPoint(3, 15, enrage, 3.5f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(RightAttack(3, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    //teleport stops

    public IEnumerator Attack13()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(Rain(enrage, 0.1f, 250));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);
        SetHeartColor changeCol = player.GetComponent<SetHeartColor>();
        changeCol.UpdateColor(changeCol.mainColor);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack14()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Swipe(7, false, enrage, 2f, 1f));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Fan(1.5f, 6, 2, enrage, 1, true, 325));
        while (fan == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        StartCoroutine(Pillar(6, 0.6f, enrage, 2, 1.5f));
        while (pillarAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack15()
    {
        StartCoroutine(RightAttack(5, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
    

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(CaderePiatraFizica(40));
        while (piatra == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;
    }
    public IEnumerator Attack16()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        StartCoroutine(Pillar(12, 0.6f, enrage, 2.5f, 1.5f));
        while (pillarAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);


        StartCoroutine(RightAttack(5, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack17()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        if (this.gameObject.GetComponent<DialogueTree>().hits == 6)
        {
            box2.blockedButtons[0] = 1;
            grabbed_at = true;
            box2.blockedButton = "fight";
        }
        else
        {
            box2.blockedButtons[1] = 1;
            grabbed_at = false;
            box2.blockedButton = "item";
        }


        StartCoroutine(Cobweb());
        while (cob == true)
        {
            yield return new WaitForSeconds(0.1f);
        }


        StartCoroutine(Rain(enrage, 0.1f, 60));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack18()
    {

        StartCoroutine(CaderePiatraFizica(45));
        while (piatra == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack19()
    {

        StartCoroutine(Six(enrage, 8f, 1, true, 3f));


        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }


        StartCoroutine(Extendo(enrage));

        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        



        if (grabbed_at == true)
            box2.blockedButtons[0] = 0;
        else
            box2.blockedButtons[0] = 0;

        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack20()
    {
        StartCoroutine(CircleToPoint(3, 15, enrage, 3.2f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(CircleToPlayer(3, 20, enrage, 4f));
        while (circleToPlayer == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack21()
    {
        StartCoroutine(Extendo(enrage));

        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Swipe(4, false, enrage, 1f, 1f));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack22()
    {
        StartCoroutine(MovePlayerDown());
        while (tel == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        
        StartCoroutine(Fan(1.5f, 6, 2, enrage, 1, true, 300));
        while (fan == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(Extendo(enrage));
        while (extendo == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack23()
    {
        StartCoroutine(CircleToPoint(3, 15, enrage, 4.3f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(RightAttack(5, enrage, 1.1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack24()
    {
        StartCoroutine(Six(enrage, 9f, 2, true, 2));
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);

        if (this.gameObject.GetComponent<DialogueTree>().hits == 7)
        {
            box2.blockedButtons[0] = 1;
            grabbed_at = true;
            box2.blockedButton = "fight";
        }
        else
        {
            box2.blockedButtons[1] = 1;
            grabbed_at = false;
            box2.blockedButton = "item";
        }

        StartCoroutine(Swipe(5, false, enrage, 1f, 1f));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack25()
    {
        StartCoroutine(Rain(enrage, 0.1f, 60));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (grabbed_at == true)
            box2.blockedButtons[0] = 0;
        else
            box2.blockedButtons[1] = 0;

        boss_animations.SetBool("isExtended", false);

        StartCoroutine(Six(enrage, 4, 1, false, 3f));
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack26()
    {

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(Swipe(4, false, enrage, 1f, 1f));
        while (swipe == true)
        {
            yield return new WaitForSeconds(0.1f);
        }


        boss_animations.SetBool("isExtended", false);

        StartCoroutine(RightAttack(4, enrage, 1f));
        while (rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack27()
    {
        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(Fan(1.5f, 6, 2, enrage, 1, true, 225));
        while (fan == true)
        {
            yield return new WaitForSeconds(0.1f);
        }



        StartCoroutine(Rain(enrage, 0.1f, 60));
        while (rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }
        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack28()
    {
        StartCoroutine(MovePlayerDown());
        while (tel == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(CircleToPlayer(2, 20, enrage, 3.6f));
        while (circleToPlayer == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(CircleToPoint(2, 20, enrage, 3.6f));
        while (circleToPoint == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack29()
    {


        StartCoroutine(Six(enrage, 6, 2, false, 3f));
        while (six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(CircleToPlayer(1, 15, enrage, 4));
        while (circleToPlayer == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        box2.afterbossSpeech = true;
    }

    public IEnumerator Attack30()
    {
        StartCoroutine(Pillar(15, 0.6f, enrage, 2.5f, 2f));
        while (pillarAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetTrigger("extend");
        boss_animations.SetBool("isExtended", true);


        StartCoroutine(CaderePiatraFizica(30));
        while (piatra == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        boss_animations.SetBool("isExtended", false);

        box2.afterbossSpeech = true;

        attack_nr = 20;
    }

    public IEnumerator Attack31()
    {
        StartCoroutine(OneShot());

        yield return new WaitForSeconds(5f);

        kill_player_now = true;



    }



    string auxDir;

    string previous = "none";

    public Sprite[] rightrockSprites;

    /// <summary>
    /// max speed 1.1 or 1.2 if you feel adventurous
    /// </summary>
    /// <param name="noAttacks"></param>
    /// <param name="enrage"></param>
    /// <param name="speed"></param>
    /// <returns></returns>
    /// 
    public AudioClip spear_throw;

   public IEnumerator RightAttack(int noAttacks = 3, float enrage = 0.4f, float speed = 1.1f)
    {
        rightAttack = true;

        int missing_rock;
        Debug.Log("Attack: RightAttack");

        for (int j = 0; j < noAttacks; j++)
        {

            int direction = Random.Range(0, 3);

            if (direction == 0)
            {
                direction = 0; auxDir = "right"; previous = "right;";
            }
          /*  else if (direction == 1)
            {
                direction = 4; auxDir = "upwards"; previous = "upwards;";
            }*/
            else if (direction == 1)
            {
                direction = 4; auxDir = "left"; previous = "left;";
            }
            else if (direction == 2)
            {
                direction = 8; auxDir = "downwards"; previous = "downwards";
            }

            missing_rock = Random.Range(direction, direction + 4);

            GameObject aux;

            this.GetComponent<AudioSource>().clip = spear_throw;
            this.GetComponent<AudioSource>().volume = 0.5f;
            this.GetComponent<AudioSource>().Play();

            for (int i = direction; i < direction + 4; i++)
            {
                int ran_sprite = Random.Range(0, rightrockSprites.Length);

                if (i != missing_rock)
                {
                    aux = Instantiate(RockAttackToRight, RockAttackToRight_pos[i], Quaternion.identity);
                    aux.gameObject.GetComponent<RockAttackFromRightMove>().direction = auxDir;
                    aux.gameObject.GetComponent<RockAttackFromRightMove>().speed = speed + speed * enrage;
                    aux.gameObject.GetComponent<SpriteRenderer>().sprite = rightrockSprites[ran_sprite];
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
        rightAttack = false;

    }

    Transform transform;

    public GameObject rock;

    Vector3 initial_rotation;

    float degree_rotation;

    float distance = 5f;

    public Transform dummy;

    public GameObject pivot;


    /// <summary>
    /// max speed of 6
    /// </summary>
    /// <param name="nrOfWaves"></param>
    /// <param name="nrOfRocks"></param>
    /// <param name="enrage"></param>
    /// <param name="speed"></param>
    /// <returns></returns>

   public IEnumerator CircleToPlayer(int nrOfWaves = 1, int nrOfRocks = 15, float enrage = 0.4f, float speed = 6f)
    {

        

        
        circleToPlayer = true;

        float degree_rotation = 360f / nrOfRocks;

        Debug.Log("Attack: CircleToPlayer");
        for (int j = 0; j < nrOfWaves; j++)
        {
            float z = Random.Range(0f, 361f);
            Debug.Log(z + " Z is");
            pivot.GetComponent<Transform>().Rotate(0, 0, z);

            for (int i = 0; i < nrOfRocks; i++)
            {
                GameObject q = Instantiate(rock, dummy.position, Quaternion.identity);

                pivot.GetComponent<Transform>().Rotate(0, 0, -180);

                q.GetComponent<LookAndMoveToPlayer>().targetPos = dummy.position;
                q.GetComponent<LookAndMoveToPlayer>().speed = speed + speed * enrage;

                pivot.GetComponent<Transform>().Rotate(0, 0, 180 + degree_rotation);

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(4f);
        }
        circleToPlayer = false;
    }

    /// <summary>
    /// max speed of 4.7f
    /// </summary>
    /// <param name="nrOfWaves"></param>
    /// <param name="nrOfRocks"></param>
    /// <param name="enrage"></param>
    /// <param name="speed"></param>
    /// <returns></returns>

    public IEnumerator CircleToPoint(int nrOfWaves = 1, int nrOfRocks = 15, float enrage = 0.4f, float speed = 4.5f)
    {
        circleToPoint = true;

        float degree_rotation = 360f / nrOfRocks;

        Debug.Log("Attack: CircleToPoint");
        for (int j = 0; j < nrOfWaves; j++)
        {
            float z = Random.Range(0f, 361f);
            Debug.Log(z + " Z is");
            pivot.GetComponent<Transform>().Rotate(0, 0, z);

            for (int i = 0; i < nrOfRocks; i++)
            {
                GameObject q = Instantiate(rock, dummy.position, Quaternion.identity);

                pivot.GetComponent<Transform>().Rotate(0, 0, -180);

                q.GetComponent<LookAndMoveToPlayer>().targetPos = dummy.position;
                q.GetComponent<LookAndMoveToPlayer>().speed = speed + speed * enrage;
                q.GetComponent<LookAndMoveToPlayer>().moveToPoint = true;

                pivot.GetComponent<Transform>().Rotate(0, 0, 180 + degree_rotation);

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(4f);
        }
        circleToPoint = false;
    }

    float sixSpeed = 5f;
    public Vector3[] sixPosX;
    public float[] sixPosX2;
    public Sprite[] sixSprites;

    /// <summary>
    /// max speed of 9
    /// </summary>
    /// <param name="enrage"></param>
    /// <param name="speed"></param>
    /// <param name="nrWaves"></param>
    /// <param name="bottom"></param>
    /// <param name="delayBetweenAttacks"></param>
    /// <returns></returns>

    public IEnumerator Six(float enrage = 0.4f, float speed = 9f, int nrWaves = 2, bool bottom = true, float delayBetweenAttacks = 3f)
    {
        six = true;

        Debug.Log("Attack: Six");
        for (int j = 0; j < nrWaves; j++)
        {

            for (int i = 0; i < 6; i++)
            {
                GameObject q = Instantiate(rock, new Vector3(sixPosX[i].x, sixPosX[i].y, -8), Quaternion.identity);
                q.GetComponent<LookAndMoveToPlayer>().speed = speed + speed * enrage;
                q.GetComponent<LookAndMoveToPlayer>().moveToPoint = false;
                int ran = Random.Range(0, sixSprites.Length);
                q.GetComponent<SpriteRenderer>().sprite = sixSprites[ran];
                q.GetComponent<Transform>().localScale = new Vector3(4.3f, 4.3f, 4.3f);


                yield return new WaitForSeconds(0.4f);
            }
            Debug.Log("done with first part of 6");
            if (bottom == true)
            {
                yield return new WaitForSeconds(1.2f);

                for (int i = 3; i >= 0; i--)
                {
                    GameObject q = Instantiate(rock, new Vector3(sixPosX2[i], -3.77f, -8), Quaternion.identity);
                    q.GetComponent<LookAndMoveToPlayer>().speed = speed + speed * enrage;
                    q.GetComponent<Transform>().localScale = new Vector3(3, 3, 3);
                    q.GetComponent<LookAndMoveToPlayer>().moveToPoint = false;
                    int ran = Random.Range(0, sixSprites.Length);
                    q.GetComponent<SpriteRenderer>().sprite = sixSprites[ran];

                    yield return new WaitForSeconds(0.8f);
                }
            }
            yield return new WaitForSeconds(delayBetweenAttacks);
            
        }
        Debug.Log("done with second part of 6");
        six = false;
    }

    ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// ////////////////// box2.afterbossSpeech = true;
    public Vector3[] armPositions;
    public GameObject extendoArm;
    public int extendoDmg = 5;

    public Knob_MvFlash usableArms;


    public IEnumerator Extendo(float enrage = 0.4f)
    {
        float speed;


        if (enrage == 0.4) speed = 0.2f;
        else if (enrage == 2) speed = 0.35f;
        else if (enrage == 3) speed = 0.6f;
        else speed = 1f;

        extendo = true;
        Debug.Log("Attack: Extendo");

        if (extendoArm != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if (usableArms.legs[i].activeSelf == true)
                {
                    GameObject q = Instantiate(extendoArm, armPositions[i], this.gameObject.transform.rotation);
                    yield return new WaitForSeconds(speed);
                }
            }
            for (int i = 3; i < 6; i++)
            {
                if (usableArms.legs[i].activeSelf == true)
                {
                    GameObject q = Instantiate(extendoArm, armPositions[i], this.gameObject.transform.rotation);

                    q.transform.Rotate(0, 0, 180f);
                    yield return new WaitForSeconds(speed);
                }
            }
        }
        yield return new WaitForSeconds(4f);
        extendo = false;
    }

    public GameObject fallRock;
    public Sprite[] sprites;
    public float[] pointsX;

    Vector2 range = new Vector2(-6f, 6f);

    public AudioClip earthquake;
    public GameObject rainLimit;

    public IEnumerator Rain(float enrage = 0.4f, float waitTime = 0f, int nrOfRocks = 0)
    {     
        rain = true;
 
        if (waitTime == 0 && nrOfRocks == 0)
        {
            if (enrage == 0f) { nrOfRocks = 100; waitTime = 0.1f; }
            else if (enrage == 0.1f) { nrOfRocks = 150; waitTime = 0.1f; }
            else if (enrage == 0.2f) { nrOfRocks = 200; waitTime = 0.08f; }
            else { nrOfRocks = 230; waitTime = 0.05f; }
        }

        Debug.Log("Attack: Rain");
        this.GetComponent<AudioSource>().clip = earthquake;
        this.GetComponent<AudioSource>().volume = 0.5f;
        this.GetComponent<AudioSource>().Play();

        Instantiate(rainLimit, new Vector3(0, -6.15f, 0 ) , Quaternion.identity);

        for (int j = 0; j < nrOfRocks; j++)
        {
            float rndPos = Random.Range(range.x, range.y);
            int rndSprite = Random.Range(0, 3);
            fallRock.GetComponent<SpriteRenderer>().sprite = sprites[rndSprite];
            Vector3 spawnPosition = new Vector3(rndPos, 7, -8);
            Instantiate(fallRock, spawnPosition, this.gameObject.transform.rotation);

            if(j % 3 == 0)
                ShakeCamera();
            yield return new WaitForSeconds(waitTime);
        }
        for(float i = 0.5f; i > -0.02; i -= 0.02f)
        {
            Debug.Log("volume for avalanche is " + i);
            this.GetComponent<AudioSource>().volume = i;
            yield return new WaitForSeconds(0.05f);
        }
        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().volume = 1f;
        rain = false;
    }

    public Vector3[] pillarPos;
    public GameObject pillar;
    public GameObject pillarKiller;

    /// <summary>
    /// max speed of 9
    /// </summary>
    /// <param name="pillarNr"></param>
    /// <param name="maxLength"></param>
    /// <param name="enrage"></param>
    /// <param name="speed"></param>
    /// <param name="delay"></param>
    /// <returns></returns>

    public IEnumerator Pillar(int pillarNr = 15, float maxLength = 0.6f, float enrage = 0.4f, float speed = 9f, float delay = 2f)
    {
        pillarAttack = true;
        

        GameObject pilkil = Instantiate(pillarKiller, new Vector3(0, -1.55f, -8f), Quaternion.identity);
        for (int i = 0; i < pillarNr; i++)
        {
            int rndPos = Random.Range(0, 4);

            GameObject aux = Instantiate(pillar, pillarPos[rndPos], this.gameObject.transform.rotation);
            GameObject auxChild = aux.gameObject.transform.GetChild(0).gameObject;

            Debug.Log("maxLength is " + maxLength);

            switch (rndPos)
            {
                case 0:
                    {
                        string[] directions = { "left", "up" };
                        float[] rotations = { -90f, 0f };
                        RotateChildSetParentDirectionSetSpeedSetLength(aux, auxChild, directions, rotations, 0, speed + speed * enrage, maxLength);
                        Debug.Log("Spawned: bottomright");
                        break;
                    }
                case 1:
                    {
                        string[] directions = { "left", "down" };
                        float[] rotations = { 90f, 0f };
                        RotateChildSetParentDirectionSetSpeedSetLength(aux, auxChild, directions, rotations, 2, speed + speed * enrage, maxLength);
                        Debug.Log("Spawned: topright");
                        break;
                    }
                case 2:
                    {
                        string[] directions = { "right", "down" };
                        float[] rotations = { 90f, 180f };
                        RotateChildSetParentDirectionSetSpeedSetLength(aux, auxChild, directions, rotations, 4, speed + speed * enrage, maxLength);
                        Debug.Log("Spawned: topleft");
                        break;
                    }
                case 3:
                    {
                        string[] directions = { "right", "up" };
                        float[] rotations = { -90f, -180f };
                        RotateChildSetParentDirectionSetSpeedSetLength(aux, auxChild, directions, rotations, 6, speed + speed * enrage, maxLength);
                        Debug.Log("Spawned: bottomleft");
                        break;
                    }
            }
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2f);

        Destroy(pilkil.gameObject, 10f);

        Debug.Log("getting out of pillar");
        pillarAttack = false;
    }

    private void RotateChildSetParentDirectionSetSpeedSetLength(GameObject parent, GameObject child, string[] directions, float[] rotations, int startRnd, float maxSpeed, float maxLength)
    {
        int rndDirection = Random.Range(startRnd, startRnd + 2);
        Move pan = parent.gameObject.GetComponent<Move>();
        float rndSpeedProcentage = Random.Range(80, 99);
        float rndLengthProcentage = Random.Range(30, 99);
        float lenX = rndLengthProcentage / 100 * maxLength;

        Debug.Log("rndLengthProc = " + rndLengthProcentage);



        pan.speed = (rndSpeedProcentage / 100) * maxSpeed;
        pan.speed = 1 / (pan.speed) * 2.5f;
        Vector3 scaleChange = new Vector3(lenX, 0, 0);
        Debug.Log("lenX " + lenX);
        pan.gameObject.transform.localScale += scaleChange;

        if (rndDirection == startRnd)
        {
            pan.gameObject.transform.Rotate(0, 0, rotations[0]);
            pan.direction = directions[0];
        }

        else
        {
            pan.gameObject.transform.Rotate(0, 0, rotations[1]);
            pan.direction = directions[1];
        }
    }

    public GameObject teleporter;
    public GameObject arena;
    bool teleporterActive = false;

    bool tell = false;

    public IEnumerator Teleport()
    {
        tell = true;

        teleporterActive = true;

        SpriteRenderer alpha = arena.gameObject.GetComponent<SpriteRenderer>();

        float max = 1f;

        for (int i = 0; i < 75; i++)
        {
            max -= 0.01f;
            alpha.color = new Color(1f, 1f, 1f, max);
            yield return new WaitForSeconds(0.015f);
        }
        player.GetComponent<MoveSoul>().speed *= 2f;
        Transform arenaTransform = arena.gameObject.GetComponent<Transform>();
        Instantiate(teleporter, new Vector3(0, 0, 0)/*arena.gameObject.GetComponent<Transform>().position*/, arena.gameObject.GetComponent<Transform>().rotation);

        tell = false;
    }

    public GameObject cobweb;

    bool cob = false;

    public IEnumerator Cobweb()
    {
        cob = true;
        yield return new WaitForSeconds(0.01f);
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<MoveSoul>().speed /= 2f;

        Transform arenaTransform = arena.gameObject.GetComponent<Transform>();
        Vector3 pos = new Vector3(arenaTransform.position.x, arenaTransform.position.y, -0.01f);

        Instantiate(cobweb, pos, arenaTransform.rotation);

        cob = false;
    }

    public HandAnimation boss_anim;
    public GameObject player;
    public GameObject wave;

    public void TelegrafTeleport()
    {

        StartCoroutine(MovePlayerDown());
    }

    bool tel = false;


    IEnumerator MovePlayerDown()
    {
        tel = true;

        StartCoroutine(Teleport());


        yield return new WaitForSeconds(1f);


        boss_anim.AttackAnimation();
        SetHeartColor changeCol = player.GetComponent<SetHeartColor>();

        changeCol.UpdateColor(changeCol.teleportedColor);

        Debug.Log("we started moving the player");

        Vector3 pos = new Vector3(0f, 1f, -7f);



        yield return new WaitForSeconds(0.75f);

        GameObject w = Instantiate(wave, pos, wave.gameObject.GetComponent<Transform>().rotation);
        Transform q = w.GetComponent<Transform>();
        int i = 0;

        Vector3 scaleChange = new Vector3(0.1f, 0.1f, 0.1f);

        while (i < 25)
        {
            q.Translate(0, -0.3f, 0);
            q.localScale += scaleChange;
            i++;
            player.GetComponent<Transform>().Translate(0, -0.1f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(w);
        box2.reply = "* You suddenly feel very brave.9";
        tel = false;
    }

    public GameObject PiatraFizica;
    public GameObject ArenaFizica;
    public GameObject wall;
    private float[] minmaxPoints = { (-1.4f), (1.4f) };
    public Sprite[] piatraFizicaSprites;

    public IEnumerator CaderePiatraFizica(int numar_de_pietre = 20)
    {
        piatra = true;
        Debug.Log("Attack: CaderePiatraFizica");

        ArenaFizica.SetActive(true);
        wall.gameObject.SetActive(true);

        this.GetComponent<AudioSource>().clip = earthquake;
        this.GetComponent<AudioSource>().volume = 0.5f;
        this.GetComponent<AudioSource>().Play();

        for (int i = 0; i < numar_de_pietre; i++)
        {
            Vector3 pozitie_auxiliara = new Vector3(Random.Range(minmaxPoints[0], minmaxPoints[1]), 6f, -9f);
            GameObject q = Instantiate(PiatraFizica, pozitie_auxiliara, gameObject.GetComponent<Transform>().rotation);

            q.GetComponent<SpriteRenderer>().sprite = piatraFizicaSprites[Random.Range(0, piatraFizicaSprites.Length)];

            if (i % 3 == 0)
                ShakeCamera();

            yield return new WaitForSeconds(0.5f);
        }
        ArenaFizica.SetActive(false);
        
        Debug.Log("we exited CaderePiatraFizica");
        yield return new WaitForSeconds(1.5f);
        wall.gameObject.SetActive(false);
        this.GetComponent<AudioSource>().Stop();
        piatra = false;
    }

    public GameObject rotatingLeg;
    public GameObject[] legs = new GameObject[6];
    public Vector3[] legs_initPos;

    public IEnumerator Fan(float speed = 1f, int nr_legs = 6, int damage = 1, float enrage = 0.4f, int direction = 1, bool changeDirection = true, int rotate_points = 300)
    {
        fan = true;


        rotatingLeg.GetComponent<Transform>().Rotate(0, 0, Random.Range(0, 366f));
        rotatingLeg.SetActive(true);
        for (int i = 0; i < nr_legs; i++)
        {
            if (usableArms.legs[i].activeSelf == true)
            {

                legs[i].SetActive(true);
                StartCoroutine(legs[i].GetComponent<MoveToPoint>().MoveToPtn());
                legs[i].GetComponent<Damage>().damage = damage;
            }
        }
        yield return new WaitForSeconds(5f);
        int j = 0;

        int ranChange = 0;
        int ranCounter = 0;
        bool changed = false;
        if (changeDirection == true)
        {
            ranChange = Random.Range(45, 101);
        }

        while (j < rotate_points)
        {

            rotatingLeg.GetComponent<Transform>().Rotate(0, 0, speed * direction + speed * enrage);
            if (changeDirection == true)
            {
                ranCounter++;
                if (ranChange == ranCounter)
                {
                    ranCounter = 0;
                    ranChange = Random.Range(45, 101);
                    direction *= -1;
                }
            }
            j++;
            yield return new WaitForSeconds(0.03f);
        }
        for (int i = 0; i < nr_legs; i++)
        {
            legs[i].SetActive(false);
            legs[i].transform.localPosition = legs_initPos[i];

        }
        fan = false;
    }

    //////////////////////////////////////

    public IEnumerator Swipe(int nrAttacks = 10, bool doubleAttacks = false, float enrage = 0.4f, float speed = 5f, float delayAttacks = 0f)
    {
        swipe = true;


        swipeAnime.gameObject.SetActive(true);
        swipeAnime.speed = 1f;
        swipeAnime.speed = speed + speed * enrage;
        if (doubleAttacks == false)
            for (int i = 0; i < nrAttacks; i++)
            {
                int ran = Random.Range(1, 9);
                swipeAnime.SetTrigger("leg" + ran);
                yield return new WaitForSeconds(delayAttacks);
            }
        else
        {
            for (int i = 0; i < nrAttacks; i++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    int ran = Random.Range(0, 4);
                    switch (ran)
                    {
                        case 0:
                            {
                                swipeAnime.SetTrigger("leg1-3");
                                break;
                            }
                        case 1:
                            {
                                swipeAnime.SetTrigger("leg2-7");
                                break;
                            }
                        case 2:
                            {
                                swipeAnime.SetTrigger("leg4-5");
                                break;
                            }
                        default:
                            {
                                swipeAnime.SetTrigger("leg6-8");
                                break;
                            }
                    }
                    if (i <= nrAttacks - 1)
                        yield return new WaitForSeconds(delayAttacks);
                }
                else
                {
                    int ran = Random.Range(1, 9);
                    swipeAnime.SetTrigger("leg" + ran);
                    if (i <= nrAttacks - 1)
                        yield return new WaitForSeconds(delayAttacks);
                }
            }
        }

        int x = 0;

        while (x < 7)
        {
            if (swipeAnime.GetCurrentAnimatorStateInfo(0).IsName("default"))
            {
                x++;
            }
            else x = 0;

            Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX = " + x);
            yield return new WaitForSeconds(0.1f);
        }

        swipeAnime.speed = 1f;
        swipeAnime.gameObject.SetActive(false);
        swipe = false;

    }

    public Animator swipeAnime;

    /// <summary>
    /// //hard is speed 1.7 delay of 0
    /// </summary>
    /// <param name="nrAttacks"></param>
    /// <param name="doubleAttacks"></param>
    /// <param name="speed"></param>
    /// <param name="delayAttacks"></param>
    /// <returns></returns>

    public GameObject bone;
    public GameObject bone_limit;

    public IEnumerator Bony()
    {
        int nBones = 10;
        float boneDist = 0.3f;
        float initialSpawnLoc = -1.359f;

        float[] xRemember = new float[10];

        xRemember[0] = initialSpawnLoc;

        bool[] spawned = new bool[10];

        for (int i = 1; i < nBones; i++)
        {
            xRemember[i] = xRemember[i - 1] + boneDist;
            spawned[i] = false;
        }

        Instantiate(bone_limit, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < nBones; i++)
        {
            int aux = 0;

            int ran = Random.Range(0, nBones);

            if (spawned[ran] == true)
            {
                while (spawned[ran] == true)
                {
                    Debug.Log("bone position taken, trying another");
                    ran = Random.Range(0, nBones);
                }
            }
            spawned[ran] = true;
            Instantiate(bone, new Vector3(xRemember[ran], -2.848f, -0.1f), Quaternion.identity);
            Debug.Log("Spawning bone, waiting for next");
            yield return new WaitForSeconds(0.2f);
        }
    }

    public GameObject oneShotRock;
    public Vector3 oneSPos;

    public IEnumerator OneShot()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(oneShotRock, oneSPos, Quaternion.identity);
    }


}


