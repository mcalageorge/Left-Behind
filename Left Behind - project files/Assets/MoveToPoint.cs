using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    public Vector3 desiredPos;
    public int movePoints;
    Animator shakeYaBooty;
    // Start is called before the first frame update
    void Start()
    {
        GameObject q = GameObject.Find("Main Camera");
        shakeYaBooty = q.gameObject.GetComponent<Animator>();
    }

    //7.14 -7.13
    //11.81 0.04
    // 10.29 10.31
    //-10.23 10.26
    //-11.89 0.04
    //-7.06 -7.09

    public IEnumerator MoveToPtn()
    {
        Vector3 currentPos = transform.position;
        int i = 0;
        while(i < movePoints)
        {
            i++;
            Debug.Log("Distance is " + Vector3.Distance(currentPos, desiredPos));
            this.gameObject.GetComponent<Transform>().Translate(Vector3.left / 5);
            currentPos = transform.position;
            yield return new WaitForSeconds(0.05f);
        }
        AudioSource ass = gameObject.GetComponentInParent(typeof(AudioSource)) as AudioSource;
        if(shakeYaBooty!= null)
        shakeYaBooty.SetTrigger("shakeTrig");
        ass.PlayOneShot(ass.clip, 0.7f);
    }
}
