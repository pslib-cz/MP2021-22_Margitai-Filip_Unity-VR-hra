using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// --- GameController ---
// Řízení, komunikace s unity, propojení modelů a dat

public class GameController : MonoBehaviour
{
    private AktualniInformace aktualniInformace;
    private DataCollection dataCollection;
    public Osoba osoba;
    private static System.Random random;
    private int cas = 0;

    public GameObject Obcanka;
    public GameObject PracPovoleni;
    public GameObject Povolenka;
    public GameObject ImigracList;
    public void Start()
    {
        aktualniInformace = GetComponent<AktualniInformace>();
        dataCollection = GetComponent<DataCollection>();
        random = new System.Random();
    }
    public void Update()
    {
        //if (Time.time > cas)
        //{
        //    cas += 3;
        //    DalsiClovek();
        //    VytvorObjekty();
        //    Debug.Log("Vytvořeno");
        //}
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
        var mesta = dataCollection.Mesta.Where(m => m.Planeta == osoba.Planeta.TypPlanety && m.JePlatne).ToList();
        var mesto = mesta[random.Next(mesta.Count)];

        var startDate = aktualniInformace.AktualniDen;
        var endDate = aktualniInformace.AktualniDen.AddYears(1);

        var platnost = NahodneDatum(startDate, endDate);
        osoba.Obcanka = new Obcanka(osoba.JmenoKrestni, osoba.JmenoPrijmeni, osoba.Planeta, platnost, osoba.Pohlavi, osoba.Narozeni, mesto.Text, osoba.Id);

        // Jakýkoliv další dokument, je potřeba pro vstup cizinci
        if (osoba.Planeta.TypPlanety != TypPlanety.Zeme)
        {
            GenerujDruhyDokument(true);
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

            List<Mesto> mesta = new List<Mesto>();
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
                        GenerujDruhyDokument(false);
                        provedloSe = true;
                    }
                    break;
                case "NarozeniNeshoduji":
                    if (osoba.TypDruhehoDokumentu != TypDokumentu.Povolenka)
                    {
                        osoba.Narozeni = osoba.Narozeni.AddDays(random.Next(-400, 400));
                        GenerujDruhyDokument(false);
                        provedloSe = true;
                    }
                    break;
                case "IdNeshoduji":
                    //if (osoba.TypDruhehoDokumentu != TypDokumentu.PracovniPovoleni)
                    //{
                        osoba.Id = NahodnyRetezec(9).Insert(5, "-");
                        GenerujDruhyDokument(false);
                        provedloSe = true;
                    //}
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
                        osoba.DruhyDokument.Planeta = dataCollection.Planety.SingleOrDefault(p => p.TypPlanety == TypPlanety.LaleloAa);
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
                    mesta = dataCollection.Mesta.Where(m => m.Planeta == osoba.Planeta.TypPlanety && !m.JePlatne).ToList();
                    osoba.Obcanka.Mesto = mesta[random.Next(mesta.Count)].Text;
                    provedloSe = true;
                    break;
                case "MestoJinaPlaneta":
                    mesta = dataCollection.Mesta.Where(m => m.Planeta != osoba.Planeta.TypPlanety).ToList();
                    osoba.Obcanka.Mesto = mesta[random.Next(mesta.Count)].Text;
                    provedloSe = true;
                    break;
                case "NeplatnaPrace":
                    if (osoba.TypDruhehoDokumentu == TypDokumentu.PracovniPovoleni)
                    {
                        var VsechnyPrace = dataCollection.Prace;
                        foreach(var prace in aktualniInformace.PlatnaPrace)
                        {
                            VsechnyPrace.Remove(prace);
                        }
                        osoba.Prace = VsechnyPrace[random.Next(VsechnyPrace.Count)];
                        GenerujDruhyDokument(false);
                        provedloSe = true;
                    }
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
        random = new System.Random();
        var krestniJmena = new List<Jmeno>();
        var prijmeniJmena = new List<Jmeno>();
        var pohlavi = new Pohlavi();
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
                var vicePohlavi = Enum.GetValues(typeof(Pohlavi));
                pohlavi = (Pohlavi)vicePohlavi.GetValue(random.Next(vicePohlavi.Length));
                break;
            case TypPlanety.LaleloAa:
                krestniJmena = dataCollection.JmenaKrestni.Where(j => j.Planeta == TypPlanety.LaleloAa).ToList();
                prijmeniJmena = dataCollection.JmenaPrijmeni.Where(j => j.Planeta == TypPlanety.LaleloAa).ToList();
                pohlavi = MuzXZena();
                break;
        }
        return new CeleJmenoAPohlavi(krestniJmena[random.Next(krestniJmena.Count)].Text, prijmeniJmena[random.Next(prijmeniJmena.Count)].Text, pohlavi);
    }
    private void GenerujDruhyDokument(bool ZmenitDokument)
    {
        TypDokumentu typDruhehoDokumentu;
        if (ZmenitDokument)
        {
            var values = Enum.GetValues(typeof(TypDokumentu));;
            do
            {
                typDruhehoDokumentu = (TypDokumentu)values.GetValue(random.Next(values.Length));
            } while ((typDruhehoDokumentu == TypDokumentu.Obcanka) || (typDruhehoDokumentu == TypDokumentu.PracovniPovoleni));
            osoba.TypDruhehoDokumentu = typDruhehoDokumentu;
        }
        else
        {
            typDruhehoDokumentu = osoba.TypDruhehoDokumentu;
        }

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
    public void VytvorObjekty()
    {
        // --- Obcanka ---
        var Udaje = Obcanka.transform.GetChild(1);
        Udaje.GetChild(1).GetComponent<TextMeshPro>().text = osoba.Obcanka.JmenoPrijmeni + ", " + osoba.Obcanka.JmenoKrestni;
        Udaje.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Obcanka.Narozeni.ToString("MM/dd/yyyy");
        Udaje.GetChild(3).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Obcanka.Pohlavi.ToString();
        Udaje.GetChild(4).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Obcanka.Mesto;
        Udaje.GetChild(5).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Obcanka.Platnost.ToString("MM/dd/yyyy");
        Udaje.GetChild(7).GetComponent<TextMeshPro>().text = osoba.Obcanka.Planeta.TypPlanety.ToString();
        Udaje.GetChild(8).GetComponent<TextMeshPro>().text = osoba.Obcanka.Id;
        switch (osoba.Obcanka.Planeta.TypPlanety)
        {
            case TypPlanety.Zeme:
                Udaje.GetChild(7).GetComponent<TextMeshPro>().color = Color.green;
                break;
            case TypPlanety.C212:
                Udaje.GetChild(7).GetComponent<TextMeshPro>().color = Color.cyan;
                break;
            case TypPlanety.Ugandus:
                Udaje.GetChild(7).GetComponent<TextMeshPro>().color = Color.red;
                break;
            case TypPlanety.LaleloAa:
                Udaje.GetChild(7).GetComponent<TextMeshPro>().color = Color.blue;
                break;
        }


        Instantiate(Obcanka);

        if (!osoba.JePlatny)
        {
            Debug.Log(osoba.Obcanka.ToString());
            if (osoba.DruhyDokument != null) Debug.Log(osoba.DruhyDokument.ToString());
            else Debug.Log("Nope");
            Debug.Log(osoba.NeplatnyUdaj);
        }


        // --- Druhy dokument ---
        if (osoba.DruhyDokument != null)
        {
            switch (osoba.TypDruhehoDokumentu)
            {
                case TypDokumentu.PracovniPovoleni:
                    Udaje = PracPovoleni.transform.GetChild(1);
                    Udaje.GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.JmenoKrestni;
                    Udaje.GetChild(1).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.JmenoPrijmeni;
                    Udaje.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Narozeni.ToString("MM/dd/yyyy");
                    Udaje.GetChild(3).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.Platnost.ToString("MM/dd/yyyy");
                    Udaje.GetChild(4).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Pohlavi.ToString();
                    Udaje.GetChild(5).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Prace;
                    Udaje.GetChild(6).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.Planeta.TypPlanety.ToString();
                    Instantiate(PracPovoleni);
                    break;
                case TypDokumentu.ImigracniList:
                    Udaje = ImigracList.transform.GetChild(1);
                    Udaje.GetChild(0).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.JmenoKrestni + " " + osoba.DruhyDokument.JmenoPrijmeni;
                    Udaje.GetChild(1).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.Platnost.ToString("MM/dd/yyyy");
                    Udaje.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Narozeni.ToString("MM/dd/yyyy");
                    Udaje.GetChild(3).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Id;
                    Udaje.GetChild(4).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.Planeta.TypPlanety.ToString();
                    Instantiate(ImigracList);
                    break;
                case TypDokumentu.Povolenka:
                    Udaje = Povolenka.transform.GetChild(1);
                    Udaje.GetChild(0).GetComponent<TextMeshPro>().text = osoba.Obcanka.JmenoPrijmeni.ToUpper() + ", " + osoba.Obcanka.JmenoKrestni.ToUpper();
                    Udaje.GetChild(1).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Pohlavi.ToString();
                    Udaje.GetChild(2).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Id;
                    Udaje.GetChild(3).GetChild(1).GetComponent<TextMeshPro>().text = osoba.DruhyDokument.Platnost.ToString("MM/dd/yyyy");
                    Udaje.GetChild(4).GetChild(1).GetComponent<TextMeshPro>().text = osoba.Planeta.TypPlanety.ToString();
                    Instantiate(Povolenka);
                    break;
            }

        }
    }
}
