using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrivingTimeSlider : MonoBehaviour
{

    public TextMeshPro text;
    public TextMeshPro value;
    public static float drivingTime;

    void Start()
    {
        
    }

    void Update()
    {
        string s = value.text.Replace(',', '.');

        float v = float.Parse(s);

        v /= 1.0f;
        v *= 60.0f;
        //v += 1000;

 

        drivingTime = v;



        text.SetText((int)v + " s");

        Timer.maxTime = (int)drivingTime;
    }
}
