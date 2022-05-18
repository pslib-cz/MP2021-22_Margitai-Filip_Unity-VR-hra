using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// --- HandController ---
// Ovládání chování rukou

public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand Hand;
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        Hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        Hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
