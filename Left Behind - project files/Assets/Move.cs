using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == "up")
            directionQ = Vector3.up;
        else if (direction == "down")
            directionQ = Vector3.down;
        else if (direction == "left")
            directionQ = Vector3.left;
        else
            directionQ = Vector3.right;

        Debug.Log("Spawned: " + direction);
    }

    Vector3 directionQ;
    public float speed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position += directionQ * Time.deltaTime * speed;
    }
}
