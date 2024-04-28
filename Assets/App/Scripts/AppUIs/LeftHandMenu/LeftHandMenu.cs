using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandMenu : MonoBehaviour
{

    public static bool resetApp = false;

    public GameObject selectionMap;
    public GameObject openMenu;
    public GameObject openMenu2;
    public GameObject closeMenu;
    public GameObject openMap;




    //Wenn man auf App Zurücksetzen klickt, soll die App zurückgesetz werden :-O
    public void onClickResetApp()
    {
        resetApp = true;
    }






    //Das Einstellungsfenster öffnen und and der richtigen Position (vor dem Benutzer) anzeigen 
    public void Settings()
    {
        if (openMenu != null && openMenu2 != null)
        {

            openMenu.SetActive(true);
            openMenu2.SetActive(true);

            //Das Fenster 1.3 Meter von dem Benutzer setzen
            Vector3 f = Camera.main.transform.forward;
            f *= 1.3f;

            Vector3 pos = Camera.main.transform.position;
            pos.y -= 0.3f;
            pos.x += f.x;
            pos.z += f.z;


            Vector3 eulerAngles = Camera.main.transform.rotation.eulerAngles;
            eulerAngles.x = 0;
            //eulerAngles.y = 0;
            //eulerAngles.z = 0;

            openMenu.transform.eulerAngles = eulerAngles;

            openMenu.transform.position = pos;
            closeMenu.SetActive(false);
        }
    }



    //Um die Minimap ein- und aus zu schalten
    public void miniMap()
    {
        RenderMap.renderMap = !RenderMap.renderMap;
        openMap.SetActive(RenderMap.renderMap);
        selectionMap.GetComponent<Renderer>().enabled = RenderMap.renderMap;
    }
}
