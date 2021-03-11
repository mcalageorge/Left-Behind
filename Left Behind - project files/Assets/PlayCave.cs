using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCave : MonoBehaviour
{
    public AudioClips_L1 l1;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("we swithing song...");
        
    }
}
