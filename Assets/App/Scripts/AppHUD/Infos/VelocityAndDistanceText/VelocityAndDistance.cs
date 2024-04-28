using TMPro;
using UnityEngine;

public class VelocityAndDistance : MonoBehaviour
{

    public TextMeshPro text;

    void Update()
    {
        //Die Geschwindigkeit und Distanz anzeigen
        text.SetText("V: " + Mathf.Round(Vector3.Magnitude(Camera.main.velocity*4.3f) * 10f) / 10f + 
            "\nD: " + Mathf.Round((drivenDistance.GetDistanceTraveled()) * 100f) / 100f);
    }
}
