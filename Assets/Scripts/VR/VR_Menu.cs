using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class VR_Menu : MonoBehaviour
{
    private GameObject menu_cube;

    private UnityEngine.XR.InputDevice _RightController;
    private UnityEngine.XR.InputDevice _LeftController;
    private List<UnityEngine.XR.InputDevice> inputDevices = new List<UnityEngine.XR.InputDevice>();
    bool MenuButtonPressed = false;
    public float TimeScale = 0.0000001f;
    private float defaultTimeScale;
    private float fixedDeltaTime;

    void Start()
    {
        menu_cube = transform.Find("Cube").gameObject;
        TryInitialize();
        fixedDeltaTime = Time.fixedDeltaTime;
        defaultTimeScale = Time.timeScale;
    }

    void TryInitialize()
    {
        UnityEngine.XR.InputDeviceCharacteristics rightControllerCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }
        _RightController = inputDevices[0];

        UnityEngine.XR.InputDeviceCharacteristics leftControllerCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, inputDevices);

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
            if (!StoredKnowledge.DoneTutorial_1 && StoredKnowledge.StartTutorial_1)
            {
                StartCoroutine(WaitForKeyPress());
            }
            else
            { // if (StoredKnowledge.MenuUnlocked)
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
    }

    public void ToggleMenu()
    {
        if (menu_cube.activeInHierarchy)
        {
            menu_cube.SetActive(false);
            Time.timeScale = defaultTimeScale;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }
        else if (!menu_cube.activeInHierarchy)
        {
            menu_cube.SetActive(true);
            Time.timeScale = TimeScale;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }
    }

    IEnumerator WaitForKeyPress()
    {
        while (!StoredKnowledge.DoneTutorial_1) // essentially a "while true", but with a bool to break out naturally
        {
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    }
}
