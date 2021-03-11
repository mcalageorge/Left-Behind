using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEndIn : MonoBehaviour
{
    SpriteRenderer ren;
    public PMovement pm;
    public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        ren = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(stop_black());
    }

    float b = 1f;

    IEnumerator stop_black()
    {
        yield return new WaitForSeconds(0.1f);
        pm.speed = 0f;

        while (ren.color.a > 0)
        {
            ren.color = new Color(255, 255, 255, b);
            b -= 0.03f;
            yield return new WaitForSeconds(0.1f);
        }
        pm.speed = 0.1f;
        aud.Play();
    }
}
