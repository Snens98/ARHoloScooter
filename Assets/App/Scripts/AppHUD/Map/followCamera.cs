using UnityEngine;

public class followCamera : MonoBehaviour
{

    public static Vector3 middlePoint = new Vector3(0, 0, 0);
    public Camera cam;

    void Update()
    {

        //Berechnung erst durchführen, wenn der 1. Wegpunkt erstellt wurde
        if (createWaypoints.obj.Count < 1) {
            return;
        }

        // Wenn das Evaluations-Fenster offen ist, soll der Mittelpunkt der gesamten Strecke berechnet
        // werden und so weit rausgezoom werden,
        // damit man die komplette Route sehen kann.
        if (RightHandTipMenuInputs.evaluationState)
        {
            middle();       //Mittelpunkt der Strecke berechnen
            correctZoom();  // Rauszoomen, bis komplette Strecke sichtbar ist
            rorate();       //Strecke Rotieren
        }
        else
        {
            calcMiddlePoint();  // Die Hololens bzw. Benutzerposition soll immer der Mittelpunkt der Map sein
            rorate();           // Die Map richtig rotieren
        }
    }





    // Die Map richtig rotieren, nach Blickrichtung des benutzers
    private void rorate()
    {
        Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
        eulerAngles.x = 90;
        eulerAngles.z = 0;
        transform.eulerAngles = eulerAngles;
    }




    // Die Hololens bzw. Benutzerposition soll immer der Mittelpunkt der Map sein
    private void calcMiddlePoint()
    {
        middlePoint = Camera.main.transform.position;
        middlePoint.y = this.gameObject.transform.position.y;

        this.gameObject.transform.position = new Vector3(middlePoint.x, middlePoint.y, middlePoint.z);
    }






    //Mittelpunkt der gesamten Strecke berechnen
    Vector3 points = new Vector3(0, 0, 0);

    private void middle()
    {
        foreach(GameObject o in createWaypoints.obj)
        {
            points+=o.transform.position;       // Alle Positionen der Wegpunkte zusammenrechnen 
        }

        points /= createWaypoints.obj.Count;    // Gesamtposition der Wegpunkte durch die Anzahl teilen
        points.y = this.gameObject.transform.position.y;    // Y-Position ignorieren

        // Kamera, die das Bild für die Minimap erstellt am Mittelpunkt der Strecke setzen
        this.gameObject.transform.position = new Vector3(points.x, points.y, points.z);

    }









    // Rauszoomen, bis komplette Strecke sichtbar ist
    Plane[] cameraFrustum;
    Collider collider;

    Vector3 vec; 
    private void correctZoom()
    {
        foreach (GameObject o in createWaypoints.obj) // Liste aller Wegpunkte wird durchlaufen
        {

            // Kollisionsbox der Wegpunkte
            collider = o.GetComponent<Collider>();   
            var bounds = collider.bounds;           

            /*
            vec += o.transform.position;
            vec /= createWaypoints.obj.Count;
            vec.y = this.gameObject.transform.position.y;
            this.gameObject.transform.position = new Vector3(vec.x, vec.y, vec.z);
            */


            cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);

            //Wenn von der Kamera aus (Die von Oben die Wegpunkte filmt), nicht alle Wegpunkte sichbar sind
            if (!GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
            {
                cam.orthographicSize++;     //Rauszoomen
            }
        }
    }
 }
