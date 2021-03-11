using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAndMoveToPlayer : MonoBehaviour
{
    public int Dmg;

    public GameObject player;

    SpriteRenderer spriteRen;

    public bool moveToPoint = false;
    // Start is called before the first frame update
    void Start()
    {       

        player = GameObject.FindWithTag("Player");

        
        int rnd = Random.Range(0, 2);

        if (moveToPoint == true)
        {
            StartCoroutine(StartMovingToPoint(speed)); 
        }

        else
        {
            StartCoroutine(StartMovingToPlayer(speed));
            
        }

        spriteRen = this.gameObject.GetComponent<SpriteRenderer>();

    }

    public AudioClip move;

    public Vector3 targetPos;

    IEnumerator StartMovingToPoint(float speed)
    {
        yield return new WaitForSeconds(2f);

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed / 23.5f);
            yield return new WaitForSeconds(0.05f);
            
        }

        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        float x = 1f;

        for (int i = 0; i < 10; i++)
        {
            x -= 0.1f;
            spriteRen.color = new Color(1, 1, 1, x);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("destroying follow");
        Destroy(gameObject);
    }

    public float speed = 3f;

    IEnumerator StartMovingToPlayer(float speed)
    {
        Destroy(gameObject, 10);
        yield return new WaitForSeconds(2f);

        int j = 0;

        while (j < 35)
        {
            Vector2 direction = player.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            j++;

            yield return new WaitForSeconds(0.03f);
        }
        
        Debug.Log("pleayed audio");

        this.gameObject.GetComponent<AudioSource>().clip = move;
        this.gameObject.GetComponent<AudioSource>().Play();
        while (true)
        {
            Debug.Log("Moving");

            transform.position += this.transform.right * speed / 32;
            transform.position = new Vector3(transform.position.x, transform.position.y, -7f);

            yield return new WaitForSeconds(0.02f);
        }

        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

        float x = 1f;

        for (int i = 0; i < 10; i++)
        {
            x -= 0.1f;
            spriteRen.color = new Color(1,1,1, x);
            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("destroying follow");

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");


        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(Dmg);
            Destroy(gameObject, 5f);
        }



    }
    
}
