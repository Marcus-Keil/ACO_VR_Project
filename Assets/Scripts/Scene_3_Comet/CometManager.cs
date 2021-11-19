using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName ="NewButtonHandler")]
public class CometManager : MonoBehaviour
{
    public List<Attractor_Grav> ActiveComets;
    public List<Attractor_Grav> AllAttractors;
    private int MaximumComets = 10;
    public GameObject CometPrefab;
    public GameObject CometCollect;

    private bool CometDeletePressed = false;
    private bool CometSpawnPressed = false;


    private void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        Debug.Log(inputDevices);
        if (inputDevices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool DelPressed))
        {
            if (CometDeletePressed != DelPressed)
            {
                CometDeletePressed = DelPressed;
                if (CometDeletePressed)
                {
                    DestroyComet();
                }
            }
        }
        if (inputDevices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool SpawnPressed))
        {
            if (CometSpawnPressed != SpawnPressed)
            {
                CometSpawnPressed = SpawnPressed;
                if (CometSpawnPressed)
                {
                    SpawnComet();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Attractor_Grav obj in AllAttractors)
        {
            if (obj == null)
            {
                AllAttractors.Remove(obj);
                break;
            }
            else
            {
                obj.Attract();
            }
        }
    }

    public void SpawnComet()
    {
        if (ActiveComets.Count <= MaximumComets)
        {
            GameObject C = Instantiate<GameObject>(CometPrefab, Vector3.zero, Quaternion.identity);
            ActiveComets.Add(C.GetComponent<Attractor_Grav>());
            C.transform.position = CometCollect.transform.position;
            C.transform.parent = CometCollect.transform;
        }
    }

    public void DestroyComet()
    {
        foreach(Attractor_Grav Com in ActiveComets)
        {
            Destroy(Com.gameObject);
            ActiveComets.Remove(Com);
            break;
        }
    }
}
