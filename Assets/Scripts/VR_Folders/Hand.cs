using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    Animator animator;

    private float gripTarget;
    private float gripCurrent;
    [SerializeField] private float gripSpeed;
    private String animatorGripParam = "Grip";

    private float triggerTarget;
    private float triggerCurrent;
    [SerializeField] private float triggerSpeed;
    private String animatorTriggerParam = "Trigger";

    private float thumbTarget;
    private float thumbCurrent;
    [SerializeField] private float thumbSpeed;
    private String animatorThumbParam = "Thumb";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    private void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * gripSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * triggerSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
        if (thumbCurrent != thumbTarget)
        {
            thumbCurrent = Mathf.MoveTowards(thumbCurrent, thumbTarget, Time.deltaTime * thumbSpeed);
            animator.SetFloat(animatorThumbParam, thumbCurrent);
        }
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget=v;
    }
    internal void SetThumb(float v)
    {
        thumbTarget = v;
    }
}
