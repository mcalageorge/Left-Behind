using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private SpriteRenderer renderer;
    public Sprite[] faces;


    // Start is called before the first frame update
    void Start()
    {
        renderer = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Blinking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Blinking()
    {
        while (true)
        {

             int i = 0;
             while (true)
             {
                 renderer.sprite = faces[i];
                 yield return new WaitForSeconds(0.3f);
                 i++;
                 if (i == 9) { break; }
             }
             yield return new WaitForSeconds(1.5f);

            while (true)
            {
                renderer.sprite = faces[i];
                yield return new WaitForSeconds(0.3f);
                i++;
                if (i == faces.Length) { break; }
            }
            yield return new WaitForSeconds(1.5f);
        }
    }


}
