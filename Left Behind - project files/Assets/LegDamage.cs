using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegDamage : MonoBehaviour
{
    
    bool entered = false;
    GameObject q;

    int damage = 2;

    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");

        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(damage);
            entered = true;
            q = collided.gameObject;
        }
    }

    void FixedUpdate()
    {
        if (entered == true && q != null)
            q.gameObject.GetComponent<HP>().ChangeHP(damage);
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
