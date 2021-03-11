using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    Attacks attacks;
    bool entered = false;
    GameObject q;
    public int Dmg;

    

    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");

        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(Dmg);
            entered = true;
            q = collided.gameObject;
        }

        Debug.Log("asdas");

        if (collided.gameObject.tag == "limit")
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if(entered == true && q != null)
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
