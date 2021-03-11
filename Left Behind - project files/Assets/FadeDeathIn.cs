using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeDeathIn : MonoBehaviour
{
    public CanvasGroup group;
    public AudioClip determination;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Dying");
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(determination, 0.7f);
        while (group.alpha < 1)
        {
            group.alpha += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
