using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpdateTimeValue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void updateValue()
    {
        createWaypoints.Spawntimer = TimeSlider.time;
    }
}
