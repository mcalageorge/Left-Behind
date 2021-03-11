using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public GameObject wall;

    // Update is called once per frame
    void Update()
    {
        if(wall.activeSelf == false)
        {
            SceneManager.LoadScene(2);
        }
    }
}
