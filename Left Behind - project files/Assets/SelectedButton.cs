using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SelectedButton : MonoBehaviour, ISelectHandler
{
    UnityEngine.EventSystems.EventSystem myEvent;
    public GameObject heartSelect;

    void Start()
    {
        myEvent = GameObject.FindGameObjectWithTag("eventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
    }

    public void OnSelect(BaseEventData myEvent)
    {
        StartCoroutine("delayAssignmentOfHeart");
    }

    IEnumerator delayAssignmentOfHeart()
    {
        yield return new WaitForSeconds(1f * Time.deltaTime);
        EnableHeart();
        Vector3 currentPos = this.GetComponent<Transform>().position;
        currentPos.x -= 20f;
        currentPos.y -= 20f;
        heartSelect.GetComponent<Transform>().position = currentPos;
    }

    public void EnableHeart()
    {
        heartSelect.SetActive(true);
    }

    public void DisableHeart()
    {
        heartSelect.SetActive(false);
    }

}
