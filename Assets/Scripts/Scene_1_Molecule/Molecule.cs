using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour
{
    public Rigidbody rb;
    private float K_b = 100.0f;
    public float K_theta = 50.0f;
    private float r_0 = 0.2f; // Also used for NonBondedForce
    private float BondDistance; // Distance at which the bonded force should affect the atoms
    private float epsilon = 10.0f;
    private float DistanceModifier = 1.582f;
    public int MaxBonds;
    public List<Molecule> BondedMolecules;
    [SerializeField] private float magnitudeMax;
    [SerializeField] private float vibMag;

    void Start()
    {
        BondDistance = 2 * r_0;
        rb = GetComponent<Rigidbody>();
        if (this.tag == "Hydrogen")
            MaxBonds = 1;
        else if (this.tag == "Oxygen")
            MaxBonds = 2;
    }

    private void Update()
    {
        CheckDeath();
        if (rb.useGravity && BondedMolecules.Count > 0)
        {
            rb.useGravity = false;
        }
    }

    private void FixedUpdate()
    {
        if (BondedMolecules.Count < MaxBonds)
        {
            BondCheck();
        }
        if (BondedMolecules.Count == 2)
        {
            AngleRepulse(BondedMolecules[0], BondedMolecules[1]);
            AngleRepulse(BondedMolecules[1], BondedMolecules[0]);
        }
        if (BondedMolecules.Count < MaxBonds)
        {
            foreach (Molecule molecule in FindObjectsOfType<Molecule>())
            {
                if (molecule.BondedMolecules.Count < molecule.MaxBonds && this != molecule && this.tag != molecule.tag)
                    NonBondAttract(molecule);
                if (BondedMolecules.Count == 0 && molecule.BondedMolecules.Count == 0 && this != molecule && this.tag == molecule.tag)
                    NonBondAttract(molecule);
            }
        }
        if (BondedMolecules.Count > 0)
        {
            foreach (Molecule BondMolecule in BondedMolecules)
            {
                BondAttract(BondMolecule);
            }
        }
        rb.AddForce(new Vector3(vibMag * (Random.value * 2 - 1), vibMag * (Random.value * 2 - 1), vibMag * (Random.value * 2 - 1)));
        if (rb.velocity.magnitude > magnitudeMax)
        {
            rb.AddForce(-rb.velocity.normalized * (rb.velocity.magnitude - magnitudeMax));
        }
        
    }

    void NonBondAttract(Molecule objToAffect)
    {
        float forceMagnitude = -12 * epsilon * (r_0 / Mathf.Pow(Distance_Calc(objToAffect.rb.position), 2)) * (0 - Mathf.Pow(r_0 / Distance_Calc(objToAffect.rb.position), 5));
        Vector3 force = NormDistance_Calc(objToAffect.rb.position) * forceMagnitude;
        objToAffect.rb.AddForce(force);
    }

    void BondAttract(Molecule objToAffect)
    {
        float forceMagnitude = K_b * (Distance_Calc(objToAffect.rb.position) - r_0);
        Vector3 force = NormDistance_Calc(objToAffect.rb.position) * forceMagnitude;
        objToAffect.rb.AddForce(force);
    }

    void AngleRepulse(Molecule Hydrogen_1, Molecule Hydrogen_2)
    {
        float forceMagnitude = K_theta * (Distance_Calc_Between(Hydrogen_1.transform.position, Hydrogen_2.transform.position) - r_0*DistanceModifier); 
        Vector3 force = NormDistance_Calc_Between(Hydrogen_1.rb.position, Hydrogen_2.rb.position) * forceMagnitude;
        Hydrogen_2.rb.AddForce(force);
    }
    private void BondCheck()
    {
        foreach (Molecule molecule in FindObjectsOfType<Molecule>())
        {
            if (this != molecule && BondedMolecules.Count < MaxBonds && !BondedMolecules.Contains(molecule))
            {
                if (molecule.BondedMolecules.Contains(this))
                {
                    rb.useGravity = false;
                    BondedMolecules.Add(molecule);
                    if (this.tag == molecule.tag)
                        MaxBonds = 1;
                }
                if (Distance_Calc(molecule.GetComponent<Transform>().position) <= BondDistance && molecule.BondedMolecules.Count < molecule.MaxBonds)
                {
                    if (this.tag == molecule.tag && BondedMolecules.Count == 0 && molecule.BondedMolecules.Count == 0)
                    {
                        BondedMolecules.Add(molecule);
                        MaxBonds = 1;
                        rb.useGravity = false;

                    }
                    else if (this.tag != molecule.tag)
                    {
                        BondedMolecules.Add(molecule);
                        rb.useGravity = false;
                    }
                }
            }
        }
    }
    private void CheckDeath()
    {
        if (Mathf.Abs(transform.position.x) > 50.0f || Mathf.Abs(transform.position.z) > 50.0f || transform.position.y > 50.0f || transform.position.y < -10.0f)
        {
            if (BondedMolecules.Count > 0)
            {
                foreach (Molecule molecule in BondedMolecules)
                {
                    if (molecule.BondedMolecules.Contains(this))
                        molecule.BondedMolecules.Remove(this);
                }
            }
            Destroy(gameObject);
        }
    }
    private float Distance_Calc(Vector3 Position_Other)
    {
        return Mathf.Abs((transform.position - Position_Other).magnitude);
    }
    private float Distance_Calc_Between(Vector3 Position_1, Vector3 Position_2)
    {
        return Mathf.Abs((Position_1 - Position_2).magnitude);
    }
    private Vector3 NormDistance_Calc(Vector3 Position_Other)
    {
        return (transform.position - Position_Other) / Distance_Calc(Position_Other);
    }
    private Vector3 NormDistance_Calc_Between(Vector3 Position_1, Vector3 Position_2)
    {
        return (Position_1 - Position_2) / Distance_Calc(Position_2);
    }
}
