using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VR_Menu : MonoBehaviour
{
    private GameObject menu_cube;

    private InputDevice _RightController;
    private InputDevice _LeftController;
    private List<InputDevice> inputDevices = new List<InputDevice>();
    bool MenuButtonPressed = false;

    void Start()
    {
        menu_cube = transform.Find("Cube").gameObject;
        TryInitialize();
    }

    void TryInitialize()
    {
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        if (inputDevices.Count == 0)
        {
            return;
        }
        _RightController = inputDevices[0];

        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        if (inputDevices.Count == 0)
        {
            return;
        }
        _LeftController = inputDevices[0];
    }

    void Update()
    {
        if (!_RightController.isValid || !_LeftController.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (_LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool MenuPressed) && MenuPressed)
            {
                if (!MenuButtonPressed)
                {
                    MenuButtonPressed = true;
                    ToggleMenu();
                }
            }
            else if (_LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool MenuNotPressed) && !MenuNotPressed)
            {
                MenuButtonPressed = false;
            }
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
