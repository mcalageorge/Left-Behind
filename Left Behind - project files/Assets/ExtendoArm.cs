using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendoArm : MonoBehaviour
{
    GameObject player;

    public AudioClip extendo_sound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        arm = gameObject.transform.GetChild(0).gameObject;

        StartCoroutine("RotateExtend");
    }
    float speed = 5f;

    GameObject arm;

    // Update is called once per frame

    IEnumerator RotateExtend()
    {
        int x = 0;
        while (x < 100)
        {
           
            Vector2 direction = player.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 0.01f));

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
            
            x++;
            yield return new WaitForSeconds(0.03f);
        }
        int z = 0;
        float resizeAmount = 0.1f;
        yield return new WaitForSeconds(0.4f);

        //
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(extendo_sound, 0.5f);
        while (z < 25)
        {

            transform.localScale = new Vector3(transform.localScale.x + resizeAmount, transform.localScale.y, transform.localScale.z + Time.deltaTime *10f);
            yield return new WaitForSeconds(0.015f);
            z++;
        }

    }
}
