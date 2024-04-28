using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSlider : MonoBehaviour
{

    public TextMeshPro text;
    public TextMeshPro value;
    public static float time;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        string s = value.text.Replace(',', '.');

        float v = float.Parse(s);

        v /= 1.0f;
        v *= 2.0f;

        time = v;

        text.SetText(v + " s");
    }
}
