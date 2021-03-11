using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : MonoBehaviour
{
    bool triggered = false;
    bool started = false;

    SpriteRenderer thisSpriteRenderer;

    public Sprite[] sprites;

    Interacting thisInteraction;

    void Start()
    {
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        thisInteraction = this.GetComponent<Interacting>();

        triggered = false;
    }

    bool hotkeyd;

    PInteraction q;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            triggered = true;
            q = col.gameObject.GetComponent<PInteraction>();
        }
        StartCoroutine(FakeUpdate());
    }

    void OnTriggerExit2D(Collider2D col)
    {
        triggered = false;
        q = null;
        started = false;
        StopCoroutine(FakeUpdate());
    }

    IEnumerator FakeUpdate()
    {
        while (true)
        {
            if (thisInteraction.lineFinished == true)
            {
                Debug.Log("stoped");
                StopCoroutine("Talking");
                thisSpriteRenderer.sprite = sprites[0];

            }

            else if (thisInteraction.lineFinished == false)
            {
                StartCoroutine("Talking");

            }

            yield return new WaitForSeconds(0.3f);
        }
    }

 
    
    IEnumerator Talking()
    {
        while (true)
        {
            thisSpriteRenderer.sprite = sprites[0];
            yield return new WaitForSeconds(0.15f);
            thisSpriteRenderer.sprite = sprites[1];
            yield return new WaitForSeconds(0.15f);
        }
    }
}
