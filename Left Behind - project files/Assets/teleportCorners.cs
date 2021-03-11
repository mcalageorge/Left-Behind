using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportCorners : MonoBehaviour
{
    GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Transform playerTransform = collider.gameObject.GetComponent<Transform>();

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal != 0 && vertical == 0)
            {
                if (horizontal > 0)
                    playerTransform.position = new Vector3(-1.4f, playerTransform.position.y, playerTransform.position.z);
                else
                    playerTransform.position = new Vector3(1.4f, playerTransform.position.y, playerTransform.position.z);
            }

            else if (horizontal == 0 && vertical != 0)
            {
                if (vertical > 0)
                    playerTransform.position = new Vector3(playerTransform.position.x, -2.726f, playerTransform.position.z);
                else
                    playerTransform.position = new Vector3(playerTransform.position.x, -0.386f, playerTransform.position.z);
            }

            else if (horizontal != 0 && vertical != 0)
            {
                if (vertical < 0 && horizontal < 0)
                {      
                        playerTransform.position = new Vector3(1.4f, -0.386f, playerTransform.position.z); 
                }
                else if (vertical < 0 && horizontal > 0)
                {
                        playerTransform.position = new Vector3(-1.4f, -0.386f, playerTransform.position.z);
                }
                else if (vertical > 0 && horizontal > 0)
                {
                    playerTransform.position = new Vector3(-1.4f, -2.726f, playerTransform.position.z);
                }
                else
                {
                    playerTransform.position = new Vector3(1.4f, -2.726f, playerTransform.position.z);
                }
            }
            parent.GetComponent<DestroyTeleported>().PlayTele();
        }
        
    }
}

