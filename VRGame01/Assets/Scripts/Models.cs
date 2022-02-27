using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// --- Models ---
// Modely tříd, které přímo nekomunikují s Unity
// dokumenty/DiagramFunkčnosti.png

public enum TypPlanety
{
    Zeme,           // Lidi
    C212,           // Medůzy
    Ugandus,        // Knuckles
    LaleloAa    // Slimáci
}
public enum TypDokumentu
{
    Obcanka,
    PracovniPovoleni,
    ImigracniList,
    Povolenka
}
/*public enum TypUdaje
{
    // --- Neshodují se údaje ---
    JmenaNeshoduji,
    PohlaviNeshoduji,
    NarozeniNeshoduji,
    IdNeshoduji,
    //FotkyNeshoduji,
    PlanetyNeshoduji,

    // --- Nesprávný údaj ---
    PlatnostVyprsela,
    MestoNevydava,
    MestoJinaPlaneta,
    //SpatnaFotka,
    //JeZlocinec,
    NeplatnaPrace,

    // --- Chybí dokument ---
    ChybiDokument,
    //ChybiPracPovoleni,
    //ChybiPovolenka,
    //ChybiImigracList,
    //ChybiObcanka
}*/
public enum Pohlavi
{
    Muz,
    Zena,
    Nebinarni
}
public abstract class Dokument
{
    public string JmenoKrestni;
    public string JmenoPrijmeni;
    public Planeta Planeta;
    public DateTime Platnost;
}
public class Obcanka : Dokument
{
    public Pohlavi Pohlavi;
    public DateTime Narozeni;
    public string Mesto; 
    public string Id;
    //public Fotka Fotka;
    public Obcanka(string jmenoKrestni, string jmenoPrijmeni, Planeta planeta, DateTime platnost, Pohlavi pohlavi, DateTime narozeni, string mesto, string id)
    {
        JmenoKrestni = jmenoKrestni;
        JmenoPrijmeni = jmenoPrijmeni;
        Planeta = planeta;
        Platnost = platnost;
        Pohlavi = pohlavi;
        Narozeni = narozeni;
        Mesto = mesto;
        Id = id;
    }
    public override string ToString()
    {
        return " --- Obcanka --- " +
            ", JmenoKrestni: " + JmenoKrestni +
            ", JmenoPrijmeni: " + JmenoPrijmeni +
            ", Pohlaví: " + Pohlavi.ToString() +
            ", Narozeni: " + Narozeni.ToString("MM/dd/yyyy") +
            ", Platnost: " + Platnost.ToString("MM/dd/yyyy") +
            ", Planeta: " + Planeta.TypPlanety.ToString() +
            ", Id: " + Id;
    }
}
public class PracovniPovoleni : Dokument
{
    public Pohlavi Pohlavi;
    public DateTime Narozeni;
    public string Prace;
    public PracovniPovoleni(string jmenoKrestni, string jmenoPrijmeni, Planeta planeta, DateTime platnost, Pohlavi pohlavi, DateTime narozeni, string prace)
    {
        JmenoKrestni = jmenoKrestni;
        JmenoPrijmeni = jmenoPrijmeni;
        Planeta = planeta;
        Platnost = platnost;
        Pohlavi = pohlavi;
        Narozeni = narozeni;
        Prace = prace;
    }
    public override string ToString()
    {
        return " --- PracovniPovoleni --- " +
            ", JmenoKrestni: " + JmenoKrestni +
            ", JmenoPrijmeni: " + JmenoPrijmeni +
            ", Pohlaví: " + Pohlavi.ToString() +
            ", Narozeni: " + Narozeni.ToString("MM/dd/yyyy") +
            ", Platnost: " + Platnost.ToString("MM/dd/yyyy") +
            ", Planeta: " + Planeta.TypPlanety.ToString() +
            ", Prace: " + Prace;
    }
}
public class Povolenka : Dokument
{
    public Pohlavi Pohlavi;
    public string Id;
    //public Fotka Fotka;
    public Povolenka(string jmenoKrestni, string jmenoPrijmeni, Planeta planeta, DateTime platnost, Pohlavi pohlavi, string id)
    {
        JmenoKrestni = jmenoKrestni;
        JmenoPrijmeni = jmenoPrijmeni;
        Planeta = planeta;
        Platnost = platnost;
        Pohlavi = pohlavi;
        Id = id;
    }
    public override string ToString()
    {
        return " --- Povolenka --- " +
            ", JmenoKrestni: " + JmenoKrestni +
            ", JmenoPrijmeni: " + JmenoPrijmeni +
            ", Pohlaví: " + Pohlavi.ToString() +
            ", Platnost: " + Platnost.ToString("MM/dd/yyyy") +
            ", Planeta: " + Planeta.TypPlanety.ToString() +
            ", Id: " + Id;
    }
}
public class ImigracniList : Dokument
{
    public DateTime Narozeni;
    public string Id;
    //public Fotka Fotka;
    public ImigracniList(string jmenoKrestni, string jmenoPrijmeni, Planeta planeta, DateTime platnost, DateTime narozeni, string id)
    {
        JmenoKrestni = jmenoKrestni;
        JmenoPrijmeni = jmenoPrijmeni;
        Planeta = planeta;
        Platnost = platnost;
        Narozeni = narozeni;
        Id = id;
    }
    public override string ToString()
    {
        return " --- ImigracniList --- " +
            ", JmenoKrestni: " + JmenoKrestni +
            ", JmenoPrijmeni: " + JmenoPrijmeni +
            ", Narozeni: " + Narozeni.ToString("MM/dd/yyyy") +
            ", Platnost: " + Platnost.ToString("MM/dd/yyyy") +
            ", Planeta: " + Planeta.TypPlanety.ToString() +
            ", Id: " + Id;
    }
}
public class Osoba
{
    // --- Viditelné údaje ---
    public string JmenoKrestni;
    public string JmenoPrijmeni;
    public Pohlavi Pohlavi;
    public DateTime Narozeni;
    public Planeta Planeta;
    public string Id;
    //public Fotka Fotka;

    // --- Skryté údaje ---
    public bool JePlatny;
    public TypUdaje NeplatnyUdaj;
    public Obcanka Obcanka;
    public Dokument DruhyDokument;
    public TypDokumentu TypDruhehoDokumentu;
    public string Prace;

    public override string ToString()
    {
        return "JmenoKrestni: " + JmenoKrestni +
            ", JmenoPrijmeni: " + JmenoPrijmeni +
            ", Pohlaví: " + Pohlavi.ToString() +
            ", Narozeni: " + Narozeni.ToString("MM/dd/yyyy") +
            ", Planeta: " + Planeta.TypPlanety.ToString() +
            ", Id: " + Id +
            ", JePlatny: " + JePlatny.ToString() +
            ", NeplatnyUdaj: " + NeplatnyUdaj.ToString() +
            Obcanka.ToString() +
            DruhyDokument.ToString();
    }
}
public class CeleJmenoAPohlavi
{
    public string JmenoKrestni;
    public string JmenoPrijmeni;
    public Pohlavi Pohlavi;

    public CeleJmenoAPohlavi(string jmenoKrestni, string jmenoPrijmeni, Pohlavi pohlavi)
    {
        JmenoKrestni = jmenoKrestni;
        JmenoPrijmeni = jmenoPrijmeni;
        Pohlavi = pohlavi;
    }
}