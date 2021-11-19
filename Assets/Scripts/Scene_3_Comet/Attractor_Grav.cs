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

    public Rigidbody rb;


    private void Start()
    {
        rb.AddForce(Kick);
        rb.AddTorque(Rotation);
        CM = FindObjectOfType<CometManager>();
        if (IsComet)
            CM.ActiveComets.Add(this);
        CM.AllAttractors.Add(this);
    }
    private void Update()
    {
        CheckDeath();
    }

    public void Attract()
    {
        if (Active)
        {
            foreach (Attractor_Grav attractor in CM.AllAttractors)
            {
                if (attractor != this && attractor.Active)
                {
                    Rigidbody rbToAttract = attractor.rb;

                    Vector3 direction = rb.position - rbToAttract.position;
                    float distance = direction.magnitude;

                    if (distance == 0f)
                        return;

                    float forceMagnitude = (G * (rb.mass * rbToAttract.mass)) / Mathf.Pow(distance, 2);
                    Vector3 force = direction.normalized * forceMagnitude;

                    rbToAttract.AddForce(force);
                }
            }
        }        
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
