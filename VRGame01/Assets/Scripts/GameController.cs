using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ListyNazvu Listy;
    private Pas AktualniPas;
    private double cas;
    void Start()
    {
        Listy = transform.GetChild(0).GetComponent<ListyNazvu>();
        cas = 0;
    }

    void Update()
    {
        if (Time.time > cas)
        {
            cas = Time.time + 1;
            AktualniPas = Pas.GenerujPas(Listy.Sance, Listy);
            Debug.Log(AktualniPas.ToString());
        }
    }
}
