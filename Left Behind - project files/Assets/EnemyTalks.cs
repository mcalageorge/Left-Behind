using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTalks : MonoBehaviour
{
    public AudioClip rap;
    public Text text;

    public string[] lines = new string[6];

    float timer_for_each_letter = 0f;

    int i = 0;
    int j = 0;

    bool reachedend = false;

    string s;

    // Start is called before the first frame update
    void Start()
    {
        
        s = lines[i];

        InvokeRepeating("WriteEachLetter", 1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            reachedend = true;
        }
    }

    void WriteEachLetter()
    {
        if (s[j] != '9')
        {
            text.text = text.text + s[j];
            GetComponent<AudioSource>().Play();
            j++;
            reachedend = false;
        }




        if (s[j] == '9' && reachedend == true)
        {
            i++;
            j = 0;
            s = lines[i];
            text.text = "";

            Debug.Log("J IS " + j + " AND I IS " + i);



        }
    }
}
