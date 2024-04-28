using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPositionOnMap : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

        // Objekte mit der Kamera (Benutzerposition) bewegen, aber die Rotation der Kamera ignorieren
        Vector3 pos = Camera.main.transform.position;
        pos.y += 10.0f;                                 //Objekt 10 Meter nach oben verschieben, um auﬂerhalb des Blickfeldes zu sein

        this.gameObject.transform.position = pos;

        // Objekt richtig rotieren
        Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
        eulerAngles.x = 90;    
        //eulerAngles.y = 0;
        eulerAngles.z = 0;

        transform.eulerAngles = eulerAngles;
    }
}
