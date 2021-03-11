using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttackFromRightMove : MonoBehaviour
{
   
    public float speed = 1f;
    public string direction = "left";

    public int Dmg;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("attack");
        
        q = gameObject.GetComponent<SpriteRenderer>();
    }

    bool started = false;

    SpriteRenderer q;
    float w = 1f;

    bool down = false;

    IEnumerator attack()
    {
        yield return new WaitForSeconds(0.05f);
        switch (direction)
        {
            case "left": 
                {
                    for (int i = 0; i < 130; i++)
                    {
                        Debug.Log("speed is " + speed);
                        this.GetComponent<Transform>().Translate(-0.05f * speed, 0, 0, Space.World);
                        this.GetComponent<Transform>().Rotate(0, 0, 10f);
                        if (i >= 100) {
                            if (down == false) { gameObject.GetComponent<CircleCollider2D>().enabled = false; down = true; }
                            w -= 0.04f;
                            q.color = new Color(1f, 1f, 1f, w);
                            yield return new WaitForSeconds(0.02f);
                        }
                            
                        
                        yield return new WaitForSeconds(0.01f);
                        
                    }
                    Destroy(this.gameObject);
                    break;
                }
            case "right":
                {
                    for (int i = 0; i < 130; i++)
                    {
                        this.GetComponent<Transform>().Translate(0.05f * speed, 0, 0, Space.World);
                        this.GetComponent<Transform>().Rotate(0, 0, -10f);
                        if (i >= 100) {
                            if (down == false) { gameObject.GetComponent<CircleCollider2D>().enabled = false; down = true; }
                            w -= 0.04f;
                            q.color = new Color(1f, 1f, 1f, w);
                            yield return new WaitForSeconds(0.04f);
                        }
                            
                        yield return new WaitForSeconds(0.01f);
                    }
                    Destroy(this.gameObject);
                    break;
                }
          /*  case "upwards":
                {
                    for (int i = 0; i < 130; i++)
                    {
                        this.GetComponent<Transform>().Translate(0, 0.05f * speed, 0, Space.World);
                        this.GetComponent<Transform>().Rotate(0, 0, 10f);
                        if (i >= 100) {
                            w -= 0.04f;
                            q.color = new Color(1f, 1f, 1f, w);
                            yield return new WaitForSeconds(0.04f);
                        }
                           
                        yield return new WaitForSeconds(0.01f);
                    }
                    Destroy(this.gameObject);
                    break;
                }*/
            case "downwards":
                {
                    for (int i = 0; i < 130; i++)
                    {
                        this.GetComponent<Transform>().Translate(0, -0.05f * speed, 0, Space.World);
                        this.GetComponent<Transform>().Rotate(0, 0, -10f);
                        if (i >= 100) {
                            if (down == false) { gameObject.GetComponent<CircleCollider2D>().enabled = false; down = true; }
                            w -= 0.04f;
                            q.color = new Color(1f, 1f, 1f, w);
                            yield return new WaitForSeconds(0.04f);
                        }
                             
                        yield return new WaitForSeconds(0.01f);
                    }
                    Destroy(this.gameObject);
                    break;
                }
            default:
                {
                    Debug.Log("how did we even get here");
                    break;
                }
        }
        
    }
    void OnTriggerEnter2D(Collider2D collided)
    {
        Debug.Log("asdas");

        entered = true;
        if (collided.gameObject.tag == "Player")
        {
            collided.gameObject.GetComponent<HP>().ChangeHP(Dmg);
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

    }

    bool entered = false;

    

    /*void OnTriggerExit2D(Collider2D collided)
    {
        Debug.Log("Stayed");

            
            entered = false;
            q = null;
    }*/
}
