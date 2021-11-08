using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Button : MonoBehaviour
{
    private Animator animator;
    private XRSimpleInteractable interactor;
    private String animatorPressedParam = "Pressed";
    public AudioSource pressedSound;

    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<XRSimpleInteractable>();
        animator = GetComponent<Animator>();
    }

    public void OnPress()
    {
        StartCoroutine(DissableButtonPress());
        animator.SetTrigger(animatorPressedParam);
        pressedSound.pitch = UnityEngine.Random.Range(0.8f, 1.0f);
        pressedSound.Play();
    }
    IEnumerator DissableButtonPress()
    {
        interactor.enabled = false;
        yield return new WaitForSeconds(1); 
        interactor.enabled = true;
    }
}
