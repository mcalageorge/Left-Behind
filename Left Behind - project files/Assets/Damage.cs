using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;

   
    bool entered = false;
    GameObject q;

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
            //collided.gameObject.GetComponent<HP>().ChangeHP(damage);
            entered = false;
            q = null;
        }
    }
}
