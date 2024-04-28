using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slider : MonoBehaviour
{


    public TextMeshPro text;
    public TextMeshPro value;
    public static float Bodyheight = 180;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        string s = value.text.Replace(',', '.');

        float v = float.Parse(s);

         v /= 1.0f;
        v *= 200.0f;
       // v += 5000;

       

        Bodyheight = v;

        text.SetText(v + " cm");

    }
}
