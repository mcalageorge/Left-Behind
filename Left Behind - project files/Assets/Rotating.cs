using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{

    GameObject[] legs = new GameObject[6];

    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            legs[i] = this.gameObject.transform.GetChild(i).gameObject;
            Debug.Log("leg registere");
        }
    }

   /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(EnableLegsAndRotate(speed: 1.5f, nr_legs: 5));
        }
    }*/

    /*IEnumerator EnableLegsAndRotate(float speed, int nr_legs, int damage = 1, int direction = 1, bool changeDirection = true, int rotate_points = 300)
    {
        for (int i = 0; i < nr_legs; i++)
        {
            legs[i].SetActive(true);
            legs[i].GetComponent<Damage>().damage = damage;

        }
        yield return new WaitForSeconds(5f);
        int j = 0;

        float ranChange = 0;
        float counterToChange = 0;
        bool changed = false;
        if (changeDirection == true)
        {
            ranChange = Random.Range(0f, 2f);
        }

        while (j < rotate_points)
        {
            
            GetComponent<Transform>().Rotate(0, 0, speed * direction);
            if(ranChange != 0 && changed == false)
            {
                counterToChange += 0.01f;
                if (counterToChange <= ranChange)
                {
                    Debug.Log("close to switch planes");
                    speed *= -1;
                    counterToChange = 0;
                    changed = false;
                }
            }
            j++;
            yield return new WaitForSeconds(0.03f);
        }
    }*/
}
