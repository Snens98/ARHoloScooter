using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchButtonInputs : MonoBehaviour
{




    //Hier wird geregelt, Was passieren soll, wenn man auf die Buttons im EInstellungsfenster klickt.




    public static bool showWayPointsState = false;
    public static bool showDirectionArrow = true;
    public static bool showWayPointsOnMap = true;

    public GameObject directionArrow;


    void Start()
    {
        //directionArrow.SetActive(showDirectionArrow);
    }



    //Wegpunkte an/aus
    public void onClickshowWayPoints()
    {
       showWayPointsState = !showWayPointsState;
    }


    //Richtungspfeil an/aus
    public void onClickshowDirectionArrow()
    {
        showDirectionArrow = !showDirectionArrow;
        directionArrow.SetActive(showDirectionArrow);   

    }


    //Wegpunkte auf Map an/aus
    public void onClickshowShowPointsOnMap()
    {
        showWayPointsOnMap = !showWayPointsOnMap;
    }
}
