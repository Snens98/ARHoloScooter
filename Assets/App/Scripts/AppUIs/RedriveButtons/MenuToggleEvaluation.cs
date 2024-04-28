using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToggleEvaluation : MonoBehaviour
{
    public GameObject menu2;
    public static bool enable = false;





    // Der Auswertungs-button soll nur angezeigt werden, wenn die Route nachgefahren wurde und der benutzer nicht im Startpunkt steht
    void Update()
    {
        if (RightHandTipMenuInputs.finishedDrive2 && !RightHandTipMenuInputs.close && !StartPosition.startCollide)
        {
            enable = true;
        }
        else
        {
            enable = false;
        }
        menu2.SetActive(enable);
    }
}
