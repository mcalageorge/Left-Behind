using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadFirst : MonoBehaviour
{
    public FadeOut fade;

    // Update is called once per frame
    void Update()
    {
        if(fade.canvas.GetComponent<CanvasGroup>().alpha == 0)
        {
            SceneManager.LoadScene("L1");
        }
    }
}
