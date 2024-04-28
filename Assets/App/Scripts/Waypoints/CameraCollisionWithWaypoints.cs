using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionWithWaypoints : MonoBehaviour
{


    public Renderer renderer;
    public GameObject cam;


    void Start()
    {
        renderer = this.gameObject.GetComponent<Renderer>(); // Renderer für die Wegpunkte, um die farbe zu ändern
    }



    //Was passieren soll, wenn der Benutzer durch ein Wegpunkt fährt
    private void OnTriggerEnter(Collider other)
    {
        if (RightHandTipMenuInputs.finishedDrive && RightHandTipMenuInputs.start_2)
        {
            Color c = renderer.material.color;

            if (c.g != 1.0f)        //Für alle Wegpunkte die nicht Grün sind und der benutzer durchfährt
            {
                playSound(clip);    // Wird ein Ton abgespielt
            }


            //Wenn man durch einen Wegpunkt fährt, wird er grün
            if (other.gameObject.tag.Equals(cam.gameObject.tag))
            {
                renderer.material.color = new Color(0.35f, 1.0f, 0.0f, 0.8f); //Grün
            }
        }
    }




    //Wenn man durch einen Wegpunkt fährt, wird ein Sound wiedergegeben
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
