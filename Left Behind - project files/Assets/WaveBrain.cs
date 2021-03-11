using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBrain : MonoBehaviour
{
    public float speed;
    SpriteRenderer mySpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wave());
        mySpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    public Sprite[] mySprites;

    IEnumerator Wave()
    {
        int i = 0;
        while (i < 4)
        {
            yield return new WaitForSeconds(speed);
            mySpriteRenderer.sprite = mySprites[i];
            i++;

            if (i == 3) i = 0;
        }
    }
}
