using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RenderSlider : MonoBehaviour
{


    public TextMeshPro text;
    public TextMeshPro value;
    public static float renderDistance;

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
        v *= 800.0f;

        renderDistance = v;

        text.SetText(v + " cm");
    }
}
