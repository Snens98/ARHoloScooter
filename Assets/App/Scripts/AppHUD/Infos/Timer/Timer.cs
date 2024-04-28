using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float time = 0;
    public static bool timeEnded = false;
    public static int maxTime = 10;
    public static bool resetTimer = false;
    public TextMeshPro text;

    public AudioSource audioSource;
    public AudioClip clip;
    public GameObject endPosition;
    public GameObject velocity;
    public GameObject camPosition;
    public GameObject startPosition;

    public static float t;






    //Sound abspielen, wenn nur noch 5 Sekunden zu fahren ist
    private void playSound(int secondBevorEnd, AudioClip src, float t, float d, bool b)
    {
        if (!audioSource.isPlaying && t > (maxTime*d) - secondBevorEnd && !(t >= (maxTime) *d) && b)
        {
            startCountdown = true;
            audioSource.PlayOneShot(src);
        }
    }






    //Endpunkt setzen, wenn die Zeit vorbei ist
    private void placeEndPositionWhenTimerEnded()
    {

        if (t >= maxTime && !timeEnded)
        {
            RightHandTipMenuInputs.finishedDrive = true;

            Vector3 endPos = Camera.main.transform.position;
            endPos.y -= (((Slider.Bodyheight / 100.0f))-0.4f);

            endPosition.transform.position = endPos;

            endPosition.SetActive(true);
            camPosition.SetActive(true);
            startPosition.SetActive(true);
            timeEnded = true;
        }
    }




    //Timer auf den Wert in den Einstellungen festlegen
    void Start()
    {
        t = time;
    }





    // Den Countdown anzeigen, wenn nur noch 5 Sekunden übrig sind
    public static float startTimer = 6.0f;
    public TextMeshPro CoutdownText;
    public static bool startCountdown = false;
    public static bool startCountdownRetry = false;

    public void startCoutdown()
    {

        if (CoutdownText != null)
        {

            if (startTimer > -2.0f)
            {
                startTimer -= Time.deltaTime;
            }

            CoutdownText.SetText((int)startTimer + "");

            if (startTimer <= 1.0f && startTimer >= -1.0f)
            {
                CoutdownText.SetText("ENDE!");
            }

            if (!RightHandTipMenuInputs.start_2 && RightHandTipMenuInputs.finishedDrive)
            {

                CoutdownText.SetText("");
                startCountdown = false;
                startCountdownRetry = false;
                startTimer = 6.0f;

            }
        }
    }







    //Countdown starten
    private void countdown()
    {
        if (startCountdown || startCountdownRetry)
        {
            startCoutdown();
        
        }
    }






    //Zeit zurücksetzen
    private void resetTime()
    {
        if (resetTimer)
        {
            t = 0;
            tie = 0;
            time = 0;
            resetTimer = false;
        }
    }






    //Zeit anzeigen lassen, während man die Route fährt
    public static bool setDistanceDuringCreateRoute = true;
    public static bool setDistanceDuringRedrive = false;

    private void setTimerDuringCreateRoute()
    {

        if (RightHandTipMenuInputs.startGame)
        {
            if (!RightHandTipMenuInputs.finishedDrive)
                t += Time.deltaTime;
        }

        if (!RightHandTipMenuInputs.finishedDrive)
        {
            text.SetText("T: " + (int)t + "");
        }


        if (RightHandTipMenuInputs.restartGame)
        {
            t = time;
        }
    }






    void Update()
    {
        if ((EndPosition.endPosCollide && RightHandTipMenuInputs.start_2) || RightHandTipMenuInputs.finishedDrive2)
        {
           //return;
        }

        //Countdown aufrufen
        countdown();

        //Zeit zurücksetzen
        timerResetIfNeeded();

        //Sound abspielen
        playSound(6, clip, t, 1, !RightHandTipMenuInputs.finishedDrive);

        //Zeit anzeigen, bei Route fahren und Route nachfahren
        setTimerDuringCreateRoute();
        setTimerDuringRedrive();
    }





    private void timerResetIfNeeded()
    {
        resetTime();
        placeEndPositionWhenTimerEnded();
    }



    //Zeit und auch die Genauigkeit anzeigen, nachdem man die Route nachgafahren ist
    public static float tie = 0.0f;
    public static bool timeIsOver = false;
    private void setTimerDuringRedrive()
    {

        if (tie > (maxTime*2f) - 6 && !(tie >= maxTime*2f))
        {
            playSound(6, clip, tie, 2f, true);
            startCountdownRetry = true;
        }

        if (RightHandTipMenuInputs.finishedDrive && RightHandTipMenuInputs.start_2 && !RightHandTipMenuInputs.finishedDrive2)
        {
            if(!RightHandTipMenuInputs.finishedDrive2)
                tie += Time.deltaTime;

            if (RightHandTipMenuInputs.start_2)
            {
                text.SetText("T: " + (int)tie +"");
            }
        }

        if ((EndPosition.endPosCollide && RightHandTipMenuInputs.start_2) || RightHandTipMenuInputs.finishedDrive2)
        {
            text.SetText("T: " +(int)tie + "\n" + Mathf.Round((createWaypoints.accurate) * 10f) / 10f + " %");
        }

        finishDriveWhenTimeIsOver();
    }





    //Zeit zurücksetzen, beim nachfahren
    private void finishDriveWhenTimeIsOver()
    {
        if (tie >= maxTime * 2f)
        {
            RightHandTipMenuInputs.finishedDrive2 = true;

            if (!timeIsOver)
            {
                timeIsOver = true;
            }
        }
    }
}
