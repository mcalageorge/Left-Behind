using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
       // CommenceMove();
    }

    public void CommenceMove()
    {
        StartCoroutine("Movement");
    }

    IEnumerator Movement()
    {
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Animation>().Play();     
    }
}
