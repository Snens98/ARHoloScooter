using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StartPosition : MonoBehaviour
{

    public Renderer renderer;
    public GameObject cam;



    //Start-EIgenschaffen für den Startpunkt festlegen
    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>();
        renderer.material.color = new Color(0.35f, 1.0f, 0.0f, 0.6f); //Grün

        Vector3 startPos = Camera.main.transform.position;
        startPos.y -= (((Slider.Bodyheight / 100.0f)));
        this.gameObject.transform.position = startPos;
        renderer.enabled = true;
        this.gameObject.SetActive(false);

    }




    public static bool startCollide = false;

    //Wenn der Benutzer den Startpunkt berührt
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals(cam.gameObject.tag))
        {

            if (RightHandTipMenuInputs.finishedDrive)
            {
                renderer.enabled = false;
                startCollide = true;

                if (!RightHandTipMenuInputs.start_2)
                {
                    MenuToggleRedrive.enable = true;
                }
            }
        }
    }



    //Wenn der Benutzer den Startpunkt verlässt
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(cam.gameObject.tag))
        {
            if (RightHandTipMenuInputs.finishedDrive)
            {
                renderer.enabled = true;
                startCollide = false;
                MenuToggleRedrive.enable = false;

            }
        }
    }
}
