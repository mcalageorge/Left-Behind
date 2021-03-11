using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBone : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("BiggifyBone");
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    public void StopBig()
    {
        Debug.Log("Stopping Biggify...");
        StopCoroutine("BiggifyBone");
    }
    
   public IEnumerator BiggifyBone()
    {
        Vector3 scaleChange = new Vector3(0f, 0.03f, 0f);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 100; i++)
        {
            Debug.Log("Currently extending");
            yield return new WaitForSeconds(0.03f);
            transform.localScale += scaleChange; 
        }
    }
}
