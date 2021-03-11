using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHeartColor : MonoBehaviour
{
    public SpriteRenderer[] heartParts;
    public SpriteRenderer mainHeart;
    public Image[] mainNav;
    public Image secondaryNav;

    public Color mainColor = Color.red;
    public Color teleportedColor = Color.cyan;

    void Start()
    {
        UpdateColor(mainColor);
    }

    public void UpdateColor(Color color)
    {
        heartParts[0].color = color;
        heartParts[1].color = color;
        mainHeart.color = color;
        secondaryNav.color = color;

        for (int i = 0; i < 4; i++)
        {
            mainNav[i].color = color;
        }
    }

}
