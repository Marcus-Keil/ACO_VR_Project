using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CreateAssetMenu(fileName ="NewButtonHandler")]
public class CometManager : MonoBehaviour
{
    public List<Attractor_Grav> ActiveComets;
    public List<Attractor_Grav> AllAttractors;
    public List<Attractor_Grav> GravAttractors;
    private int MaximumComets = 10;
    public GameObject CometPrefab;
    public GameObject CometCollect;

    private bool CometDeletePressed = false;
    private bool CometSpawnPressed = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        if (inputDevices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool SpawnPressed))
        {
            if (CometSpawnPressed != SpawnPressed)
            {
                CometSpawnPressed = SpawnPressed;
                if (CometSpawnPressed)
                {
                    if (ActiveComets.Count < MaximumComets)
                        SpawnComet();
                    else
                        DestroyComet();
                }
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
            GameObject C = Instantiate<GameObject>(CometPrefab, Vector3.zero, Quaternion.identity);
            C.transform.position = CometCollect.transform.position;
            C.transform.parent = CometCollect.transform;
        }
    }

    public void DestroyComet()
    {
        Attractor_Grav Com = ActiveComets[0];
        if (Com != null)
        {
            Destroy(Com.gameObject);
            AllAttractors.Remove(Com);
            ActiveComets.Remove(Com);
            SpawnComet();
        }
        else
        {
            ActiveComets.Remove(Com);
            AllAttractors.Remove(Com);
        }

    }
}
