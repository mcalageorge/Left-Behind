using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommenceKO : MonoBehaviour
{
    public GameObject[] bones;
    public int boneRef = 0;


    HP playerHP;

    Animator shakeYaBooty;

    void Start()
    {
        bones = new GameObject[10];
        playerHP = GameObject.FindWithTag("Player").GetComponent<HP>();
        GameObject q = GameObject.Find("Main Camera");
        shakeYaBooty = q.gameObject.GetComponent<Animator>();
    }

    

    

    bool commenced = false;

    void Update()
    {
        if (boneRef == 10 && bones[9] != null && commenced == false)
        {
            Debug.Log("Started final attack");
            commenced = true;
            StartCoroutine(KO());
        }
    }

    IEnumerator KO()
    {


        while (playerHP.health > 0)
        {
            
            Vector3 scaleChange = new Vector3(0f, 0.03f, 0f);

            if (shakeYaBooty != null)
                

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < 30; i++)
            {
                for (int q = 0; q < 10; q++)
                {
                    Debug.Log("Currently de-extending");
                    bones[q].transform.parent.localScale -= scaleChange;
                }
                yield return new WaitForSeconds(0.03f);
                
            }
            
            for (int i = 0; i < 30; i++)
            {
                
                for (int q = 0; q < 10; q++)
                {
                    Debug.Log("Currently extending back up");
                    bones[q].transform.parent.localScale += scaleChange;
                }
                yield return new WaitForSeconds(0.03f);

            }
            this.gameObject.GetComponent<AudioSource>().Play();
            shakeYaBooty.SetTrigger("shakeTrig");
        }
    }
}
