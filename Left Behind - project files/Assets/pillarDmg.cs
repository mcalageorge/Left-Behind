using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarDmg : MonoBehaviour
{
    public int Dmg;


    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");


        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(Dmg);

        }
        if (collided.gameObject.tag == "pillarKiller")
        {
            Destroy(gameObject.transform.parent.gameObject);

        }

    }

    bool entered = false;
    GameObject q;

    void FixedUpdate()
    {
        if (entered == true && q != null)
            q.gameObject.GetComponent<HP>().ChangeHP(Dmg);
    }

    void OnTriggerExit2D(Collider2D collided)
    {
        Debug.Log("Stayed");

        if (collided.gameObject.tag == "Player")
        {
            entered = false;
            q = null;
        }
    }
}
