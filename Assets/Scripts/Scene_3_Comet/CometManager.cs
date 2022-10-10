using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

[CreateAssetMenu(fileName ="NewButtonHandler")]
public class CometManager : MonoBehaviour
{
    public List<Attractor_Grav> ActiveComets;
    public List<Attractor_Grav> AllAttractors;
    public List<Attractor_Grav> GravAttractors;
    private int MaximumComets = 10;
    public Vector3 ComLocation = new Vector3(0.0f, 1.2f, -13.0f);
    public GameObject CometPrefab;
    public GameObject CometCollect;

    private InputDevice _RightController;
    private InputDevice _LeftController;
    private List<InputDevice> inputDevices = new List<InputDevice>();

    private bool CometSpawnPressed = false;

    private void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }
        _RightController = inputDevices[0];

        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, inputDevices);

        if (inputDevices.Count == 0)
        {
            return;
        }
        _LeftController = inputDevices[0];
    }

    private void Update()
    {
        if (!_RightController.isValid || !_LeftController.isValid)
        {
            TryInitialize();
        }
        else if (StoredKnowledge.StartTutorial_3 || StoredKnowledge.Start_Game_3)
        {

            if (_LeftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool SpawnPressed) && SpawnPressed)
            {
                if (!CometSpawnPressed)
                {
                    CometSpawnPressed = true;
                    if (!StoredKnowledge.Start_Game_3)
                    {
                        StoredKnowledge.Start_Game_3 = true;
                    }

                    if (ActiveComets.Count < MaximumComets)
                        SpawnComet();
                    else
                    {
                        DestroyComet();
                        SpawnComet();
                    }
                }
            }
            else if (_LeftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool SpawnNotPressed) && !SpawnNotPressed)
            {
                CometSpawnPressed = false;
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Attractor_Grav obj in GravAttractors)
        {
            obj.Attract();
        }
    }

    public void SpawnComet()
    {
        if (ActiveComets.Count < MaximumComets)
        {
            GameObject C = Instantiate<GameObject>(CometPrefab, ComLocation, Quaternion.identity);
            C.transform.position = CometCollect.transform.position;
            C.transform.parent = CometCollect.transform;
        }
    }

    public void DestroyComet(int index_Comet = 0)
    {
        Attractor_Grav Com = ActiveComets[index_Comet];
        Debug.Log(Com);
        if (Com != null)
        {
            Destroy(Com.gameObject);
            AllAttractors.Remove(Com);
            ActiveComets.Remove(Com);
        }
    }
}
