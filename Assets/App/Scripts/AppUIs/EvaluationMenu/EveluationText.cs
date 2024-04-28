using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EveluationText : MonoBehaviour
{

    public TextMeshPro text;
    public TextMeshPro text2;
    public TextMeshPro time;


    // Hier ist der Text der im Evaluations-Fenster angezeit werden soll

    float s = 0;
    void Update()
    {
        s = drivenDistance.distanceTraveled;

        if (text != null)
        {

            text.SetText("Max. Zeitvorgabe: " +
                "\nDistanzvorgabe: " +
                "\nZeitabstand " +

                "\n\n\nDeine Zeit: " +
                " \nDeine Distanz: " +
                "\nGenauigkeit: " +
                "\nVersuche: " +
                "\nAusblend-Radius: " +

                "\n\nRichtungspfeil: " +
                "\nMinimap: " +
                "\nWegpunkte: " +
                "\nWegpunkte auf Map: ");
        }


        string showDirectionArrow = switchButtonInputs.showDirectionArrow ? "An" : "Aus";
        string renderMap = RenderMap.renderMap ? "An" : "Aus";
        string showWayPointsState = switchButtonInputs.showWayPointsState ? "An" : "Aus";
        string showWayPointsOnMap = switchButtonInputs.showWayPointsOnMap ? "An" : "Aus";



        text2.SetText(((int)Timer.maxTime * 2.0f) + " s" +
            "\n" + (Mathf.Round(drivenDistance.distanceDuringCreateRoute * 100f) / 100f) + " m" +
            "\n" + TimeSlider.time + " s" +

            "\n\n\n" + (int)Timer.tie + "s" +
            " \n" + (Mathf.Round(s * 100f) / 100f) + " m/km" +
            "\n" + (Mathf.Round(createWaypoints.accurate * 100f) / 100f) + " %" +
            "\n" + RightHandTipMenuInputs.tries + "" +
            "\n" + RenderSlider.renderDistance + " cm" +

            "\n\n" + showDirectionArrow +
            "\n" + renderMap +
            "\n" + showWayPointsState +
            "\n" + showWayPointsOnMap);


        time.SetText("Datum:                           " + GetCurrentDayName() +
                  "\nUhrzeit:                          " + GetCurrentTime());
    }




    //Datum
    public static string GetCurrentDayName()
    {
        return DateTime.Now.ToString("dd.MM.yy");
    }


    //Uhrzeit
    public static string GetCurrentTime()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }
}
