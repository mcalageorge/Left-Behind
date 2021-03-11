using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSoul : MonoBehaviour
{
    public float normalSpeed = 0.05f;
   public float speed = 0.05f;
    public float slowSpeed = 0.025f;
    public float fastSpeed = 0.1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;
        float vertical = Input.GetAxisRaw("Vertical") * speed;

        

        /*   float localx = this.transform.localPosition.x;
           float localy = this.transform.localPosition.y;*/

        if (horizontal > 0) horizontal = speed;
        else if(horizontal < 0) horizontal = -speed;

        if (vertical > 0) vertical = speed;
        else if (vertical < 0) vertical = -speed;

        Debug.Log("horizontal speed is: " + horizontal + " AND vertical speed is: " + vertical);

        transform.Translate(horizontal * 0.7f, 0, 0);
        transform.Translate(0, vertical * 0.7f, 0);
     }   

    
}
