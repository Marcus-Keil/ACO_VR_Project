using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField] Material GoalMat;


    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Oxygen"))
        {
            Molecule otherMol = other.gameObject.GetComponent<Molecule>();
            if (otherMol.BondedMolecules.Count == 2)
            {
                Debug.Log(otherMol.BondedMolecules.Count);
                GoalMat.SetColor("GoalColor", Color.red);
            }
        }
    }
}
