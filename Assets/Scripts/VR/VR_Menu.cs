using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Menu : MonoBehaviour
{
    private GameObject menu_cube;

    private List<UnityEngine.XR.InputDevice> inputDevices;
    bool MenuButtonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        menu_cube = transform.Find("Cube").gameObject;
        inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
    }

    void Update()
    {
        if (inputDevices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool MenuPressed) && MenuPressed)
        {
            if (!MenuButtonPressed)
            {
                MenuButtonPressed = true;
                ToggleMenu();
            }
        }
        else if (inputDevices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool MenuNotPressed) && !MenuNotPressed)
        {
            MenuButtonPressed = false;
        }


    }

    public void ToggleMenu()
    {
        if (menu_cube.activeInHierarchy)
        {
            menu_cube.SetActive(false);
        }
        else if (!menu_cube.activeInHierarchy)
        {
            menu_cube.SetActive(true);
        }
    }
}
