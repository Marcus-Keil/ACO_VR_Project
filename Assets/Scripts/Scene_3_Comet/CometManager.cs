using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometManager : MonoBehaviour
{
    public List<Attractor_Grav> ActiveComets;

    public GameObject CometPrefab;

    private int MaximumComets = 10;

    public void SpawnComet()
    {
        if (ActiveComets.Count <= MaximumComets)
        {
            GameObject C = Instantiate<GameObject>(CometPrefab, Vector3.zero, Quaternion.identity);
            ActiveComets.Add(C.GetComponent<Attractor_Grav>());
        }
    }
}
