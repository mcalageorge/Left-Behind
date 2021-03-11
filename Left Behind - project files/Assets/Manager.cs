using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Image fakeArena;
    public GameObject arena;

   public bool bigSize = false;
   public bool smallSize;

    public bool allowed_to_change = false;

    public UnityEngine.EventSystems.EventSystem myEvent;

    public GameObject fighted;

    public bool webbed = false;
    public bool tped = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            allowed_to_change = true;

        }

        if (smallSize == true && allowed_to_change == true)
            EnlargeArenaDisableItAndEnableFakeArena();

        

        if(bigSize == true && allowed_to_change == true)
            ReduceFakeArenaDisableItAndEnableArena();
    }
    float x = 10f;
    float y = 10f;    

    void EnlargeArenaDisableItAndEnableFakeArena()
    {
        heart.GetComponent<MoveSoul>().speed = 0;
        if (arena.transform.localScale.x < 3.5f)
        {
            arena.transform.localScale = new Vector3(x * Time.deltaTime, 1, 1);
            x += 4f;
        }
        else if (arena.transform.localScale.x > 3.5f) arena.transform.localScale = new Vector3(3.5f, 1, 1);
        else { fakeArena.gameObject.SetActive(true); x = 10f; bigSize = true; smallSize = false; allowed_to_change = false; myEvent.SetSelectedGameObject(null); heart.GetComponent<MoveSoul>().speed = 0; }
    }
    void ReduceFakeArenaDisableItAndEnableArena()
    {
        if (arena.transform.localScale.x > 1f)
        {
            fakeArena.gameObject.SetActive(false);
            arena.transform.localScale = new Vector3(arena.transform.localScale.x - y * Time.deltaTime / 6f, 1, 1);
            y += 0.4f;
        }
        else { arena.gameObject.SetActive(true); y = 10f; bigSize = false; smallSize = true; allowed_to_change = false; arena.transform.localScale = new Vector3(1, 1, 1); ResetHeart();
            if (webbed == false && tped == false) heart.GetComponent<MoveSoul>().speed = heart.GetComponent<MoveSoul>().normalSpeed;
            else if(webbed == true) heart.GetComponent<MoveSoul>().speed = heart.GetComponent<MoveSoul>().slowSpeed;
            else heart.GetComponent<MoveSoul>().speed = heart.GetComponent<MoveSoul>().fastSpeed;
        }
    }

    public void SwitchArenaSize()
    {
        allowed_to_change = !allowed_to_change;
    }

    public GameObject heart;

    void ResetHeart()
    {
        heart.GetComponent<Transform>().position = new Vector3(0, -1.5f, -9.67f);
    }
}
