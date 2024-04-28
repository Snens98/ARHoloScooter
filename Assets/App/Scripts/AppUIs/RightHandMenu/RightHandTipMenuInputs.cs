using TMPro;
using UnityEngine;

public class RightHandTipMenuInputs : MonoBehaviour
{
    public static bool startGame = false;
    public static bool restartGame = false;
    public static bool finishedDrive = false;
    public static bool finishedDrive2 = false;


    public GameObject timer;
    public GameObject velocity;
    public GameObject endPosition;
    public GameObject startPosition;
    public GameObject camPosition;
    public GameObject closethis;

    public GameObject openSettingsMenu;
    public GameObject openSettingsCloseButton;

    public static float startTimer = 6.0f;
    public TextMeshPro CoutdownText;
    public static bool count = false;









    //Route fahren. Startpunkt wird gesetzt
    public void start()
    {
        if (!startGame)
        {
            count = true;                                      // Countdown starten
            startPosition.SetActive(true);                     // Startpunkt aktivieren           

            //Startpunkt an aktuelle Position setzen
            Vector3 startPos = Camera.main.transform.position;
            startPos.y -= (((Slider.Bodyheight / 100.0f)) - 0.4f);
            startPosition.transform.position = startPos;

            cam.orthographicSize = 3;                           //Minimap-zoom zurücksetzen
            RightHandTipMenuInputs.evaluationState = false;
            close = false;
            //UnityEngine.XR.InputTracking.Recenter();
        }
    }


    



    // Strecke fahren im Menu beenden. Endpunkt wird gesetzt
    public void finished()
    {
        if (startGame && !finishedDrive && !start_2)
        {
            finishedDrive = true;               // Fahrt beenden
            endPosition.SetActive(true);        // Ensposition aktivieren
            camPosition.SetActive(true);        // Kollisionsbox aktivieren

            //Endpunkt an aktuelle Position setzen
            Vector3 endPos = Camera.main.transform.position;
            endPos.y -= (((Slider.Bodyheight / 100.0f)) - 0.4f);
            endPosition.transform.position = endPos;
        }
    }






    // Route fahren oder auch Route nachfahren zurücksetzen
    public void restart()
    {
        restartGame = true;
    }





    public static bool createMap = false;
    public static bool start_2 = false;

    public static bool calculateMistakes = false;
    public static bool showResultText = false;



    // Route nachfahren starten
    public void start2()
    {
        if (!Timer.timeIsOver)
        {
            //Vorherige Werte zurücksetzen
            createWaypoints.wrongWaypoints = 0;
            createWaypoints.accurate = 0.0f;
            createWaypoints.i = 0;
            drivenDistance.lastPosition = Camera.main.transform.position;

            //versuch+1
            tries++;

            //Ergebis berechnen anzeigen und anzeigen
            showResultText = true;
            calculateMistakes = true;

            finishedDrive2 = false;
            createMap = true;
            start_2 = true;
            MenuToggleRedrive.enable = false;

            cam.orthographicSize = 3;
            evaluationState = false;
            close = false;

            openEvaluation.SetActive(false);
        }
    }




    public GameObject retryObj;
    public GameObject openStart2;
    public Camera cam;

    public static int tries = 0;



    /*
    //Neuen Versuch starten, um die Route nachzufahren
    public void retry()
    {
        foreach (GameObject o in createWaypoints.obj)
        {
            o.GetComponent<Renderer>().material.color = new Color(0.9f, 0.9f, 1.0f, 0.8f);
        }

        drivenDistance.resetDistanceDuringRedrive();
        RightHandTipMenuInputs.evaluationState = false;
        cam.orthographicSize = 3;


        drivenDistance.distanceTraveled = 0.0f;
        createWaypoints.i = 0;
        createWaypoints.wrongWaypoints = 0;
        createWaypoints.accurate = 0;
        Timer.tie = 0;
        tries = 0;

        Timer.timeIsOver = false;
        createWaypoints.accurate = 0;
        close = false;

        RightHandTipMenuInputs.finishedDrive2 = false;
        Timer.timeIsOver = false;
        openStart2.SetActive(true);
        retryObj.SetActive(false);
    }


    */






    public static bool evaluationState = false;
    public GameObject disableMap;
    public GameObject closeThis;
    public GameObject openEvaluation;
    public GameObject closeButtonEval;


    public static bool close = false;
    public static bool clickOnEvaluation = false;




    //Evaluations-Fester anzeigen und richtig positionieren
    public void evaluation()
    {
        close = true;
        clickOnEvaluation = true;



        Vector3 f = Camera.main.transform.forward;
        f *= 1.3f;

        Vector3 pos = Camera.main.transform.position;
        pos.y -= 0.1f;
        pos.x += f.x;
        pos.z += f.z;

        Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        //eulerAngles.y = 0;
        //eulerAngles.z = 0;

        openEvaluation.transform.eulerAngles = eulerAngles;

        openEvaluation.transform.position = pos;

        evaluationState = true;

        closeThis.SetActive(false);
        openEvaluation.SetActive(true);
        retryObj.SetActive(false);
        openStart2.SetActive(true);
        closeButtonEval.SetActive(true);

    }





    //Evaluations-Fenster schließen
    public void closeEval()
    {

        evaluationState = false;
        close = false;
    }
}
