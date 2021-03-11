using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBrain : MonoBehaviour
{

    float speed = 2f;

    public bool allowed;

    int damage = 3;
    
    void FixedUpdate()
    {
        if(allowed == true)
        transform.Translate(-Time.deltaTime * speed, 0, 0);     
    }

    void OnCollisionEnter2D(Collision2D collided)
    {
        speed *= -1;
        
    }
    void OnTriggerEnter2D(Collider2D collided)
    {
        
        if (collided.gameObject.tag == "Player")
        {
           
            collided.gameObject.GetComponent<HP>().ChangeHP(damage);
            

        }
    }

}
