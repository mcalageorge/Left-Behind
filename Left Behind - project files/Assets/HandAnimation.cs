using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (anim.GetBool("isExtended") == false)
            {
                anim.SetTrigger("extend");
                anim.SetBool("isExtended", true);
            }
            else
            {
                
                anim.SetBool("isExtended", false);
            }

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
                anim.SetTrigger("attack");
        }
    }

    public void AttackAnimation()
    {
        anim.SetTrigger("attack");
    }
    public void ExtendAnimation()
    {
        if (anim.GetBool("isExtended") == false)
        {
            anim.SetTrigger("extend");
            anim.SetBool("isExtended", true);
        }
        else
        {
            anim.SetBool("isExtended", false);
        }
    }
}
