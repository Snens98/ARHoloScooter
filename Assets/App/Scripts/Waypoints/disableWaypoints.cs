using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableWaypoints : MonoBehaviour
{

    bool waypointsVisible = false;
    void Update()
    {   
        if ((RightHandTipMenuInputs.showResultText && RightHandTipMenuInputs.finishedDrive2 && waypointsVisible) || RightHandTipMenuInputs.finishedDrive)
        {
            //return;
        }


        // Wenn die Wegpunkt nicht duch die Hololens angezeigt werden sollen, sondern nur auf der Minimap
        if (switchButtonInputs.showWayPointsState == false)
        {
            if (RightHandTipMenuInputs.start_2)
            {
                foreach (GameObject o in createWaypoints.obj)
                {
                    if (switchButtonInputs.showWayPointsOnMap)
                    {
                        o.GetComponent<Renderer>().enabled = true;
                        o.layer = 3;                                // Layer der Wegpunkte ändern, damit die Wegpunkte nur auf der Minimap zu sehen sind
                    }
                    else
                    {
                        o.GetComponent<Renderer>().enabled = false;
                    }
                    waypointsVisible = false;
                }
            }          
        }





        // Wenn die Fahrt beendet wurde, sollen wirder alle Wegpunkte sichbat sein, um das Ergebniss zu sehen
        if (EndPosition.endPosCollide && RightHandTipMenuInputs.start_2 || RightHandTipMenuInputs.finishedDrive2)
        {
            foreach (GameObject o in createWaypoints.obj)
            {
                if (switchButtonInputs.showWayPointsOnMap)
                {
                    o.GetComponent<Renderer>().enabled = true;
                    o.layer = 6;
                }
                else
                {
                    o.GetComponent<Renderer>().enabled = true;
                }
                waypointsVisible = true;

            }
            RightHandTipMenuInputs.start_2 = false;
        }







        // Um die Wegpunkte auszubleden, die zu nah am Benutzer sind (Ausblend-Radius in den Einstellungen)
        if (switchButtonInputs.showWayPointsState == true && !(EndPosition.endPosCollide 
            && RightHandTipMenuInputs.start_2 || RightHandTipMenuInputs.finishedDrive2)) {

            foreach (GameObject o in createWaypoints.obj)
            {
                float distance = Vector3.Distance(Camera.main.transform.position, o.transform.position);
                if (distance < (RenderSlider.renderDistance / 100.0f) && RightHandTipMenuInputs.finishedDrive)
                {
                    if (switchButtonInputs.showWayPointsOnMap)
                    {
                        o.GetComponent<Renderer>().enabled = true;
                        o.layer = 3;
                    }
                    else
                    {
                        o.GetComponent<Renderer>().enabled = false;
                    }
                    waypointsVisible = false;
                }
                else
                {
                    if (switchButtonInputs.showWayPointsOnMap)
                    {
                        o.GetComponent<Renderer>().enabled = true;
                        o.layer = 6;
                    }
                    else
                    {
                        o.GetComponent<Renderer>().enabled = true;
                    }
                    waypointsVisible = true;
                }
            }
        }
    }
}
            
        
    
