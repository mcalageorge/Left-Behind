using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaScirpt : MonoBehaviour
{
    public Interacting_FaceMovement mind;

    public bool destroyAnyway;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (destroyAnyway == false)
        {
            if (col.gameObject.tag == "Player" && mind.gameObject != null)
            {
                if (mind.didTalk == true)
                    Destroy(mind.gameObject);
            }
        }
        else
        {
            Destroy(mind.gameObject);
        }
    }
}
