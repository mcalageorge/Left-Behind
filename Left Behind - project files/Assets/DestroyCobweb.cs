using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCobweb : MonoBehaviour
{
    Attacks bossAttack;
    int roundUponDestroy;
    int difference = 3;
    MoveSoul pMovement;
    Manager man;


    void Start()
    {
        man = GameObject.Find("UI manager").gameObject.GetComponent<Manager>();
        man.webbed = true;
        bossAttack = GameObject.FindWithTag("enemy").gameObject.GetComponent<Attacks>();

        roundUponDestroy = bossAttack.round + difference;

        pMovement = GameObject.FindWithTag("Player").GetComponent<MoveSoul>();
        pMovement.speed = pMovement.slowSpeed;



        StartCoroutine(TurnCheck());
        Debug.Log("Cobweb: roundUponDestroy is " + roundUponDestroy + "; and round is " + bossAttack.round);
    }

    IEnumerator TurnCheck()
    {
        while (true)
        {
            if (bossAttack.round == roundUponDestroy)
            {
                pMovement.speed = pMovement.normalSpeed;
                
                break;
            }
            yield return new WaitForSeconds(0.1f);

        }
        pMovement.speed = pMovement.normalSpeed;
        man.webbed = false;
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }    
}
