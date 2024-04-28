using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPosition : MonoBehaviour
{

    public Renderer renderer;
    public GameObject cam;



    //End-Eigenschafften wir den Startpunkt festlegen
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        renderer.material.color = new Color(1.0f, 0.35f, 0.0f, 0.6f); //rot
        renderer.GetComponent<Renderer>().enabled = true;
        this.gameObject.SetActive(false);
    }






    public static bool endPosCollide = false;

    //Wenn der Benutzer den Endpunkt berührt
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals(cam.gameObject.tag))    //Endpunkt kollision mit Camera (Benutzer)
        {
            if (RightHandTipMenuInputs.finishedDrive)
            {
                endPosCollide = true;

                if (RightHandTipMenuInputs.showResultText)
                {
                    RightHandTipMenuInputs.finishedDrive2 = true;
                }
            }
        }
    }




    //Wenn der Benutzer den Endpunkt verlässt
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(cam.gameObject.tag))
        {
            if (RightHandTipMenuInputs.finishedDrive)
            {
                endPosCollide = false;
            }
        }
    }
}
