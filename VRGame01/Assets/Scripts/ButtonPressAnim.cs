using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressAnim : MonoBehaviour
{
    public Vector3 DefaultPosition;
    public Vector3 PressedPossition;

    private float CasZmacknuti;
    private float CasPockani = 0.4f;
    private bool isPressed;

    public void Press()
    {
        transform.position = PressedPossition;
        CasZmacknuti = Time.time;
        isPressed = true;
        Debug.Log("Press");
    }
    void Update()
    {
        if (isPressed)
        {
            if(CasPockani + CasZmacknuti < Time.time)
            {
                isPressed = false;
                transform.position = DefaultPosition;
            }
        }
    }
}
