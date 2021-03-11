using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public Image canvas;

    public Button[] buttonss;

    void Awake()
    {

        Screen.SetResolution(800, 600, true);

    }

    //Fade out

    public void FadeOCon()
    {
        CanvasGroup canvasGroup = canvas.gameObject.GetComponent<CanvasGroup>();

        CutButtonFromNetwork(buttonss[0]);

        PlayerPrefs.SetInt("saved", 1);
        StartCoroutine(CoroutineToFadeOut());
    }

    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();      
    }


    public void FadeONew()
    {
       CanvasGroup canvasGroup = canvas.gameObject.GetComponent<CanvasGroup>();

        CutButtonFromNetwork(buttonss[1]);

        PlayerPrefs.SetInt("saved", 0);
        StartCoroutine(CoroutineToFadeOut());
    }

    void CutButtonFromNetwork(Button buq)
    {
        Navigation navigation = buq.gameObject.GetComponent<Button>().navigation;
        navigation.selectOnUp = null;
        navigation.selectOnDown = null;
        buq.navigation = navigation;
    }

    public AudioSource source;

    public IEnumerator CoroutineToFadeOut()
    {
        FadeOut fade = this.gameObject.GetComponent<FadeOut>();
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();
        
        while(canvasGroup.alpha > 0)
        {
            Debug.Log("Runnning fading... " + (1f - canvasGroup.alpha) + " / 1");
            canvasGroup.alpha -= Time.deltaTime / 2;
            source.volume -= Time.deltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        if (fade.canvas.GetComponent<CanvasGroup>().alpha == 0)
        {
            SceneManager.LoadScene("L1");
        }
        yield return null;
    }

    //Fade in

    public void FadeI()
    {
        CanvasGroup canvasGroup = canvas.gameObject.GetComponent<CanvasGroup>();

        StartCoroutine(CoroutineToFadeIn());
    }

    public IEnumerator CoroutineToFadeIn()
    {
        CanvasGroup canvasGroup = canvas.GetComponent<CanvasGroup>();

        while (canvasGroup.alpha < 1)
        {
            Debug.Log("Runnning fading... " + (1f - canvasGroup.alpha) + " / 1");
            canvasGroup.alpha += Time.deltaTime / 2;

            yield return null;
        }
        canvasGroup.interactable = false;
        
        yield return null;
    }
}
