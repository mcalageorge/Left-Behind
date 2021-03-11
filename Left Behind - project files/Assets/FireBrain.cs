using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBrain : MonoBehaviour
{
    int damage = 3;
    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("fire gogo");


        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(damage);

        }

    }
}
