using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Attractor_Grav : MonoBehaviour
{
    public Vector3 Kick = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 Rotation = new Vector3(0.0f, 0.0f, 0.0f);
    public GameObject Poof;
    private CometManager CM;
    public GameManager GM;
    private int MaxDistance = 25;
    private int MyIndex;
    public bool Active;
    public bool IsComet;

    const float G = 667.4f;

    public Rigidbody rb;


    private void Start()
    {
        rb.AddForce(Kick);
        rb.AddTorque(Rotation);
        GM = FindObjectOfType<GameManager>();
        CM = FindObjectOfType<CometManager>();
        if (IsComet)
        {
            CM.ActiveComets.Add(this);
            MyIndex = CM.ActiveComets.IndexOf(this);
        }
            
        else
            CM.GravAttractors.Add(this);
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
                if(attractor == null)
                {
                    CM.AllAttractors.Remove(attractor);
                    break;
                }
                else if (attractor != this && attractor.Active)
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

    private void OnTriggerEnter(Collider other)
    {
        if (IsComet && other.gameObject.name == "Earth")
        {
            GM.AnimateEarth(MyIndex, this);
        }
        else if (IsComet && other.gameObject.name == "Sun")
        {
            CreatePoof();
            CM.DestroyComet(MyIndex);
        }
    }

    private void CheckDeath()
    {
        if (Mathf.Abs(gameObject.transform.position.magnitude) >= MaxDistance)
        {
            CM.DestroyComet(MyIndex);
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

    public void CreatePoof()
    {
        GameObject C = Instantiate<GameObject>(Poof, this.transform.position, Quaternion.identity);
        StartCoroutine(ExampleCoroutine(C));
    }

    IEnumerator ExampleCoroutine(GameObject poof)
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(poof);
    }

}
