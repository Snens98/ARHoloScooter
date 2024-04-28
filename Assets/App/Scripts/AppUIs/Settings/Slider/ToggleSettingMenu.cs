using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ToggleSettingMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject closeBtn;

    public bool active = true;

    void Start()
    {
        //werte in cm und Sekunde
        DistanceSlider.wayPointDistance = 100f;
        Slider.Bodyheight = 170f;
        RenderSlider.renderDistance = 0f;
        TimeSlider.time = 0.0f;
        DrivingTimeSlider.drivingTime = 15;
        Timer.maxTime = (int)DrivingTimeSlider.drivingTime;

        switchButtonInputs.showDirectionArrow = true;
        switchButtonInputs.showWayPointsOnMap = true;
        switchButtonInputs.showWayPointsState = true;

        settings.SetActive(active);
        settings.transform.position = new Vector3(0, 10000, 0);
    }


    public void onClickClose()
    {
        settings.SetActive(false);
    }


    public GameObject a;
    public GameObject b;
    public GameObject c;

    void Update()
    {
        if (a != null && b != null && c != null && RightHandTipMenuInputs.startGame)
        {

            a.GetComponent<PinchSlider>().enabled = false;
            b.GetComponent<PinchSlider>().enabled = false;
            c.GetComponent<PinchSlider>().enabled = false;
        }
        else
        {
            a.GetComponent<PinchSlider>().enabled = true;
            b.GetComponent<PinchSlider>().enabled = true;
            c.GetComponent<PinchSlider>().enabled = true;
        }
    }
}
