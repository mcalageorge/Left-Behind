using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTeleporter : MonoBehaviour
{
    public string direction;

    GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Transform playerTransform = collider.gameObject.GetComponent<Transform>();

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");


            switch (direction)
            {
                case ("up"):
                    {
                        playerTransform.position = new Vector3(playerTransform.position.x, -2.726f, playerTransform.position.z);
                        break;
                    }
                case ("down"):
                    {
                        playerTransform.position = new Vector3(playerTransform.position.x, -0.386f, playerTransform.position.z);
                        break;
                    }
                case ("left"):
                    {
                        playerTransform.position = new Vector3(1.4f, playerTransform.position.y, playerTransform.position.z);
                        break;
                    }
                case ("right"):
                    {
                        playerTransform.position = new Vector3(-1.4f, playerTransform.position.y, playerTransform.position.z);
                        break;
                    }
                default:
                    {
                        Debug.Log("how did we even get here?");
                        break;
                    }
            }
            parent.GetComponent<DestroyTeleported>().PlayTele();

        }
    }
}
