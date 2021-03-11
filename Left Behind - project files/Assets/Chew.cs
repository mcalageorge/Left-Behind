using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chew : MonoBehaviour
{
    public Sprite[] chew;
    SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Chewing());
    }

    IEnumerator Chewing()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            renderer.sprite = chew[0];
            yield return new WaitForSeconds(0.4f);
            renderer.sprite = chew[1];
        }
    }
   
}
