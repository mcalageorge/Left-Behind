using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrder : MonoBehaviour
{
    Attacks attacks;

    void Start() 
    {
        attacks = this.gameObject.GetComponent<Attacks>();
    }


    //rightattack Easy
    public IEnumerator Attack1()
    {
        StartCoroutine(attacks.RightAttack());
        while (attacks.rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        attacks.box2.afterbossSpeech = true;
    }

    // six easy
    public IEnumerator Attack2()
    {
        StartCoroutine(attacks.Six());
        while (attacks.six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        attacks.box2.afterbossSpeech = true;
    }

    // rain easy
    public IEnumerator Attack3()
    {
        StartCoroutine(attacks.Rain());
        while (attacks.rain == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        attacks.box2.afterbossSpeech = true;
    }

    // right attack and six medium
    public IEnumerator Attack4()
    {
        StartCoroutine(attacks.RightAttack());
        while (attacks.rightAttack == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine(attacks.Six());
        while (attacks.six == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        attacks.box2.afterbossSpeech = true;
    }
}
