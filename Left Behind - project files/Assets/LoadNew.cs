using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNew : MonoBehaviour
{
    public GameObject image;
    int i = 0;
    bool done = false;

    Cutscene cutie;

    // Start is called before the first frame update
    void Start()
    {
        cutie = this.gameObject.GetComponent<Cutscene>();
    }

    void Update()
    {

        if(image.gameObject.activeSelf == false && done == false)
        {
            i++;
            done = true;
            
            Debug.Log("started loading scene");
        }
        if (image.gameObject.activeSelf == true) { done = false; print("reeeeeeeeeeee"); }
        if (i == 3) { /*SceneManager.LoadScene(2);*/ cutie.AO.allowSceneActivation = true;  }
        print("YISUS IS + "+ i);
    }
}
