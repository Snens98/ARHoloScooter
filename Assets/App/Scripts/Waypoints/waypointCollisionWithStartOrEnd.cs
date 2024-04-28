using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointCollisionWithStartOrEnd : MonoBehaviour
{

    public GameObject endPosition;
    public GameObject startPosition;




    // Wenn Wegpunkte innerhalb von Start oder Endpunkt sind, wird die farbe von weiß auf grün geändert
    // Es kann vorkommen, dass ein Wegpunkt im Endpunkt ist, dann kann der Benutzer diesen Endpunkt nicht erreichen.
    // Damit der benuter trotzdem 100% erreichen kann wird dieses Wegpunkt dann automatisch als "Durchfahren" markiert
    private void OnTriggerStay(Collider other)
    {

        if (endPosition != null && startPosition != null)
        {
            if (other.gameObject.tag.Equals(endPosition.gameObject.tag))
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0.35f, 1.0f, 0.0f, 0.6f); //Grün
                gameObject.GetComponent<Renderer>().enabled = false;
            }

            if (other.gameObject.tag.Equals(startPosition.gameObject.tag))
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0.35f, 1.0f, 0.0f, 0.6f); //Grün
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
