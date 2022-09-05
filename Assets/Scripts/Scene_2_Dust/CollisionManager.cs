using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    public List<Attractor> ObjectsToAttract;
    public List<Attractor> ObjectsToCombine;

    private GameObject planetoidParentObject;
    public GameObject PlanetoidPrefab;
    private GameObject dustParentObject;
    public GameObject DustPrefab;

    public SecondTimerScript TimerTxt;
    public GameObject G_Center;
    public GameObject S_Center;
    private float ForceMagnitude = 0.005f;
    private float DustSpawnRadius = 0.5f;
    private float MoveRadius = 2.5f;
    private int MaximumDust = 1000;
    private int itteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        planetoidParentObject = GameObject.Find("PlanetoidGroup");
        dustParentObject = GameObject.Find("DustGroup");
        G_Center = GameObject.Find("G_Center");
        S_Center = GameObject.Find("S_Center");
        InitiateLife();
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
        if (TimerTxt.TimerOn)
        {
            foreach (Attractor Obj in ObjectsToCombine)
            {
                if (Obj != null)
                {
                    if (Obj.Bonder.gameObject.GetComponent<Attractor>() != null)
                    {
                        if (Obj.Bonder.gameObject.GetComponent<Attractor>().Bonder == Obj.gameObject && Obj.Bonder.gameObject.GetComponent<Attractor>().Bondable)
                        {
                            ObjectsToAttract.Remove(Obj);
                            ObjectsToAttract.Remove(Obj.Bonder.gameObject.GetComponent<Attractor>());
                            ObjectsToCombine.Remove(Obj);
                            ObjectsToCombine.Remove(Obj.Bonder.gameObject.GetComponent<Attractor>());
                            Combine(Obj.gameObject, Obj.Bonder.gameObject);
                            break;
                        }
                    }
                    else
                    {
                        ObjectsToCombine.Remove(Obj);
                        ObjectsToCombine.Remove(Obj.Bonder.gameObject.GetComponent<Attractor>());
                        break;
                    }
                }
                else
                {
                    ObjectsToCombine.Remove(Obj);
                    ObjectsToCombine.Remove(Obj.Bonder.gameObject.GetComponent<Attractor>());
                    break;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Attractor Obj in ObjectsToAttract)
        {
            if (Obj == null)
            {
                ObjectsToAttract.Remove(Obj);
                break;
            }
            else
                MoveToPoint(G_Center, Obj.gameObject);
        }
    }

    private void MoveToPoint(GameObject C, GameObject Mov)
    {
        
        Rigidbody MovRB = Mov.GetComponent<Rigidbody>();
        Vector3 direction = C.transform.position - Mov.transform.position;
        if (direction.magnitude < MoveRadius && MovRB.mass >= 0.2f)
        {
            MovRB.AddForce(direction.normalized * ForceMagnitude);
        }
        else if (MovRB.mass >= 0.2f)
        {
            MovRB.AddForce(direction * ForceMagnitude * 10);
        }
        
    }

    public void Combine(GameObject First, GameObject Second)
    {
        Vector3 FirstPosition = First.gameObject.transform.localPosition;
        Vector3 FirstScale = First.gameObject.transform.localScale;
        float FirstMass = First.gameObject.GetComponent<Rigidbody>().mass;
        

        Vector3 SecondPosition = Second.gameObject.transform.localPosition;
        Vector3 SecondScale = Second.gameObject.transform.localScale;
        float SecondMass = Second.gameObject.GetComponent<Rigidbody>().mass;

        Vector3 Velocity = (First.GetComponent<Rigidbody>().velocity * FirstMass + Second.GetComponent<Rigidbody>().velocity * SecondMass) / (FirstMass + SecondMass);
        Vector3 AngVelocity = (First.GetComponent<Rigidbody>().angularVelocity * FirstMass + Second.GetComponent<Rigidbody>().angularVelocity * SecondMass) / (FirstMass + SecondMass);
        Vector3 Scale = new Vector3(Mathf.Max(FirstScale.x, SecondScale.x), Mathf.Max(FirstScale.y, SecondScale.y), Mathf.Max(FirstScale.z, SecondScale.z));
        Quaternion Rot;
        if (FirstMass >= SecondMass)
            Rot = First.gameObject.transform.rotation;
        else
            Rot = Second.gameObject.transform.rotation;

        Destroy(First.gameObject);
        Destroy(Second.gameObject);

        Vector3 MidPosition = (FirstPosition * FirstScale.magnitude + SecondPosition * SecondScale.magnitude) /
                (FirstScale.magnitude + SecondScale.magnitude);

        GameObject p = Instantiate<GameObject>(PlanetoidPrefab, MidPosition, Rot);

        
        switch (GetMinIndex(Scale))
        {
            case 0:
                Scale.x += (Mathf.Min(FirstScale.x, SecondScale.x) / (2));
                break;
            case 1:
                Scale.y += (Mathf.Min(FirstScale.y, SecondScale.y) / (2));
                break;
            case 2:
                Scale.z += (Mathf.Min(FirstScale.z, SecondScale.z) / (2));
                break;
        }
        p.transform.localScale = Scale;
        p.transform.parent = planetoidParentObject.transform;
        
        p.GetComponent<Rigidbody>().mass = FirstMass + SecondMass;
        p.GetComponent<Rigidbody>().velocity = Velocity;
        p.GetComponent<Rigidbody>().angularVelocity = AngVelocity;
    }

    private void CheckLife()
    {
        if (ObjectsToAttract.Count < MaximumDust && itteration < UnityEngine.Random.Range(10, 51))
            itteration += 1;
        else
        {
            itteration = 0;
            Vector3 SpawnLocation = UnityEngine.Random.onUnitSphere * DustSpawnRadius + S_Center.gameObject.transform.position;
            GameObject d = Instantiate(DustPrefab, SpawnLocation, Quaternion.identity);
            d.transform.parent = dustParentObject.transform;
        }
    }

    private void InitiateLife()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 Spawn = UnityEngine.Random.insideUnitSphere * DustSpawnRadius;
            Spawn.y += DustSpawnRadius;
            Spawn += S_Center.transform.position;
            GameObject d = Instantiate(DustPrefab, Spawn, Quaternion.identity);
            d.transform.parent = dustParentObject.transform;
        }
    }

    private int GetMinIndex(Vector3 vec)
    {
        int index = 0;
        if (vec.x <= vec.y && vec.x <= vec.z)
            return index;
        else if (vec.y < vec.x && vec.y <= vec.z)
        {
            index = 1;
            return index;
        }
        else
        {
            index = 2;
            return index;
        }
    }
}

