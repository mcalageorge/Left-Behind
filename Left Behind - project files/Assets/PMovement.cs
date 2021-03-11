using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
    public SpriteRenderer walking_picture;
    // Start is called before the first frame update
    void Start()
    {
        walking_picture = GetComponentInChildren<SpriteRenderer>();
    }

    //sprites for anim on x axis

    public Sprite[] wl = new Sprite[4];
    public Sprite[] wr = new Sprite[4];

    //sprites for anim on y asix :: back and forth ::

    public Sprite[] wc = new Sprite[4];
    public Sprite[] wcs = new Sprite[4];

    //variables

    public float speed = 0.1f;

    float timer = 7f;

    int anim_order1 = 0;
    int anim_order2 = 0;

    string facing = "camera";

    string focus = "";

    float translation_v;
    float translation_h;

    public int help = 5;
    bool chosen = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        translation_v = Input.GetAxisRaw("Vertical") * speed;

        translation_h = Input.GetAxisRaw("Horizontal") * speed;

        //reduces timer if no movement is registered

        if (translation_h == 0 && timer > 0 || translation_v == 0 && timer > 0)
        {
            timer -= 10f * Time.deltaTime;
        }

        //this will check which one of the axis was moved first so that we can see which way to face the character even if he uses the other axis

        if (chosen == false)
        {
            if (translation_h > 0)
            {     
                help = 1;
                timer = 0;
                chosen = true;
            }
            if(translation_h < 0)
            {
                
                help = 3;
                timer = 0;
                chosen = true;
            }
            if(translation_v > 0)
            {
                help = 0;
                timer = 0;
                chosen = true;
            }
            if(translation_v < 0)
            {
                help = 2;
                timer = 0;
                chosen = true;
            }
            
        }

        //this will simply play the required animation depending on the 'help' variable from above

        switch (help)
        {
            case 0:
                {
                    CycleWalkingAnimations(wcs);
                    facing = "away";
                    break;
                }
            case 1:
                {
                    CycleWalkingAnimations(wr);
                    facing = "left";
                    break;
                }
            case 2:
                {
                    CycleWalkingAnimations(wc);
                    facing = "camera";
                    break;
                }
            case 3:
                {
                    CycleWalkingAnimations(wl);
                    facing = "right";
                    break;
                }
        }

        if (help == 0 && translation_v <= 0)
            chosen = false;
        if (help == 2 && translation_v >= 0)
            chosen = false;
        if (help == 1 && translation_h <= 0)
            chosen = false;
        if (help == 3 && translation_h >= 0)
            chosen = false;

        //when input = 0, faces character in the last known direction

        if (facing == "camera" && translation_h == 0 && translation_v == 0) { walking_picture.sprite = wc[0];  }
        else if (facing == "away" && translation_h == 0 && translation_v == 0) { walking_picture.sprite = wcs[0];  }
        else if (facing == "right" && translation_h == 0 && translation_v == 0) { walking_picture.sprite = wl[0];   }
        else if (facing == "left" && translation_h == 0 && translation_v == 0) { walking_picture.sprite = wr[0];  }

        if ((translation_h == 0 && translation_v == 0) || (translation_h != 0 && translation_v == 0) || (translation_h == 0 && translation_v != 0)) focus = "";

        //this simply moves the player object on the axes

        transform.Translate(translation_h, translation_v, 0);
    }

    void CycleWalkingAnimations(Sprite[] sprites)
    {
        if(translation_h == 0 || translation_v == 0)
        timer -= 10f * Time.deltaTime;
        else
        timer -= 10f * Time.deltaTime * 2;

        walking_picture = GetComponentInChildren<SpriteRenderer>();

        if (timer <= 0)
        {

            Sprite anim = sprites[anim_order2];
            walking_picture.sprite = anim;
            if (anim_order2 == 0 || anim_order2 == 2)
                timer = 3f;
            else
                timer = 6f;

            anim_order2++;
        }
        if (anim_order2 > 3) anim_order2 = 0;
    }
}
       
    
