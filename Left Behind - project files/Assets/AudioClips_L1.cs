using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips_L1 : MonoBehaviour
{
    public AudioClip[] audioClips;

    public IEnumerator FadeAndSwitch(AudioClip song)
    {
        AudioSource aud = this.gameObject.GetComponent<AudioSource>();
        while (aud.volume > 0)
        {
            aud.volume -= 0.01f;
            yield return new WaitForSeconds(0.03f);
        }
        aud.Stop();
        aud.clip = song;
        aud.Play();
        while (aud.volume < 0.15)
        {
            aud.Play();
            aud.volume += 0.02f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
