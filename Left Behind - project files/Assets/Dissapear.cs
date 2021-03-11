using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
    public GameObject boss;
    bool done = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && done == false)
        {
            done = true;
            StartCoroutine(sound());
        }
    }

    IEnumerator sound()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<AudioSource>().Play();
        Destroy(boss.gameObject);
        done = true;
    }
}
