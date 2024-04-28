using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionWithWaypoints : MonoBehaviour
{


    public Renderer renderer;
    public GameObject cam;


    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>(); // Renderer f�r die Wegpunkte, um die farbe zu �ndern
    }



    //Was passieren soll, wenn der Benutzer durch ein Wegpunkt f�hrt
    private void OnTriggerEnter(Collider other)
    {
        if (RightHandTipMenuInputs.finishedDrive && RightHandTipMenuInputs.start_2)
        {
            Color c = renderer.material.color;

            if (c.g != 1.0f)        //F�r alle Wegpunkte die nicht Gr�n sind und der benutzer durchf�hrt
            {
                playSound(clip);    // Wird ein Ton abgespielt
            }


            //Wenn man durch einen Wegpunkt f�hrt, wird er gr�n
            if (other.gameObject.tag.Equals(cam.gameObject.tag))
            {
                renderer.material.color = new Color(0.35f, 1.0f, 0.0f, 0.8f); //Gr�n
            }
        }
    }




    //Wenn man durch einen Wegpunkt f�hrt, wird ein Sound wiedergegeben
    public AudioSource audioSource;
    public AudioClip clip;

    private void playSound(AudioClip src)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(src);
        }
    }
}
