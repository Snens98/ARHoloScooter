using UnityEngine;

public class MenuToggleRedrive : MonoBehaviour
{

    public GameObject menu2;
    public GameObject map;
    public static bool enable = false;






    // Der Nachfahr-Button soll angezeigt werden, wenn die Route gefahren wurde und  der benutzer in Endpunkt steht,
    // der benutzer im Startpunkt steht, die Zeit um ist, oder man eine genauigkeit von 100% hat
    private void Update()
    {
        if (Timer.timeIsOver || createWaypoints.accurate >= 100)
        {
            enable = false;
        }

        menu2.SetActive(enable);
    }
}
