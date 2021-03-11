using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transfer2Phase2 : MonoBehaviour
{
    public CanvasGroup white;

    public GameObject[] bosses;

    public AudioClip throwClothing;

    public Animation animation;
    public AnimationClip clip;

    public AudioSource musicPlayer;


    public AudioClip phase1Music;
    public AudioClip phase2Music;

    public BackgroundMovement bkm;

    public BoxAI2 box2;

    public Text[] names;

    public AudioClip brimaVoice;

    IEnumerator SetStage2()
    {
        
        white.gameObject.SetActive(true);
        for (int i = 0; i <= 100; i++)
        {
            white.alpha += 0.01f;
            musicPlayer.volume -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        musicPlayer.clip = phase1Music;
        musicPlayer.Play();
        bosses[0].gameObject.SetActive(false);
        bosses[1].gameObject.SetActive(true);
        musicPlayer.Play();
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(throwClothing, 0.7f);
        animation.clip = clip;
        animation.Play();

        yield return new WaitForSeconds(0.4f);

        bkm.CommenceMove();

        for (int i = 50; i >= 0; i--)
        {
            white.alpha -= 0.02f;
            musicPlayer.volume += 0.008f;
            yield return new WaitForSeconds(0.02f);
        }
        
       
        for(int i = 0; i < names.Length; i++)
        {
            names[i].text = "* Ebrima";
        }

        box2.gameObject.GetComponent<BossAI>().voices[6] = brimaVoice;

        this.gameObject.GetComponent<Buttons>().actII = false;
        white.gameObject.SetActive(false);
        box2.reply = "* The battle begins now.9";
        box2.afterbossSpeech = true;
    }

    public void SetSetStage2()
    {
        StartCoroutine(SetStage2());
    }
}
