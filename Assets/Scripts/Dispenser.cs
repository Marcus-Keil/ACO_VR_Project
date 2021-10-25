using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{

    public Rigidbody MoleculeToDispense;
    public GameObject parentObject;
    public VR_Manager VR_Manager;
    public Vector3 DispenseLocation = new Vector3(0.0992f, 2.748576f, 0.8319845f);
    public Vector3 StartingForce = new Vector3(0.0f,0.0f,-0.5f);

    // Start is called before the first frame update

    public void Start()
    {
        VR_Manager = GameObject.Find("VR_Manager").GetComponent<VR_Manager>();
    }

    public void DispenseMolecule()
    {
        Rigidbody p = Instantiate(MoleculeToDispense, transform.position + DispenseLocation, Quaternion.identity);
        p.AddForce(StartingForce);
        p.transform.parent = parentObject.transform;
        p.useGravity = true;
        VR_Manager.GrabbableObjects.Add(p.transform.parent.gameObject);
    }
}
