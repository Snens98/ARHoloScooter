using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBox : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

        //Die Kollisionsbox ist immer an der Position der Hololens und hat die Breite des E-Scooters

        Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        //eulerAngles.y = 0;
        eulerAngles.z = 0;

        transform.eulerAngles = eulerAngles;
    }
}
