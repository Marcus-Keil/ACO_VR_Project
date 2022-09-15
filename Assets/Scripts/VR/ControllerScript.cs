using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    Animator animator;

    private float triggerTarget;
    private float triggerCurrent;
    [SerializeField] private float triggerSpeed;
    private String animatorTriggerParam = "Trigger";
    public bool TutorialGlowBool = false;
    public GameObject GlowSphere;

    public float duration = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
}

    // Update is called once per frame
    void Update()
    {
        AnimateTrigger();
    }

    private void AnimateTrigger()
    {
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * triggerSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    public void StartGlow()
    {
        TutorialGlowBool = true;
        GlowSphere.SetActive(true);
    }

    public void EndGlow()
    {
        TutorialGlowBool = false;
        GlowSphere.SetActive(false);
    }
}
