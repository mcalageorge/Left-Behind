using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndSoIChita : MonoBehaviour
{
    PInteraction pInter;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject play = col.gameObject;
        pInter = play.GetComponent<PInteraction>();

        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(CheckForZ());
        }
    }

    void OnTriggerExitD(Collider2D col)
    {
        pInter = null;
        StopCoroutine(CheckForZ());
    }

    public AudioClip chit;

    IEnumerator CheckForZ()
    {
        while (pInter != null)
        {
            if (pInter.hotkeyd == true)
            {
                this.GetComponent<AudioSource>().PlayOneShot(chit, 0.5f);

            }
            yield return new WaitForSeconds(0.1f);
        }
    }




    
}
