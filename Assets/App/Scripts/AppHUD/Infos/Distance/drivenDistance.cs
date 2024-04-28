using UnityEngine;

public class drivenDistance : MonoBehaviour
{
    public static float distanceTraveled = 0f;
    public static float distance = 0f;
    static float timePassed = 0f;

    private Transform tx;
    public static Vector3 lastPosition;

    void Start()
    {
        //Letzte Position des Benutzers am Start speichern
        tx = Camera.main.transform;
        lastPosition = tx.position;
    }


    public static float distanceDuringCreateRoute = 0.0f;
    public static float distanceDuringRedrive = 0.0f;

    public static bool setDistanceDuringCreateRoute = true;
    public static bool setDistanceDuringRedrive = false;



    void Update()
    {
        //Letzte Position zurücksetzen, wenn der Timer abgelaufen ist.
        resetLastpositionWhenDriveEnded(); 

        if (DistanceCanBeCalculated())
        {
            calcDistance();
        }

        saveAndResetDistances();
    }






    private bool DistanceCanBeCalculated()
    {
        if (Camera.main.velocity.magnitude > 0.2f) //Kleine Kopfbewegungen ignorieren
        {
            // Distanz erst berechnen, wenn fahrt startet
            if ((!RightHandTipMenuInputs.finishedDrive && RightHandTipMenuInputs.startGame) 
                || (RightHandTipMenuInputs.start_2 && !RightHandTipMenuInputs.finishedDrive2))
            {
                return true;
            }
        }
        return false;
    }






    //Letzte Position zurücksetzen, wenn der Timer abgelaufen ist.
    private void resetLastpositionWhenDriveEnded()
    {
        if (RightHandTipMenuInputs.finishedDrive2)
        {
            lastPosition = Camera.main.transform.position;
        }
    }





    // Distanz berechnen
    private void calcDistance()
    {
        distanceTraveled += ((lastPosition - Camera.main.transform.position).magnitude);
        distance = distanceTraveled;
        timePassed += Time.deltaTime;
        lastPosition = Camera.main.transform.position;
    }





    // Die Distanzen speichen und dann zurücksetzen
    private void saveAndResetDistances()
    {

        if (RightHandTipMenuInputs.startGame && RightHandTipMenuInputs.finishedDrive)
        {
            SaveAndResetDistanceDuringCreateRoute();
            setDistanceDuringCreateRoute = false;
        }


        if (RightHandTipMenuInputs.clickOnEvaluation)
        {
            SaveDistanceDuringRedrive();
            RightHandTipMenuInputs.clickOnEvaluation = false;
        }
    }





    // Die Distanz speichen und dann zurücksetzen nach erstellen der Route
    public void SaveAndResetDistanceDuringCreateRoute()
    {
        if (setDistanceDuringCreateRoute)
        {
            distanceDuringCreateRoute = Mathf.Round((GetDistanceTraveled()) * 100f) / 100f;
            distance = 0;
            distanceTraveled = 0;
        }
    }



    // Die Distanz nur speichen nach dem nachfahren, Damit die Distanzen später zusammengerechnet werden können, bei mehreren versuchen
    public void SaveDistanceDuringRedrive()
    {
        distanceDuringRedrive = Mathf.Round((GetDistanceTraveled()) * 100f) / 100f;    
    }




    // Die Distanz nur Zurücksetzen nach dem nachfahren
    public void ResetresetDistanceDuringRedrive()
    {
        if (setDistanceDuringRedrive)
        {
            distance = 0;
            distanceTraveled = 0;
        }
    }



    public static void resetDistanceDuringRedrive()
    {

        setDistanceDuringRedrive = true;
        distanceDuringRedrive = 0;
        distanceTraveled = 0;
    }





    //Gibt die aktuelle Distanz zurück
    public static float GetDistanceTraveled()
    {
        return distance;
    }



    // Die Distanz nur Zurücksetzen nach erstellen der Route

    public static void resetDistanceDuringCreateRoute()
    {

        setDistanceDuringCreateRoute = true;
        distanceDuringCreateRoute = 0;
        distanceTraveled = 0;
    }

}
