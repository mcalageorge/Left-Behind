using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySounds : MonoBehaviour
{
    public AudioClip ping;

    public void PlayPing()
        {
            this.GetComponent<AudioSource>().clip = ping;
            this.GetComponent<AudioSource>().Play();
        }
}
