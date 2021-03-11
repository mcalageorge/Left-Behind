using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HP : MonoBehaviour
{

    public int health = 20;
    public Text text;

    public Slider bar;

    public Animation deathAnimation;

    public UnityEngine.EventSystems.EventSystem myEvent;

    public AudioSource music;

    bool dying = false;

    void Update()
    {
        if (health <= 0 && dying == false)
        {

            StartCoroutine(Die());
            
        }

    }

    bool tookDmgRecently = false;
    bool invulnerable = false;

    public void ChangeHP(int damage)
    {
        if (tookDmgRecently == false && invulnerable == false)
        {
            
                tookDmgRecently = true;
                
                health -= damage;
                UpdateHUD(health);
                
                this.GetComponent<AudioSource>().Play();
                StartCoroutine("Invulnerable");
        }
    }

    void UpdateHUD(int hp)
    {
        bar.value = hp;
        text.text = ("HP: " + hp);
        hpDisplay.text = (hp + "/20");
    }

    public Canvas canvas;
    public Canvas deadCanvas;
    public GameObject black;

   

    public AudioClip deathClip;

    public GameObject camera;

    IEnumerator Die()
    {
        music.Stop();
        deathAnimation.enabled = true;
        deathAnimation.Play();
        this.gameObject.GetComponent<MoveSoul>().speed = 0f;
        invulnerable = true;
        black.SetActive(true);

        transform.position = new Vector3(1000 + transform.position.x, transform.position.y, -9.66f);
        camera.GetComponent<Animator>().enabled = false;
        camera.transform.Translate(1000, camera.transform.position.y, -10f);

        dying = true;
        health = 0;
        UpdateHUD(health);
        Debug.Log("You dead");
        canvas.gameObject.SetActive(false);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(deathClip, 0.7f);
        yield return new WaitForSeconds(1.2f);
        deadCanvas.gameObject.SetActive(true);
    }

    

    IEnumerator Invulnerable()
    {
        for (int i = 0; i < 9; i++)
        {
            if (this.GetComponent<SpriteRenderer>().enabled == false)
                this.GetComponent<SpriteRenderer>().enabled = true;
            else
                this.GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(0.2f);
        }


        
        tookDmgRecently = false;
        this.GetComponent<SpriteRenderer>().enabled = enabled;
    }

    public int[] healing;
    public Text hpDisplay;

    void DealDamage()
    {
        health -= 10;
        bar.value = health;
        hpDisplay.text = (health + "/ 20");
    }

    public void Heal01()
    {
        health += healing[0];

        if (health > 20)
        {
            health = 20;
        }
        bar.value = health;
        hpDisplay.text = (health + "/ 20");
        GameObject.FindWithTag("f1").SetActive(false);
    }
    

}
