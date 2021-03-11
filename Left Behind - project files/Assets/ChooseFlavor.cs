using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFlavor : MonoBehaviour
{
    public string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        int ran = Random.Range(0, lines.Length);

        this.gameObject.GetComponent<Text>().text = lines[ran];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
