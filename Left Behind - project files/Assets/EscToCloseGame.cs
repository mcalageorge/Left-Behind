using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscToCloseGame : MonoBehaviour
{
    Text slider;
    bool started = false;
    // Update is called once per frame
    void Update()
    {
        if (started == false && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(CheckForEsc());
        }
    }


    IEnumerator CheckForEsc()
    {
        slider = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        slider.gameObject.SetActive(true);
        string default0 = "Quitting";
        char dot = '.';
        slider.text = default0;
        while (Input.GetKey(KeyCode.Escape))
        {
            

            if(slider.text == "Quitting....")
            {
                Debug.Log("QuitGame");
                Application.Quit();
            }
            yield return new WaitForSeconds(0.4f);
            slider.text += dot;
        }
            started = false;

            slider.text = "";
            slider.gameObject.SetActive(false);
    }
}
