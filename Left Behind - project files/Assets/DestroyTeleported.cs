using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTeleported : MonoBehaviour
{
    Attacks bossAttack;
    int roundUponDestroy;
    int difference = 3;

    Manager man;

    SpriteRenderer arena;

    GameObject player;

    public AudioClip sound;
    public float volume;
    AudioSource aud;

    public void PlayTele()
    {
        aud.PlayOneShot(sound, volume);
    }

    void Start()
    {
        aud = this.gameObject.GetComponent<AudioSource>();
        man = GameObject.FindWithTag("manager").GetComponent<Manager>();
        man.tped = true;
        Debug.Log("man.tped: " + man.tped);
        player = GameObject.FindWithTag("Player");
        player.GetComponent<MoveSoul>().speed = player.GetComponent<MoveSoul>().fastSpeed;
        bossAttack = GameObject.FindWithTag("enemy").gameObject.GetComponent<Attacks>();
        arena = GameObject.Find("Arena").GetComponent<SpriteRenderer>();

        Debug.Log("man.tped: " + man.tped + " === player.GetComponent<MoveSoul>().speed: "+ player.GetComponent<MoveSoul>().speed);

        roundUponDestroy = bossAttack.round + difference;

        StartCoroutine(TurnCheck());
        Debug.Log("roundUponDestroy is " + roundUponDestroy + "; and round is " + bossAttack.round);
    }

    IEnumerator TurnCheck()
    {
        while (true)
        {
            if (bossAttack.round == roundUponDestroy) 
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
                
        }
        float min = 0.25f;
        for (int i = 0; i < 75; i++)
        {
            min += 0.01f;
            arena.color = new Color(1f, 1f, 1f, min);
            yield return new WaitForSeconds(0.015f);
            Debug.Log("alpha is " + arena.color);
        }
        SetHeartColor changeCol = player.GetComponent<SetHeartColor>();

        changeCol.UpdateColor(changeCol.mainColor);

        player.GetComponent<MoveSoul>().speed = player.GetComponent<MoveSoul>().normalSpeed;
        man.tped = false;
        Destroy(gameObject);
    }
}
