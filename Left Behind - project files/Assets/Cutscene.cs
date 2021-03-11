using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{

    GameObject camera;
    public GameObject frisk;

    public string LASTLINE;

    public Vector3 camera_position;

    public AsyncOperation AO;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            AO = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            AO.allowSceneActivation = false;
            frisk = player.gameObject;
            frisk.GetComponent<PMovement>().enabled = false;

            StartCoroutine("MoveCamera");
        }
    }

    void FindCamera()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void DisableMovementRotateToBoss()
    {
        frisk.GetComponent<PMovement>().help = 3;
        frisk.GetComponent<PMovement>().walking_picture.sprite = frisk.GetComponent<PMovement>().wl[0];
        frisk.GetComponent<PMovement>().speed = 0f;
    }

    bool positioned = false;
    float speed = 1f;

    IEnumerator MoveCamera()
    {
        FindCamera();
        DisableMovementRotateToBoss();

        while (positioned == false)
        {
            if (Vector3.Distance(camera.gameObject.GetComponent<Transform>().transform.position, camera_position) < 2f)
            { positioned = true; }

            float step = speed * Time.deltaTime * 2.5f;
            camera.gameObject.GetComponent<Transform>().transform.position = Vector3.MoveTowards(
                camera.gameObject.GetComponent<Transform>().transform.position, camera_position, step);



            yield return new WaitForSeconds(0.01f);



        }


        yield return new WaitForSeconds(3f);

        SetBossTrigger();
    }

    public GameObject boss;

    void SetBossTrigger()
    {
        boss.GetComponent<Collider2D>().enabled = true;
        this.gameObject.GetComponent<LoadNew>().enabled = true;
        challenged = true;
    }

    bool alreadyCalled = false;
    bool challenged = false;
    bool pressed_Z = false;



    int number = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0))
            pressed_Z = true;
        else pressed_Z = false;

        if (challenged == true && alreadyCalled == false)
        {
            frisk.GetComponent<PInteraction>().hotkeyd = true;
            alreadyCalled = true;
        }

        CheckForPause();
            
        if (boss.GetComponent<Interacting>().lineFinished == true) fin = true;
        else fin = false;

    }

    bool fin = false;

    public int[] pause_Checker;
    int chk = 0;

    public GameObject image;
    public Text text;

    bool done_i = false;

    void CheckForPause()
    {
        if (boss.GetComponent<Interacting>().i == pause_Checker[chk] && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)) && done_i == false && fin == true)
        {
            Debug.Log("WE GET HERE");
            done_i = true;

            image.SetActive(false);
            print("we got here");
            StartCoroutine("changePos");
            chk++;
        }
    }
    public Sprite looking_right;
    public AudioSource aus;

    IEnumerator changePos()
    {
        
        frisk.GetComponent<PInteraction>().wait = true;
        yield return new WaitForSeconds(1.5f);
        
        print("we waited 1.5");
        boss.GetComponent<SpriteRenderer>().sprite = looking_right;
        this.GetComponent<AudioSource>().Play();
         yield return new WaitForSeconds(1.5f);
         

        if (image.activeSelf == false)
        {
            image.SetActive(true);
            frisk.GetComponent<PInteraction>().hotkeyd = true;
        }
        frisk.GetComponent<PInteraction>().wait = false;
        
    }

    void CheckForFight()
    {
    }

    void LoadFight()
    {
        SceneManager.LoadScene(2);

    }
   

}
   
