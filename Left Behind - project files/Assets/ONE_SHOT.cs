using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONE_SHOT : MonoBehaviour
{
    GameObject player;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(Kill(speed));
    }

    IEnumerator Kill(float speed)
    {
        int j = 0;

        yield return new WaitForSeconds(2.5f);

        while (j < 35)
        {
            Vector2 direction = player.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            j++;

            yield return new WaitForSeconds(0.01f);
        }

        this.gameObject.GetComponent<AudioSource>().Play();
        int x = 0;
        while (x < 50)
        {
            Debug.Log("Moving");

            transform.position += this.transform.right * speed;
            
            yield return new WaitForSeconds(0.1f);
            player.GetComponent<HP>().health = 0;
        }
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
