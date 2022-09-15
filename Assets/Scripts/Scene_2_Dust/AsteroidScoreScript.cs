using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AsteroidScoreScript : MonoBehaviour
{
    public GameObject PlanetoidCollection;
    private GameObject MaxPlanet = null;

    public GameObject TimerTxt;
    public TextMeshProUGUI Score;

    // Start is called before the first frame update
    void Start()
    {
        TimerTxt = GameObject.Find("TimerText");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in PlanetoidCollection.transform)
        {
            if (MaxPlanet == null)
            {
                MaxPlanet = child.gameObject;
            }
            else if (MaxPlanet.GetComponent<Rigidbody>().mass < child.GetComponent<Rigidbody>().mass)
            {
                MaxPlanet = child.gameObject;
                float mass = MaxPlanet.GetComponent<Rigidbody>().mass * 10;
                Score.text = string.Format("{0:0.00}", mass);
            }
        }
    }
}
