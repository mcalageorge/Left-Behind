using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendoDamage : MonoBehaviour
{
    public int Dmg;


    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");



        if (collided.gameObject.tag == "Player")
        {
            entered = true;
            collided.gameObject.GetComponent<HP>().ChangeHP(Dmg);
            q = collided.gameObject;
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
