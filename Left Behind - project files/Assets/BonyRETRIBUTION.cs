using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonyRETRIBUTION : MonoBehaviour
{
    GameObject player;
    public int damage;
    bool got = false;
   

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Collided with Player");
            player = col.gameObject;
            
            player.GetComponent<HP>().ChangeHP(damage);
        }
        if (col.gameObject.tag == "limit")
        {
            
            Debug.Log("collided with limit");

            GameObject parentOS = transform.parent.gameObject;

            parentOS.gameObject.GetComponent<BigBone>().StopBig();

            Debug.Log("parent name is " + parentOS.gameObject.name);

            if (got == false)
            {
                GameObject lim = col.gameObject;
                CommenceKO ko = lim.GetComponent<CommenceKO>();
                ko.bones[ko.boneRef] = this.gameObject;
                ko.boneRef++;
                got = true;
            }
            
        }
    }
}
