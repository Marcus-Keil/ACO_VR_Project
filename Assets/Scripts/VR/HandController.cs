using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    [SerializeField] ControllerScript hand;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(controller.activateAction.action.ReadValue<float>());
        hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        if (StoredKnowledge.StartTutorial && !StoredKnowledge.DoneTutorial)
        {
            if (controller.activateAction.action.ReadValue<float>() > 0.5f)
            {
                StoredKnowledge.DoneTutorial = true;
                StoredKnowledge.StartTutorial = false;
            }
        }
    }
}
