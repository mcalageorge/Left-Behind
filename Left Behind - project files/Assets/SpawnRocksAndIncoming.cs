using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocksAndIncoming : MonoBehaviour
{
    public GameObject rock;


    Vector3 initial_rotation;
    float degree_rotation = 17f;

    float distance = 5f;

    public int nrOfRocks = 10;

    public Transform dummy;

    void Start()
    {
        StartCoroutine("InstantiateRocks");
    }

    Transform transform;
    
    IEnumerator InstantiateRocks()
    {
        for (int j = 0; j < 5; j++)
        {

            float z = Random.Range(0f, 361f);
            Debug.Log(z + " Z is");
            GetComponent<Transform>().Rotate(0, 0, z);

            for (int i = 0; i < nrOfRocks; i++)
            {

                GameObject q = Instantiate(rock, dummy.position, Quaternion.identity);


                GetComponent<Transform>().Rotate(0, 0, -180);

                q.GetComponent<LookAndMoveToPlayer>().targetPos = dummy.position;

                GetComponent<Transform>().Rotate(0, 0, 180 + degree_rotation);

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(4f);
        }
    }

}
