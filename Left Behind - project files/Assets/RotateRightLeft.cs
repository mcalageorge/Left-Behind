using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRightLeft : MonoBehaviour
{
    bool lefted = false;

    public void RotateLeft()
    {
        transform.Rotate(0, 0, 180);
        lefted = true;
    }

    void Start()
    {
        StartCoroutine("StartMoving");
    }

    

    IEnumerator StartMoving()
    {
            int i = 0;
            while (i < 30)
            {
                transform.Translate(0.2f, 0, 0);
                yield return new WaitForSeconds(0.05f);
                i++;
            }
        yield return new WaitForSeconds(4f);

        GameObject elbow = transform.GetChild(0).gameObject;
        GameObject hand = elbow.transform.GetChild(0).gameObject;
        hand.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        SpriteRenderer handRen = hand.gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer thisRen = gameObject.GetComponent<SpriteRenderer>();

        float maxA = 1;
        
        while (handRen.color.a >= 0)
        {
            Debug.Log("Started making extendo invisible, a is " + handRen.color.a);
            maxA -= 0.05f;
            thisRen.color = new Color(thisRen.color.r, thisRen.color.r, thisRen.color.r, maxA);
            handRen.color = new Color(thisRen.color.r, thisRen.color.r, thisRen.color.r, maxA);

            yield return new WaitForSeconds(0.07f);
        }
        
        Destroy(gameObject);

    }

}
