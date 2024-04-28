using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadiusWayPoints : MonoBehaviour
{
    public TextMeshPro text;
    public TextMeshPro value;
    public static float radius;

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
        v *= 50.0f;
        //v += 1000;



        radius = v;



        text.SetText((int)v + " cm");

        createWaypoints.SizeRadius = (radius/100f);
    }
}
