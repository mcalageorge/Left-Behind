using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginFight : MonoBehaviour
{
    SpriteRenderer renderer;

    float timer = 1f;
    int count = 0;

    int speed = 7;

    public GameObject arena;
    public GameObject canvas;

    public AudioClip sound_of_hurt;

    public GameObject boss;
    public GameObject background;

    public BoxAI2 box2;
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().Play();
        renderer = this.GetComponent<SpriteRenderer> ();
        this.gameObject.GetComponent<MoveSoul>().speed = 0f;
    }

    public float step = 0.11f;

    public AudioClip intro;

    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        if (count < 6)
            if (timer >= 0)
            {
                timer -= 10 * Time.deltaTime;
            }
            else { timer = 1f; renderer.enabled = !renderer.enabled; count++; }

        if(count >= 6)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-5.36f, -4.37f,  -8f), step * Time.deltaTime);

        }
        if(this.transform.position.y <= -4f)
        {
            source.clip = intro;
            source.Play();
            arena.SetActive(true);
            canvas.SetActive(true);
            boss.SetActive(true);
            background.SetActive(true);
            this.GetComponent<MoveSoul>().enabled = true;
            this.GetComponent<AudioSource>().clip = sound_of_hurt;
            box2.enabled = true;
            Destroy(this.GetComponent<BeginFight>());
        }
        
    }
}
