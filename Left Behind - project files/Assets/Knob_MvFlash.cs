using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Knob_MvFlash : MonoBehaviour
{
    bool done = false;
    public Sprite[] lines;
    public Slider slider;

    bool pressed_Z = false;
    bool began = false;

    public GameObject parent;

    public UnityEngine.EventSystems.EventSystem myEvent;

    public Manager man;

    void Start()
    {
        nextLegBreak = hitsNeededToCutLeg;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)) pressed_Z = true;
        else pressed_Z = false;

        if (began == false) { Debug.Log("started attacking"); StartCoroutine("Movement"); }

        if (pressed_Z && done == false && slider.GetComponent<Slider>().value < 99)
        {
            pressed_Z = false;
            done = true;
            StopCoroutine("Movement");
            StartCoroutine("Flashing");

        }

        if (slider.GetComponent<Slider>().value > 99 && done == false)
        {
            StopCoroutine("Movement");
            done = true;
            //   this.gameObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            StartCoroutine(DisplayHideMiss());
        }
    }

    IEnumerator Flashing()
    {
        parent.SetActive(true);
        int i = 0;
        
        while (i < 8)
        {
            this.gameObject.GetComponent<Image>().sprite = lines[0];
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<Image>().sprite = lines[1];
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        hits++;
        tree.hits = this.hits;
        this.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(AnimatedAttack());

        yield return new WaitForSeconds(0.7f);

        Debug.Log("hit was " + killBoos);
        if(setTo1HP == false)
        dmgNr.GetComponent<Text>().text = CalculateDamage(slider.GetComponent<Slider>().value, killBoos).ToString();
        else
        {
            Debug.Log("set hp to 1 from flashing");
            dmgNr.GetComponent<Text>().text = (lastRoundHP - 1).ToString();
        }
        dmgNr.SetActive(true);
        //dmgNr.GetComponent<Animator>().Play();

        StartCoroutine(DisplayHideDmg());

        if (hits == nextLegBreak)
        {

            StartCoroutine(FallLeg(ChooseLeg()));
            yield return new WaitForSeconds(2f);
        }
        else
            yield return new WaitForSeconds(0.1f);

    }

    IEnumerator Movement()
    {
        began = true;
        float i = 0;
        while (i <= 100)
        {
            slider.GetComponent<Slider>().value += 2f;
            yield return new WaitForSeconds(0.01f);
            if (pressed_Z == true) break;
        }
    }

    public bool sansDeath = false;

    void ResetHand()
    {
        slider.GetComponent<Slider>().value = 0;
        began = false;
        done = false;
    }

    bool legDestroyed = false;

    public BossAI boss_hp;

    public BoxAI box;

    public GameObject mainFightInterface;

    public DialogueTree tree;

    public bool killboosfair = false;

    public string goToMap;

    public bool setTo1HP = false;

    int Get1HPdmg()
    {
        int dmgToDo = boss_hp.bossHP - 1;
        dmgToDo = boss_hp.bossHP - dmgToDo;
        Debug.Log("damage to do is: " + dmgToDo);
        return dmgToDo;

    }
    int lastRoundHP;

    public Animator bossAnimator;

    IEnumerator DisplayHideDmg()
    {
        if (setTo1HP == true)
        {
            sounds.PlayOneShot(zombieScream, 0.7f);
            bossAnimator.SetBool("isDeadArms", true);
            Debug.Log("set the hp to 1 from display dmg");
            setTo1HP = false;
            boss_hp.bossHP = Get1HPdmg();
            boss_hp.UpdateHP(false);
        }
        else
        {
            boss_hp.bossHP -= CalculateDamage(slider.GetComponent<Slider>().value, killBoos);

            boss_hp.UpdateHP(killBoos);

        }
        float waitTime = 2f;

        audioSource.Play();

        /// here came the 2 lines

        Debug.Log("legdes = " + legDestroyed);
        if (hits == nextLegBreak)
            yield return new WaitForSeconds(waitTime);
        else
            yield return new WaitForSeconds(1f);
        Debug.Log("closing number");
        dmgNr.SetActive(false);

        boss_hp.text = tree.CalculateReply("Attack");

        Debug.Log("processed message, up to bossAI to take care of the rest");

        Debug.Log("closing them both");




        if (killboosfair == true && killBoos == true)
        {
            Debug.Log("we go into here, map is: " + goToMap);
            boss_hp.StartDeathGenocide(false, goToMap);
        }

        else if (killBoos == false)
        {
            ResetHand();
            mainFightInterface.SetActive(false);
            parent.SetActive(false);
        }
        else
        {
            if (killboosfair == false) sansDeath = true;
            tree.gameStage = "sans";
        }
        if (boss_hp.bossHP <= 0)
        {
            boss_hp.StartDeath(sansDeath);
            music.Stop();
        }
        else
        {
            man.allowed_to_change = true;
            boss_hp.allowed_to_read = true;
        }
        lastRoundHP = boss_hp.bossHP;
    }

    public AudioSource music;

    IEnumerator DisplayHideMiss()
    {

        yield return new WaitForSeconds(0.3f);

        parent.SetActive(true);

        audioSource.Play();

        Debug.Log("we dealt miss");

        dmgNr.GetComponent<Text>().text = "MISS";

        dmgNr.SetActive(true);
        audioSource.Play();

        yield return new WaitForSeconds(2f);
        Debug.Log("closing number");
        dmgNr.SetActive(false);


        boss_hp.text = tree.CalculateReply("Miss");


        man.allowed_to_change = true;
        boss_hp.allowed_to_read = true;

        ResetHand();



        parent.SetActive(false);
        mainFightInterface.SetActive(false);

    }

    public GameObject dmgNr;

    public AudioSource audioSource;

    public bool killBoos = false;

    private int CalculateDamage(float x, bool oneShot = false)
    {
        if (oneShot == true)
        {
            return 9999;
            music.Stop();
        }

        else
        {

            bool one = (x <= 25 || (x >= 75 && x < 100));

            bool five = ((x > 25 && x <= 45) || (x < 75 && x >= 55));

            bool ten = (x > 45 && x < 55);

            if (one) return 1;
            else if (five) return 4;
            else return 7;
        }
    }

    public GameObject[] legs;
    public AudioClip cutSound;
    public AudioClip zombieScream;
    public AudioSource sounds;
    public GameObject[] legItems;
    int foodCounter = 0;

    int hitsNeededToCutLeg = 4;
    int hits = 0;
    int nextLegBreak;

    private GameObject ChooseLeg()
    {
        int legToCut;
        while (true)
        {
            legToCut = Random.Range(0, 6);

            if (legs[legToCut].gameObject.activeSelf == true)
                break;
        }
        return legs[legToCut];
    }

    IEnumerator FallLeg(GameObject leg)
    {

        bossAnimator.enabled = false;
        sounds.PlayOneShot(zombieScream, 0.7f);
        legDestroyed = true;
        Transform aux = leg.gameObject.GetComponent<Transform>();
        int rotDirection = Random.Range(-1, 2);
        if (rotDirection == 0) rotDirection = -1;

        //sounds.PlayOneShot(cutSound, 0.7f);

        //yield return new WaitForSeconds(0.4f);

        for (int i = 0; i < 35; i++)
        {
            leg.gameObject.GetComponent<Transform>().Translate(0, -0.01f, 0);

            aux.Rotate(rotDirection * 5, rotDirection * 5, rotDirection * 5);
            Debug.Log(rotDirection + " roDir is");
            yield return null;
            Debug.Log("im on " + i + " iteration");
        }


        leg.SetActive(false);
        bossAnimator.enabled = true;
        Debug.Log("Disabling leg");
        legItems[foodCounter].gameObject.SetActive(true);
        foodCounter++;
        nextLegBreak += hitsNeededToCutLeg;
        Debug.Log("leg broken, next one is at " + nextLegBreak);
        legDestroyed = false;
        Debug.Log("went thorugh entire fall leg");

    }


    public Sprite[] frames;
    public GameObject symbol;

    IEnumerator AnimatedAttack()
    {

        Image symbolRenderer = symbol.GetComponent<Image>();

        symbol.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            symbolRenderer.sprite = frames[i];
            yield return new WaitForSeconds(0.1f);
        }

        symbol.SetActive(false);
    }



}
