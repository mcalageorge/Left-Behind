using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dialogue : MonoBehaviour
{
    public Text text;
    public string[] deathRemarksAsgore;

    public string[] deathRemarksSans;

    string line;

    char letter;
    int nr = 0;

    public float readSpeed;

    void Start()
    {


        if (sansTalking == false)
        {
            int rnd = Random.Range(0, deathRemarksAsgore.Length);
            line = deathRemarksAsgore[rnd];
        }
        else
        {
            int rnd = Random.Range(0, deathRemarksSans.Length);
            line = deathRemarksSans[rnd];
        }

        StartCoroutine("ChangeLettersAndPlaySound");
    }
    bool hotkeyd = false;

    bool done = false;

    public bool commence = false;

    public bool sansTalking = false;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0))
                hotkeyd = true;

        if (lineFinished == true && hotkeyd == true && done == true)
        {
            Debug.Log("Load New Scene...");
            SceneManager.LoadScene("L1");
        }

        if (hotkeyd == true && lineFinished == false && done == false)
            {
                StopCoroutine("ChangeLettersAndPlaySound");
                SkipLine();
            }

            
            hotkeyd = false;
    }

    public AudioClip ping;

    IEnumerator ChangeLettersAndPlaySound()
    {
        ResetDiaBox();
        yield return new WaitForSeconds(3.5f);
        lineFinished = false;
        while (lineFinished == false)
        {
            letter = line[nr];

            Debug.Log("we get here");

            if (letter != '9')
            {

                text.text = text.text + letter;
                if (letter != ' ')
                {
                    GetComponent<AudioSource>().PlayOneShot(ping, 0.7f);
                }
                nr++;
            }
            else
            {
                lineFinished = true;
                done = true;

            }
            yield return new WaitForSeconds(readSpeed);
        }
    }

    public void ResetDiaBox()
    {
        text.text = "";
    }

    bool lineFinished = false;

    void SkipLine()
    {
        string help = line;
        text.text = "";

        text.text = help.Replace('9', ' ');
        done = true;

        lineFinished = true;
    }

}
