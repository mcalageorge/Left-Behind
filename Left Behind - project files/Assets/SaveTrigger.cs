using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public AudioClip save_sound;
    PInteraction interacting;
    Interacting inter;

    void Start()
    {
        
        inter = this.gameObject.GetComponent<Interacting>();
    }

    public GameObject image;

    void Update()
    {
        if(saved == true && image.activeSelf == false)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator CommenceChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            this.gameObject.GetComponent<Transform>().Rotate(0, 0, -35f);
            yield return new WaitForSeconds(0.4f);
            this.gameObject.GetComponent<Transform>().Rotate(0, 0, 35f);
        }
    }

    bool saved = false;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && saved == false)
        {
            interacting = col.gameObject.GetComponent<PInteraction>();

            StartCoroutine(Save());
            
            

            this.gameObject.GetComponent<AudioSource>().PlayOneShot(save_sound, 1f);
            PlayerPrefs.SetInt("saved", 1);
        }
    }

    IEnumerator Save()
    {
        yield return new WaitForSeconds(0.01f);
        saved = true;

        interacting.hotkeyd = true;

    }
}
