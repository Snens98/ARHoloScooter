using UnityEngine;

public class DirectionArrowRotation : MonoBehaviour
{
    //Wenn kein Objekt vom Richtungspfeil gefunden wird, soll der Richtungspfeil auf den EndPunkt zeigen
    public GameObject endPosition;


    void Update()
    {

       // Der Richtungspfeil soll nicht durchgehend an sein, weil er viel Leistung kostet
       if (RightHandTipMenuInputs.finishedDrive && RightHandTipMenuInputs.start_2 && switchButtonInputs.showDirectionArrow)
        {

            this.gameObject.GetComponent<Renderer>().enabled = true;//Richtungspfeil aktivieren

            transform.LookAt(FindClosestObject(50f, 0.6f).transform, Vector3.up); //Richtungspfeil zum nähesten Wegpunkt zeigen lassen

            ignoreZRotation(); // Richtungspfeil richtig ausrichten und die Rotation um die Z-Achse deaktivieren
        }
        else
        {
           this.gameObject.GetComponent<Renderer>().enabled = false; // Richtungspfeil deaktivieren
        }
    }




    
    private Plane[] cameraFrustum;
    private Collider collider;



    // Findet den nächstgelegenen Wegpunkt, der nicht grün ist (Also nicht nachgefahren wurde) und aus der Kamera aus zu sehen ist
    public GameObject FindClosestObject(float max, float min)
    {

        GameObject closest = endPosition;

        float distance = max;
        Vector3 position = Camera.main.transform.position; //Position des Benutzers speichern

        Vector3 cam = position;
        cam.y = 0;      // y-Wert löschen, um den Höhenuntschied zwischen Benutzer und Gameobjekt zu ignorieren

        foreach (GameObject o in createWaypoints.obj)  // Jeden Wegpunkt in der Liste durchlaufen
        {

            Vector3 points = o.transform.position;
            points.y = 0;   // y-Wert löschen, um den Höhenuntschied zwischen Benutzer und Gameobjekt zu ignorieren

            float curDistance = Vector3.Distance(points, cam); // Distanz zwischen Benutzer und den wegpunkten berechnen


            if (curDistance < distance && curDistance > min)    // Wenn in einen Bestimmten Abstand zu einem Wegpunkt ist (zwischen 0.8 und 50 Metern),
                                                                // und der nähesten Wegpunkt zum Benutzer
            {
                if (waypointIsGreen(o))                         // Wenn der Wegpunkt grün ist
                {
                    if (waypointsAreVisibleFromCamera(o))       // Wenn der Wegpunkt von der Kamera aus zu sehen ist
                    { 

                        closest = o;                            // aktuellen Wegpunkt aus der Liste (o) zum nähesten Wegpunkt machen
                        distance = curDistance;                 // Distanz zurücksetzen
                    }
                }
            }
        }

        return closest; // den nächstgelegenen Wegpunkt von der Aktuellen Benutzerposition zurückgeben
    }






    //Alle Wegpunkte, die grün sind, also die man richtig nachgefahren hat, sollen ignoriert werden.
    private bool waypointIsGreen(GameObject o)
    {
        Color c = o.gameObject.GetComponent<Renderer>().material.color; //Farbe der Wegpunkte speichern
        return (c.g != 1.0f);
    }




    //Nur Wegpunkte beachten, die von der Hololens aus zu sehen sind. (Damit Wegpunkte hinter uns ignoriert werden)
    private bool waypointsAreVisibleFromCamera(GameObject o)
    {
        collider = o.GetComponent<Collider>();
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        return (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds));
    }    




    private void ignoreZRotation()
    {

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = -90;
        //eulerAngles.y = 0;
        eulerAngles.z = 180;

        transform.eulerAngles = eulerAngles;
    }
}


