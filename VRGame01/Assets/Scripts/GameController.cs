using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// --- GameController ---
// Řízení, komunikace s unity, propojení modelů a dat

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
    void Start()
    {
        DataCollection = GetComponent<DataCollection>();

        AktualniDen = DataCollection.StartovniDatum;
        OdpocetDnu = 0;
        AktualniSance = 0;

        NastavPrace();
        NastavZlocince();
    }

    public void DalsiDen()
    {
        AktualniDen.AddDays(1);

        AktualniSance = DataCollection.SanceSkrzDny[++OdpocetDnu];

        NastavPrace();
        NastavZlocince();
    }
    private void NastavPrace()
    {
        PlatnaPrace = DataCollection.Prace.OrderBy(x => random.Next()).Take(5).ToList();
    }
    private void NastavZlocince()
    {
        // Nastav zločince
    }
}
public class GameController : MonoBehaviour
{
    private AktualniInformace aktualniInformace;
    private DataCollection dataCollection;
    private Osoba osoba;
    private static System.Random random;
    void Start()
    {
        aktualniInformace = GetComponent<AktualniInformace>();
        dataCollection = GetComponent<DataCollection>();
        random = new System.Random();
    }
    public void DalsiClovek()
    {
        osoba = new Osoba();
        GenerujOsobu();
        GenerujDoklady();
        if (!osoba.JePlatny) GenerujChybu();
    }
    public void GenerujOsobu()
    {
        // Nahodna planeta
        osoba.Planeta = dataCollection.Planety[random.Next(dataCollection.Planety.Count)];

        // Je platný?
        osoba.JePlatny = random.Next(100) >= (aktualniInformace.AktualniSance + osoba.Planeta.ProcentaPadelani);

        // Základní údaje
        var celeJmenoAPohlavi = GenerujCeleJmenoAPohlavi(osoba.Planeta.TypPlanety);

        osoba.JmenoKrestni = celeJmenoAPohlavi.JmenoKrestni;
        osoba.JmenoPrijmeni = celeJmenoAPohlavi.JmenoPrijmeni;
        osoba.Pohlavi = celeJmenoAPohlavi.Pohlavi;

        var startDate = new DateTime(1980, 1, 1);
        var endDate = new DateTime(2050, 12, 31);
        osoba.Narozeni = NahodneDatum(startDate, endDate);

        osoba.Id = NahodnyRetezec(9).Insert(5, "-");
    }
    private void GenerujDoklady()
    {
        // Obcanka - Mají všichni
        var mesta = dataCollection.Mesta.Where(m => m.Planeta == osoba.Planeta.TypPlanety).ToList();
        var mesto = mesta[random.Next(mesta.Count)];

        var startDate = aktualniInformace.AktualniDen;
        var endDate = aktualniInformace.AktualniDen.AddYears(1);

        var platnost = NahodneDatum(startDate, endDate);
        osoba.Obcanka = new Obcanka(osoba.JmenoKrestni, osoba.JmenoPrijmeni, osoba.Planeta, platnost, osoba.Pohlavi, osoba.Narozeni, mesto.Text, osoba.Id);

        // Jakýkoliv další dokument, je potřeba pro vstup cizinci
        if (osoba.Planeta.TypPlanety != TypPlanety.Zeme)
        {
            GenerujDruhyDokument();
        }
    }
    private void GenerujChybu()
    {
        bool provedloSe = false;
        while (!provedloSe)
        {
            // Neplatný údaj
            var mozneProblemy = new List<TypUdaje>();
            if (osoba.Planeta.TypPlanety == TypPlanety.Zeme) mozneProblemy = dataCollection.TypyUdaje.Where(t => t.OvlivnujeZdejsi).ToList();
            else mozneProblemy = dataCollection.TypyUdaje;
            osoba.NeplatnyUdaj = mozneProblemy[random.Next(mozneProblemy.Count)];

            switch (osoba.NeplatnyUdaj.NazevUdaje)
            {
                case "JmenaNeshoduji":
                    var celeJmenoAPohlavi = GenerujCeleJmenoAPohlavi(osoba.Planeta.TypPlanety);
                    osoba.DruhyDokument.JmenoKrestni = celeJmenoAPohlavi.JmenoKrestni;
                    osoba.DruhyDokument.JmenoPrijmeni = celeJmenoAPohlavi.JmenoPrijmeni;
                    provedloSe = true;
                    break;
                case "PohlaviNeshoduji":
                    if (osoba.TypDruhehoDokumentu != TypDokumentu.ImigracniList)
                    {
                        if (osoba.Pohlavi == Pohlavi.Muz) osoba.Pohlavi = Pohlavi.Zena;
                        else if (osoba.Pohlavi == Pohlavi.Zena) osoba.Pohlavi = Pohlavi.Muz;
                        else osoba.Pohlavi = Pohlavi.Zena;
                        GenerujDruhyDokument();
                        provedloSe = true;
                    }
                    break;
                case "NarozeniNeshoduji":
                    if (osoba.TypDruhehoDokumentu != TypDokumentu.Povolenka)
                    {
                        osoba.Narozeni = osoba.Narozeni.AddDays(random.Next(-400, 400));
                        GenerujDruhyDokument();
                        provedloSe = true;
                    }
                    break;
                case "IdNeshoduji":
                    if (osoba.TypDruhehoDokumentu != TypDokumentu.PracovniPovoleni)
                    {
                        osoba.Id = NahodnyRetezec(9).Insert(5, "-");
                        GenerujDruhyDokument();
                        provedloSe = true;
                    }
                    break;
                case "PlanetyNeshoduji":
                    if (osoba.Planeta == dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.Zeme))
                    {
                        osoba.DruhyDokument.Planeta = dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.C212);
                    }
                    else if (osoba.Planeta == dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.C212))
                    {
                        osoba.DruhyDokument.Planeta = dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.Ugandus);
                    }
                    else if (osoba.Planeta == dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.Ugandus))
                    {
                        osoba.DruhyDokument.Planeta = dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.LaleloOlelAa);
                    }
                    else
                    {
                        osoba.DruhyDokument.Planeta = dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.Zeme);
                    }
                    provedloSe = true;
                    break;
                case "PlatnostVyprsela":
                    if(osoba.Planeta.TypPlanety == TypPlanety.Zeme)
                    {
                        osoba.Obcanka.Platnost = NahodneDatum(aktualniInformace.AktualniDen.AddDays(-15), aktualniInformace.AktualniDen.AddDays(-1));
                    }
                    else
                    {
                        int rnd = random.Next(2);
                        if (rnd == 0)
                        {
                            osoba.Obcanka.Platnost = NahodneDatum(aktualniInformace.AktualniDen.AddDays(-15), aktualniInformace.AktualniDen.AddDays(-1));
                        }
                        else
                        {
                            osoba.DruhyDokument.Platnost = NahodneDatum(aktualniInformace.AktualniDen.AddDays(-15), aktualniInformace.AktualniDen.AddDays(-1));
                        }
                    }
                    provedloSe = true;
                    break;
                case "MestoNevydava":
                    break;
                case "MestoJinaPlaneta":
                    break;
                case "NeplatnaPrace":
                    break;
                case "ChybiDokument":
                    osoba.DruhyDokument = null;
                    provedloSe = true;
                    break;
                    //case "FotkyNeshoduji":
                    //    break;            
                    //case "SpatnaFotka":
                    //    break;            
                    //case "JeZlocinec":
                    //    break;
            }
        }
    }
    private Pohlavi MuzXZena()
    {
        int rnd = random.Next(2);
        if (rnd == 0) return Pohlavi.Muz;
        else return Pohlavi.Zena;
    }
    private static DateTime NahodneDatum(DateTime startDate, DateTime endDate)
    {
        random = new System.Random();
        TimeSpan timeSpan = endDate - startDate;
        TimeSpan newSpan = new TimeSpan(0, random.Next(0, (int)timeSpan.TotalMinutes), 0);
        return startDate + newSpan;
    }
    private static string NahodnyRetezec(int length)
    {
        random = new System.Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    private CeleJmenoAPohlavi GenerujCeleJmenoAPohlavi(TypPlanety typPlanety)
    {
        var krestniJmena = new List<Jmeno>();
        var prijmeniJmena = new List<Jmeno>();
        var pohlavi = new Pohlavi();
        var vicePohlavi = Enum.GetValues(typeof(Pohlavi));
        switch (typPlanety)
        {
            case TypPlanety.Zeme:
                krestniJmena = dataCollection.JmenaKrestni.Where(j => j.Planeta == TypPlanety.Zeme).ToList();
                prijmeniJmena = dataCollection.JmenaPrijmeni.Where(j => j.Planeta == TypPlanety.Zeme).ToList();
                pohlavi = MuzXZena();
                break;
            case TypPlanety.C212:
                krestniJmena = dataCollection.JmenaKrestni.Where(j => j.Planeta == TypPlanety.C212).ToList();
                prijmeniJmena = dataCollection.JmenaPrijmeni.Where(j => j.Planeta == TypPlanety.C212).ToList();
                pohlavi = Pohlavi.Nebinarni;
                break;
            case TypPlanety.Ugandus:
                krestniJmena = dataCollection.JmenaKrestni.Where(j => j.Planeta == TypPlanety.Ugandus).ToList();
                prijmeniJmena = dataCollection.JmenaPrijmeni.Where(j => j.Planeta == TypPlanety.Ugandus).ToList();
                pohlavi = (Pohlavi)vicePohlavi.GetValue(random.Next(vicePohlavi.Length));
                break;
            case TypPlanety.LaleloOlelAa:
                krestniJmena = dataCollection.JmenaKrestni.Where(j => j.Planeta == TypPlanety.LaleloOlelAa).ToList();
                prijmeniJmena = dataCollection.JmenaPrijmeni.Where(j => j.Planeta == TypPlanety.LaleloOlelAa).ToList();
                pohlavi = MuzXZena();
                break;
        }
        return new CeleJmenoAPohlavi(krestniJmena[random.Next(krestniJmena.Count)].Text, prijmeniJmena[random.Next(prijmeniJmena.Count)].Text, pohlavi);
    }
    private void GenerujDruhyDokument()
    {
        var values = Enum.GetValues(typeof(TypDokumentu));
        TypDokumentu typDruhehoDokumentu;
        do
        {
            typDruhehoDokumentu = (TypDokumentu)values.GetValue(random.Next(values.Length));
        } while (typDruhehoDokumentu != TypDokumentu.Obcanka);
        osoba.TypDruhehoDokumentu = typDruhehoDokumentu;

        var startDate = aktualniInformace.AktualniDen;
        var endDate = aktualniInformace.AktualniDen.AddDays(7);

        var platnost = NahodneDatum(startDate, endDate);

        switch (typDruhehoDokumentu)
        {
            case TypDokumentu.Povolenka:
                osoba.DruhyDokument = new Povolenka(osoba.JmenoKrestni, osoba.JmenoPrijmeni, osoba.Planeta, platnost, osoba.Pohlavi, osoba.Id);
                break;
            case TypDokumentu.ImigracniList:
                osoba.DruhyDokument = new ImigracniList(osoba.JmenoKrestni, osoba.JmenoPrijmeni, osoba.Planeta, platnost, osoba.Narozeni, osoba.Id);
                break;
            case TypDokumentu.PracovniPovoleni:
                var prace = aktualniInformace.PlatnaPrace[random.Next(aktualniInformace.PlatnaPrace.Count)];
                osoba.DruhyDokument = new PracovniPovoleni(osoba.JmenoKrestni, osoba.JmenoPrijmeni, osoba.Planeta, platnost, osoba.Pohlavi, osoba.Narozeni, prace);
                break;
        }
    }
}
