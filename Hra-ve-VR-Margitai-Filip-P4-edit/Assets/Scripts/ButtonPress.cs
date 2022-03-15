using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameController gameController;
    private float casZmacknuti = 0;
    private float casPockani = 0.4f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Button" && (casZmacknuti + casPockani < Time.time))
        {
            other.GetComponent<ButtonPressAnim>().Press();
            gameController.DalsiClovek();
            gameController.VytvorObjekty();
            casZmacknuti = Time.time;
        }
    }
}
