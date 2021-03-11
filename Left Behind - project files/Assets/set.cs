using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class set : MonoBehaviour
{
    // Start is called before the first frame update

    bool isFullscreen = true;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (isFullscreen == true)
            {
                Screen.SetResolution(800, 600, false);
                isFullscreen = false;
            }
            else
            {
                Screen.SetResolution(800, 600, true);
                isFullscreen = true;
            }
        }
        

    }
}
