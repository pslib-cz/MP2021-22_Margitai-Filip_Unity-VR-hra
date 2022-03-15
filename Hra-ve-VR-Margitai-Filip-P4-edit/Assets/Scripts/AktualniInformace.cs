using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AktualniInformace : MonoBehaviour
{
    public DateTime AktualniDen;
    public int AktualniSance;
    public Osoba AktualniOsoba;
    public List<string> PlatnaPrace;
    //public List<Osoba> AktualniZlocinci;
    public DataCollection DataCollection;
    public int OdpocetDnu;

    private static System.Random random;
    public void Start()
    {
        DataCollection = GetComponent<DataCollection>();

        AktualniDen = DataCollection.StartovniDatum;
        OdpocetDnu = 0;
        AktualniSance = 50;

        NastavPrace();
        NastavZlocince();
    }

    public void DalsiDen()
    {
        AktualniDen.AddDays(1);

        AktualniSance = DataCollection.SanceSkrzDny[OdpocetDnu++];

        NastavPrace();
        NastavZlocince();
    }
    private void NastavPrace()
    {
        //PlatnaPrace = DataCollection.Prace.OrderBy(x => random.Next()).Take(5).ToList();

        //PlatnaPrace = new List<string>();
        //var TempList = DataCollection.Prace;
        //for(int i = 0; i < 5; i++)
        //{
        //    var RandomCislo = random.Next(TempList.Count);
        //    PlatnaPrace.Add(TempList[RandomCislo]);
        //    TempList.RemoveAt(RandomCislo);
        //}

        PlatnaPrace = new List<string>();
        PlatnaPrace.Add(PlatnaPrace[0]);
        PlatnaPrace.Add(PlatnaPrace[1]);
        PlatnaPrace.Add(PlatnaPrace[2]);
        PlatnaPrace.Add(PlatnaPrace[3]);
    }
    private void NastavZlocince()
    {
        // Nastav zločince
    }
}
