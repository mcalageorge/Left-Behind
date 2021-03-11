using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SansLook : MonoBehaviour
{
    public Sprite[] sansPos;
    SpriteRenderer thisRenderer;


    void RotateSans(Vector2 point)
    {
        Vector3 relativePosition = transform.InverseTransformPoint(point);
        float largestDifference = Mathf.Abs(relativePosition.z);
        float sign = relativePosition.z > 0f ? 1f : -1f;
        string axisWithLargestDifference = "z";

        float xDifference = Mathf.Abs(relativePosition.x);

        if (xDifference > largestDifference)
        {
            largestDifference = xDifference;
            sign = relativePosition.x > 0f ? 1f : -1f;
            axisWithLargestDifference = "x";
        }

        float yDifference = Mathf.Abs(relativePosition.y);

        if (yDifference > largestDifference)
        {
            sign = relativePosition.y > 0f ? 1f : -1f;
            axisWithLargestDifference = "y";
        }
        if (sign > 0f)
        {
            if (axisWithLargestDifference == "x")
            {
                // To the right of
                thisRenderer.sprite = sansPos[0];
                Debug.Log("idk0");
            }
            else if (axisWithLargestDifference == "y")
            {
                // Above
                thisRenderer.sprite = sansPos[3];
                Debug.Log("idk1");
            }
           /* else if (axisWithLargestDifference == "z")
            {
                // In front of
            }*/
        }
        else
        {
            if (axisWithLargestDifference == "x")
            {
                // To the left of
                thisRenderer.sprite = sansPos[2];
                Debug.Log("idk2");
            }
            else if (axisWithLargestDifference == "y")
            {
                // Below
                thisRenderer.sprite = sansPos[1];
                Debug.Log("idk3");
            }
           /* else if (axisWithLargestDifference == "z")
            {
                // Behind
            }*/
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        thisRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    GameObject player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player = col.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Vector2 playerPosVec = new Vector2(player.transform.position.x, player.transform.position.y);
            RotateSans(playerPosVec);
        }
    }
}
