using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    [SerializeField] Material GoalMat;
    private Animator animator;
    private String animatorEnter = "Enter";
    private String animatorGoal = "Finish";
    private bool Entered = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stationary"))
        {
            Entered = true;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Oxygen") && Entered)
        {
            Molecule otherMol = other.gameObject.GetComponent<Molecule>();
            if (otherMol.BondedMolecules.Count == 2)
            {
                animator.SetTrigger(animatorGoal);
            }
        }
    }

    public void EnterIn()
    {
        animator.SetTrigger(animatorEnter);
    }
}
