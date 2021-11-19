using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Attractor_Grav : MonoBehaviour
{
    public Vector3 Kick = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 Rotation = new Vector3(0.0f, 0.0f, 0.0f);
    private CometManager CM;
    private int MaxDistance = 25;
    public bool Active;
    public bool IsComet;

    const float G = 667.4f;

    public List<Attractor_Grav> Attractors;

    public Rigidbody rb;


    private void Start()
    {
        rb.AddForce(Kick);
        rb.AddTorque(Rotation);
        CM = FindObjectOfType<CometManager>();
        if (IsComet)
            CM.ActiveComets.Add(this);
    }
    private void Update()
    {
        CheckDeath();
    }
    void FixedUpdate()
    {
        if (Active)
        {
            foreach (Attractor_Grav attractor in Attractors)
            {
                if (attractor != this && attractor.Active)
                    Attract(attractor);
            }
        }
    }

    void OnEnable()
    {
        if (Attractors == null)
            Attractors = new List<Attractor_Grav>();
        Attractor_Grav[] TempObs = FindObjectsOfType<Attractor_Grav>();
        foreach (Attractor_Grav att in TempObs)
        {
            if (att != this)
                Attractors.Add(att);
        }
    }

    void Attract(Attractor_Grav objToAttract)
    {
        if (objToAttract == null)
        {
            Attractors.Remove(objToAttract);
            return;
        }
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0f)
            return;

        float forceMagnitude = (G * (rb.mass * rbToAttract.mass)) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }

    private void CheckDeath()
    {
        if (Mathf.Abs(gameObject.transform.position.magnitude) >= MaxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Activate()
    {
        Active = true;
    }

    public void Deactivate()
    {
        Active = false;
    }

}
