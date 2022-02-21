using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListyNazvu : MonoBehaviour
{
    public DateTime DnesniDatum;
    public int Sance;
    // Země
    public List<string> JmenaKrestni_Z;
    public List<string> JmenaPrijmeni_Z;
    public List<string> Mesta_Z;
    public List<string> Mesta_Zn;

    // C212
    public List<string> JmenaKrestni_C;
    public List<string> JmenaPrijmeni_C;
    public List<string> Mesta_C;
    public List<string> Mesta_Cn;

    // Ugandus
    public List<string> JmenaKrestni_U;
    public List<string> JmenaPrijmeni_U;
    public List<string> Mesta_U;
    public List<string> Mesta_Un;

    // LaleloOlelAa
    public List<string> JmenaKrestni_L;
    public List<string> JmenaPrijmeni_L;
    public List<string> Mesta_L;
    public List<string> Mesta_Ln;

    public ListyNazvu()
    {
        DnesniDatum = new DateTime(2065, 5, 15);
    }
}