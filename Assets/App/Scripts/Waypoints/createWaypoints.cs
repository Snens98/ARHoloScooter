using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class createWaypoints : MonoBehaviour
{

    public GameObject line; 
    public GameObject startPoint;
    public GameObject endPoint;

    public BoxCollider box;
    public SphereCollider sphere;
    public Renderer renderer;

    public static float Spawntimer = 0.5f;

    public static float SizeRadius = 0.25f;
    public float positionOffset = 0.5f;

    public static List<GameObject> obj = new List<GameObject>();

    public static int wrongWaypoints = 0;
    public static float accurate = 0.0f;






    public static int i = 0;


    //Zählt die Wegpunkte die der benutzer nicht durchfahren hat
    private void countWrongWaypoints()
    {
        foreach (GameObject o in obj)
        {
            Color c = o.gameObject.GetComponent<Renderer>().material.color;

            if (c.b == 1.0f)
            {
                i++;
            }
        }
        wrongWaypoints = i;
        RightHandTipMenuInputs.calculateMistakes = false;
    }
        
    




    //Die Genauigkeit der nachgefahrenen Strecke in Prozent zu berechnen
    private void calcAccurate()
    {
        float maxNumber = obj.Count;

        accurate = ((maxNumber- wrongWaypoints) / maxNumber)*100.0f;
    }






    public GameObject evaluation;
    public GameObject finishMenu;
    public Camera cam;
    public GameObject Campos;



    //App komplett zurücksetzen
    private void resetOnClickReset()
    {
        //Wegpunkte, Start- und Endpunkt löschen
        foreach (GameObject o in obj)
        {
            DestroyImmediate(o);
        }
        obj.Clear();

        //Startpunkte, Endpunkt, Kollisionsbox deaktivieren
        startPoint.SetActive(false);
        endPoint.SetActive(false);
        Campos.SetActive(false);

        // Appstart zurücksetzen
        RightHandTipMenuInputs.startGame = false;
        RightHandTipMenuInputs.finishedDrive = false;

        //Fahrzeit zurücksetzen
        Timer.resetTimer = true;
        Timer.timeEnded = false;
        Timer.timeIsOver = false;

        //Nachfahren Ergebnisse zurücksetzen
        RightHandTipMenuInputs.showResultText = false;
        i = 0;
        wrongWaypoints = 0;
        RightHandTipMenuInputs.calculateMistakes = false;
        accurate = 0;

        //Fahren beenden
        RightHandTipMenuInputs.start_2 = false;
        RightHandTipMenuInputs.finishedDrive2 = false;
        RightHandTipMenuInputs.close = false;
        RightHandTipMenuInputs.clickOnEvaluation = false;


        //Distanzen zurücksetzen
        drivenDistance.resetDistanceDuringCreateRoute();
        drivenDistance.resetDistanceDuringRedrive();
        drivenDistance.distanceTraveled = 0.0f;

        //Menus deaktivieren
        evaluation.SetActive(false);
        finishMenu.SetActive(false);
        RightHandTipMenuInputs.evaluationState = false;

        //Versuche zurücksetzen
        RightHandTipMenuInputs.tries = 0;

        //Map-Zoom zurücksetzen
        cam.orthographicSize = 3;

        //Countdown zurücksetzen
        count = false;
        startTimer = 4.0f;
        Timer.startTimer = 6.0f;
        Timer.startCountdown = false;
        RightHandTipMenuInputs.count = false;
        RightHandTipMenuInputs.startTimer = 4.0f;
        countdownText.SetText("");


        LeftHandMenu.resetApp = false;
    }



    public GameObject settings;
    public GameObject settingsCloseBTN;
    public TextMeshPro text;

    float startTimer = 4.0f;
    public TextMeshPro countdownText;
    public bool count = false;

    Vector3 wayPointPosition;


    //Start-Countdown anzeigen
    public void countdown()
    {

        if (startTimer > -2)
        {
            startTimer -= Time.deltaTime;
        }

        countdownText.SetText((int)startTimer + "");

        if (startTimer <= 1.0f && startTimer > -1)
        {
            RightHandTipMenuInputs.startGame = true;

            countdownText.SetText("LOS!");
        }

        if (startTimer <= 0.25)
        {
            countdownText.SetText("");
            RightHandTipMenuInputs.startGame = true;
            RightHandTipMenuInputs.count = false;
            startTimer = 4.0f;
        }
    }





    void Update()
    {
        //Start-Countdown
        if (RightHandTipMenuInputs.count)
        {
            countdown();
        }

        //App Zurücksetzen, wenn nötig
        if (Input.GetKeyDown(KeyCode.R) || LeftHandMenu.resetApp)
        {
            resetOnClickReset();
        }


        //Route fahren starten
        if (Input.GetKeyDown(KeyCode.S)) //Zum testen
        {
            if (!RightHandTipMenuInputs.startGame)
            {

                RightHandTipMenuInputs.count = true;

                startPoint.SetActive(true);

                Vector3 startPos = Camera.main.transform.position;
                startPos.y -= (((Slider.Bodyheight / 100.0f)) - 0.4f);
                startPoint.transform.position = startPos;

                cam.orthographicSize = 3;
                RightHandTipMenuInputs.evaluationState = false;
                RightHandTipMenuInputs.close = false;
                //UnityEngine.XR.InputTracking.Recenter();
            }
        }






        //Route vorfahren im Menu beenden und Endpunkt an der Stelle setzen
        if (Input.GetKeyDown(KeyCode.F)) //Zum testen
        {
            if (RightHandTipMenuInputs.startGame && !RightHandTipMenuInputs.finishedDrive && !RightHandTipMenuInputs.start_2)
            {
                RightHandTipMenuInputs.finishedDrive = true;
                endPoint.SetActive(true);
                Campos.SetActive(true);

                Vector3 endPos = Camera.main.transform.position;
                endPos.y -= (((Slider.Bodyheight / 100.0f)) - 0.4f);
                endPoint.transform.position = endPos;
            }
        }







        //Akutell gefahrende Route zurücksetzen
        if ((RightHandTipMenuInputs.restartGame || Input.GetKeyDown(KeyCode.N)) && !StartPosition.startCollide)
        {

            if (RightHandTipMenuInputs.startGame && !RightHandTipMenuInputs.start_2 && !RightHandTipMenuInputs.finishedDrive2)
            {
                //Wegpunkte, Start- und Endpunkt löschen
                foreach (GameObject o in obj)
                {
                    DestroyImmediate(o);
                }
                obj.Clear();


                //Startpunkt, Endpunkt, Kollisionsbox deaktivieren
                startPoint.SetActive(false);
                endPoint.SetActive(false);
                Campos.SetActive(false);


                //Appstart zurücksetzen
                RightHandTipMenuInputs.startGame = false;
                RightHandTipMenuInputs.finishedDrive = false;


                //Fahrzeit zurücksetzen
                Timer.resetTimer = true;
                Timer.timeEnded = false;


                //Distanz zurücksetzen
                drivenDistance.distance = 0;
                drivenDistance.distanceTraveled = 0;
                drivenDistance.setDistanceDuringCreateRoute = true;


                //Countdown zurücksetzen
                count = false;
                startTimer = 4.0f;
                RightHandTipMenuInputs.count = false;
                RightHandTipMenuInputs.startTimer = 4.0f;
                countdownText.SetText("");
                Timer.startTimer = 6.0f;
                Timer.startCountdown = false;


                RightHandTipMenuInputs.restartGame = false;

            }






            //Akutell nachgefahrende Strecke zurücksetzen
            if (RightHandTipMenuInputs.startGame && (RightHandTipMenuInputs.start_2 || RightHandTipMenuInputs.finishedDrive2))
            { 
                foreach (GameObject o in obj)
                {
                    o.GetComponent<Renderer>().material.color = new Color(0.9f, 0.9f, 1.0f, 0.8f); //Alle wegpunkte wieder weiß machen
                }


                //Nachfahren Ergebnisse zurücksetzen
                RightHandTipMenuInputs.showResultText = false;
                i = 0;
                wrongWaypoints = 0;
                RightHandTipMenuInputs.calculateMistakes = false;
                accurate = 0;


                //Strecke nachfahren beenden
                RightHandTipMenuInputs.start_2 = false;
                RightHandTipMenuInputs.finishedDrive2 = false;
                RightHandTipMenuInputs.close = false;
                RightHandTipMenuInputs.clickOnEvaluation = false;



                //Fahrzeit zurücksetzen
                Timer.tie = 0;
                Timer.timeIsOver = false;


                //Menus deaktivieren
                evaluation.SetActive(false);
                finishMenu.SetActive(false);
                RightHandTipMenuInputs.evaluationState = false;


                //Versuche zurücksetzen
                RightHandTipMenuInputs.tries = 0;


                //Map-Zoom zurücksetzen
                cam.orthographicSize = 3;


                //Distanz zurücksetzen
                drivenDistance.resetDistanceDuringRedrive();
                drivenDistance.distanceDuringRedrive = 0;
                drivenDistance.distance = 0;
                drivenDistance.distanceTraveled = 0;


                //Countdown zurücksetzen
                count = false;
                startTimer = 4.0f;
                RightHandTipMenuInputs.count = false;
                RightHandTipMenuInputs.startTimer = 4.0f;
                countdownText.SetText("");
                Timer.startTimer = 6.0f;
                Timer.startCountdown = false;


                RightHandTipMenuInputs.restartGame = false;

            }
        }







        //Einstellungsfenster öffnen
        if (settings != null && settingsCloseBTN != null && Input.GetKeyDown(KeyCode.E))
        {
            settings.SetActive(true);
            settingsCloseBTN.SetActive(true);


            //Fenster 1.3 Meter vor dem Benutzer setzen und etwas nach unten schieben
            Vector3 f = Camera.main.transform.forward;
            f *= 1.3f;
            Vector3 pos = Camera.main.transform.position;
            pos.y -= 0.3f;
            pos.x += f.x;
            pos.z += f.z;


            //Fenster nur um die Y-Achse drehen, damit sich das Fenster zum Benutzer dreht beim öffnen
            Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            //eulerAngles.y = 0;
            eulerAngles.z = 0;

            //Fester öffnen anhand der Position und Roatation 
            settings.transform.eulerAngles = eulerAngles;
            settings.transform.position = pos;

            settingsCloseBTN.SetActive(false);
        }






        //Werte für das Evaluations-Fenster berechnen (genauigkeit)
        if (RightHandTipMenuInputs.finishedDrive2 && RightHandTipMenuInputs.calculateMistakes)
        {
            countWrongWaypoints();
            calcAccurate();
        }

             


        //Wegpunkte nur erstellen, wenn fahrt gestartet und nicht beendet
        if (!RightHandTipMenuInputs.startGame || RightHandTipMenuInputs.finishedDrive)
        {
            return;
        }
        
       

        //Richtige Position der Wegpunkte berechnen
        calcWaypointPosition();



        if (checkIfWaypointCanBeCreated())  // Kann der Wegpunkt erstellt werden?
        {   
            createWaypoint(wayPointPosition);   //Wegpunkt erstellen
        }

    }








    //Richtige Position der Wegpunkte berechnen
    private void calcWaypointPosition()
    {
        wayPointPosition = Camera.main.transform.position;
        wayPointPosition.y -= (((Slider.Bodyheight / 100.0f)-0.4f));
    }





    // Kann der Wegpunkt erstellt werden?
    private bool checkIfWaypointCanBeCreated()
    {

        // Damit Wegpubkte nicht ineinander sein können. Verhindert das erstellen neuer Wegpunkte, wenn man sich nicht bewegt
       //if ((!Physics.CheckSphere(wayPointPosition, SizeRadius) &&

       if(Camera.main.velocity.magnitude > 0.6f)
        {
            Spawntimer -= Time.deltaTime;   //Zeitabstand zum erstellen der Wegpunkte beachten

            if (Spawntimer <= 0.0f)
            {
                return true;
            }
        }
        return false;
    }





    //Farbe der Wegpunkte festlegen
    private void setColor(GameObject currentLine)
    {
        currentLine.GetComponent<Renderer>().material.color = new Color(0.9f, 0.9f, 1.0f, 0.8f);
    }






    //Wegpunkt immer an der Aktuellen Position der Kamera erstellen
    public void createWaypoint(Vector3 camPos)
    {

        GameObject waypoint = Instantiate(line, camPos, Quaternion.identity); //Wegpunkt erstellen
        waypoint.transform.localScale = Vector3.one * SizeRadius;              // Größe ändern

        setColor(waypoint);             //Farbe setzen
        obj.Add(waypoint);              //Wegpunkt in Liste hinzufügen

        Spawntimer = TimeSlider.time;   //Spawntimer nach ertellen des Wegpunktes zurücksetzen

    }
}
