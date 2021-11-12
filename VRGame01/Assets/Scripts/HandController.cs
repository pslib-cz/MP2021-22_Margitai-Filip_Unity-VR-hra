using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    ActionBasedController controller;
    public Hand Hand;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
    }

    // Update is called once per frame
    void Update()
    {
        Hand.SetGrip(controller.selectAction.action.ReadValue<float>());
        Hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
    }
}
