using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainAttack : MonoBehaviour
{

    void Start()
    {
       
    }

  /*  IEnumerator SpawnRocks()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 75; j++)
            {
                int rndPos = Random.Range(0, 5);
                int rndSprite = Random.Range(0, 3);
                rock.GetComponent<SpriteRenderer>().sprite = sprites[rndSprite];
                Instantiate(rock, new Vector3(pointsX[rndPos], 7, -4), transform.rotation);

                yield return new WaitForSeconds(0.3f);
            }

            yield return new WaitForSeconds(3f);
        }
    }*/
}
