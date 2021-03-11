using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ontrig : MonoBehaviour
{
    public GameObject theDis;

        void OnTriggerEnter2D(Collider2D vum)
        {
        theDis.SetActive(true);
        StartCoroutine("CreateWait");
        }

    public float secondsToWait;

    IEnumerator CreateWait()
    {

        bool done = false;
        while (theDis.GetComponent<CanvasGroup>().alpha < 0.7 && done == false)
        {
            print("hellos");

            if (theDis.GetComponent<CanvasGroup>().alpha >= 7) done = true;

            theDis.GetComponent<CanvasGroup>().alpha += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        
        StartCoroutine("FadeDestroy");
    }



    IEnumerator FadeDestroy()
    {
        yield return new WaitForSeconds(secondsToWait);
        while(theDis.GetComponent<CanvasGroup>().alpha > 0)
        {
            print("hellos");
           theDis.GetComponent<CanvasGroup>().alpha -= 0.1f;
           yield return new WaitForSeconds(0.1f);
        }
        Destroy(theDis.gameObject);
        Destroy(this.gameObject);
    }

}
