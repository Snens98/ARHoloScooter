using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceSlider : MonoBehaviour
{

    public TextMeshPro text;
    public TextMeshPro value;
    public static float wayPointDistance;

    
    void Update()
    {
        //Da wir ein Prefab für die Slider benutzen und die anstatt ein punkt ein Komma haben, mussen wir das komma mit einem Punkt ersetzen
        string s = value.text.Replace(',', '.');

        //Den Wert des Sliders in ein Float konvertieren
        float v = float.Parse(s);

        //Umrechnen
        v /= 1.0f;
        v *= 1000.0f;
        wayPointDistance = v;

        //Wert anzeigen
        text.SetText(v + "");
    }
}
