using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- DataCollection ---
// Seznamy přednahraných dat.

public class DataCollection : MonoBehaviour
{
    public List<Jmeno> JmenaKrestni;
    public List<Jmeno> JmenaPrijmeni;
    public List<Mesto> Mesta;
    public List<Planeta> Planety;
    public List<string> Prace;
    public DateTime StartovniDatum;
    public List<int> SanceSkrzDny;
    public List<TypUdaje> TypyUdaje;
    public DataCollection()
    {
        JmenaKrestni = new List<Jmeno>()
        {
            new Jmeno("Jan", TypPlanety.Zeme),
            new Jmeno("Lukáš", TypPlanety.Zeme),
            new Jmeno("Radek", TypPlanety.Zeme),
            new Jmeno("Filip", TypPlanety.Zeme),
            new Jmeno("Michal", TypPlanety.Zeme),
            new Jmeno("Ondřej", TypPlanety.Zeme),
            new Jmeno("Karel", TypPlanety.Zeme),
            new Jmeno("Andrej", TypPlanety.Zeme),
            new Jmeno("Zdeněk", TypPlanety.Zeme),
            new Jmeno("Luděk", TypPlanety.Zeme),
            new Jmeno("Klára", TypPlanety.Zeme),
            new Jmeno("Magda", TypPlanety.Zeme),
            new Jmeno("Jana", TypPlanety.Zeme),
            new Jmeno("Eliška", TypPlanety.Zeme),
            new Jmeno("Gertruda", TypPlanety.Zeme),
            new Jmeno("Anna", TypPlanety.Zeme),
            new Jmeno("Marta", TypPlanety.Zeme),
            new Jmeno("Františka", TypPlanety.Zeme),
            new Jmeno("Růžena", TypPlanety.Zeme),
            new Jmeno("Soňa", TypPlanety.Zeme),
            new Jmeno("Kalme", TypPlanety.C212),
            new Jmeno("Junna", TypPlanety.C212),
            new Jmeno("Obrozoure", TypPlanety.C212),
            new Jmeno("Kaz zaa", TypPlanety.C212),
            new Jmeno("Objat", TypPlanety.C212),
            new Jmeno("Olle ale", TypPlanety.C212),
            new Jmeno("Ro meo", TypPlanety.C212),
            new Jmeno("Gladdda", TypPlanety.C212),
            new Jmeno("Rapun", TypPlanety.C212),
            new Jmeno("Onojednovejce", TypPlanety.C212),
            new Jmeno("Babovka", TypPlanety.C212),
            new Jmeno("Glagla", TypPlanety.C212),
            new Jmeno("Abidemi", TypPlanety.Ugandus),
            new Jmeno("Bamidele", TypPlanety.Ugandus),
            new Jmeno("Taraji", TypPlanety.Ugandus),
            new Jmeno("Zuri", TypPlanety.Ugandus),
            new Jmeno("Idir", TypPlanety.Ugandus),
            new Jmeno("Faraji", TypPlanety.Ugandus),
            new Jmeno("Zaire", TypPlanety.Ugandus),
            new Jmeno("Diallo", TypPlanety.Ugandus),
            new Jmeno("Kwame", TypPlanety.Ugandus),
            new Jmeno("Makena", TypPlanety.Ugandus),
            new Jmeno("Chidinma", TypPlanety.Ugandus),
            new Jmeno("Lalelo", TypPlanety.LaleloAa),
            new Jmeno("Lelelau", TypPlanety.LaleloAa),
            new Jmeno("LoO", TypPlanety.LaleloAa),
            new Jmeno("LoOa", TypPlanety.LaleloAa),
            new Jmeno("LoaLao", TypPlanety.LaleloAa),
            new Jmeno("Lelu lou", TypPlanety.LaleloAa),
            new Jmeno("OleLo", TypPlanety.LaleloAa),
            new Jmeno("L o", TypPlanety.LaleloAa),
            new Jmeno("LoLaLeLaLo", TypPlanety.LaleloAa),
            new Jmeno("LuL", TypPlanety.LaleloAa),
            new Jmeno("Loola", TypPlanety.LaleloAa),
            new Jmeno("LaaleO", TypPlanety.LaleloAa)
        };
        JmenaPrijmeni = new List<Jmeno>()
        {
            new Jmeno("Novák", TypPlanety.Zeme),
            new Jmeno("Dvořák", TypPlanety.Zeme),
            new Jmeno("Němec", TypPlanety.Zeme),
            new Jmeno("Pokorný", TypPlanety.Zeme),
            new Jmeno("Žid", TypPlanety.Zeme),
            new Jmeno("Beneš", TypPlanety.Zeme),
            new Jmeno("Kazda", TypPlanety.Zeme),
            new Jmeno("Fialový", TypPlanety.Zeme),
            new Jmeno("Kovář", TypPlanety.Zeme),
            new Jmeno("Sobotka", TypPlanety.Zeme),
            new Jmeno("Krejčí", TypPlanety.Zeme),
            new Jmeno("Kříž", TypPlanety.Zeme),
            new Jmeno("Matoušek", TypPlanety.Zeme),
            new Jmeno("Bláha", TypPlanety.Zeme),
            new Jmeno("Bureš", TypPlanety.Zeme),
            new Jmeno("Bárta", TypPlanety.Zeme),
            new Jmeno("Vítek", TypPlanety.Zeme),
            new Jmeno("Vondra", TypPlanety.Zeme),
            new Jmeno("Ondřej", TypPlanety.Zeme),
            new Jmeno("Janu", TypPlanety.Zeme),
            new Jmeno("Duracel", TypPlanety.C212),
            new Jmeno("Nevim", TypPlanety.C212),
            new Jmeno("Francuz", TypPlanety.C212),
            new Jmeno("Brasna", TypPlanety.C212),
            new Jmeno("Nebrasna", TypPlanety.C212),
            new Jmeno("Galadi i", TypPlanety.C212),
            new Jmeno("Jaz maz kaz", TypPlanety.C212),
            new Jmeno("Nicmoc", TypPlanety.C212),
            new Jmeno("Alejeden", TypPlanety.C212),
            new Jmeno("Pokmr er", TypPlanety.C212),
            new Jmeno("Bubana", TypPlanety.C212),
            new Jmeno("Lappa", TypPlanety.C212),
            new Jmeno("Mohamed", TypPlanety.Ugandus),
            new Jmeno("Ali", TypPlanety.Ugandus),
            new Jmeno("Ahmed", TypPlanety.Ugandus),
            new Jmeno("Hassan", TypPlanety.Ugandus),
            new Jmeno("Diallo", TypPlanety.Ugandus),
            new Jmeno("Musa", TypPlanety.Ugandus),
            new Jmeno("Knuckles", TypPlanety.Ugandus),
            new Jmeno("Usman", TypPlanety.Ugandus),
            new Jmeno("Muhammad", TypPlanety.Ugandus),
            new Jmeno("Camara", TypPlanety.Ugandus),
            new Jmeno("Abdi", TypPlanety.Ugandus),
            new Jmeno("LoaLao", TypPlanety.LaleloAa),
            new Jmeno("Lopata", TypPlanety.LaleloAa),
            new Jmeno("LuLooOo", TypPlanety.LaleloAa),
            new Jmeno("OloLe", TypPlanety.LaleloAa),
            new Jmeno("Le", TypPlanety.LaleloAa),
            new Jmeno("Le E", TypPlanety.LaleloAa),
            new Jmeno("LoLeLa", TypPlanety.LaleloAa),
            new Jmeno("LoAoLe", TypPlanety.LaleloAa),
            new Jmeno("LeOu", TypPlanety.LaleloAa),
            new Jmeno("LoLaLau", TypPlanety.LaleloAa),
            new Jmeno("Lala", TypPlanety.LaleloAa),
            new Jmeno("Lale", TypPlanety.LaleloAa)
        };
        Mesta = new List<Mesto>()
        {
            new Mesto("Jablonec n/ N", TypPlanety.Zeme, true),
            new Mesto("Wuhan", TypPlanety.Zeme, true),
            new Mesto("Seattle", TypPlanety.Zeme, true),
            new Mesto("Smržovka", TypPlanety.Zeme, true),
            new Mesto("Riga", TypPlanety.Zeme, true),
            new Mesto("Warszawa", TypPlanety.Zeme, true),
            new Mesto("Tbilisi", TypPlanety.Zeme, true),
            new Mesto("Nairobi", TypPlanety.Zeme, true),
            new Mesto("Tegucigalpa", TypPlanety.Zeme, true),
            new Mesto("Brno", TypPlanety.Zeme, false),
            new Mesto("Miami", TypPlanety.Zeme, false),
            new Mesto("Paramaribo", TypPlanety.Zeme, false),
            new Mesto("Nové Město pod Smrkem", TypPlanety.Zeme, false),
            new Mesto("Katowice", TypPlanety.Zeme, false),
            new Mesto("Białystok", TypPlanety.Zeme, false),
            new Mesto("Wrocław", TypPlanety.Zeme, false),
            new Mesto("Kraków", TypPlanety.Zeme, false),
            new Mesto("Omnetu ra", TypPlanety.C212, true),
            new Mesto("Nomemalme", TypPlanety.C212, true),
            new Mesto("Uwu", TypPlanety.C212, true),
            new Mesto("Kon iec", TypPlanety.C212, true),
            new Mesto("Napomeon", TypPlanety.C212, true),
            new Mesto("Sa li na", TypPlanety.C212, true),
            new Mesto("Rohozjiec", TypPlanety.C212, true),
            new Mesto("Gagda ra nana", TypPlanety.C212, true),
            new Mesto("Unwe lle", TypPlanety.C212, true),
            new Mesto("Mala mlejn", TypPlanety.C212, false),
            new Mesto("Uven lle", TypPlanety.C212, false),
            new Mesto("Krus ov ijce", TypPlanety.C212, false),
            new Mesto("Manma iec", TypPlanety.C212, false),
            new Mesto("Kampala", TypPlanety.Ugandus, true),
            new Mesto("Mbale", TypPlanety.Ugandus, true),
            new Mesto("Mbarara", TypPlanety.Ugandus, true),
            new Mesto("Kabale", TypPlanety.Ugandus, true),
            new Mesto("Gulu", TypPlanety.Ugandus, true),
            new Mesto("Lira", TypPlanety.Ugandus, true),
            new Mesto("Gogonyo", TypPlanety.Ugandus, false),
            new Mesto("Mbarale", TypPlanety.Ugandus, false),
            new Mesto("Soroti", TypPlanety.Ugandus, false),
            new Mesto("Kunebravo", TypPlanety.Ugandus, false),
            new Mesto("LaLeLauLa", TypPlanety.LaleloAa, true),
            new Mesto("Lo Lau", TypPlanety.LaleloAa, true),
            new Mesto("Lal Le", TypPlanety.LaleloAa, true),
            new Mesto("LalaLeoui", TypPlanety.LaleloAa, true),
            new Mesto("LolalaA", TypPlanety.LaleloAa, true),
            new Mesto("LolalAa", TypPlanety.LaleloAa, false),
            new Mesto("Lal lE", TypPlanety.LaleloAa, false),
            new Mesto("LaulouLe", TypPlanety.LaleloAa, false),
            new Mesto("Lelen", TypPlanety.LaleloAa, false),
            new Mesto("LaulalEouLo", TypPlanety.LaleloAa, false),
        };
        Planety = new List<Planeta>()
        {
            new Planeta(TypPlanety.Zeme, 20),
            new Planeta(TypPlanety.C212, 15),
            new Planeta(TypPlanety.Ugandus, 35),
            new Planeta(TypPlanety.LaleloAa, 25),
        };
        Prace = new List<string>()
        {
            "zedník", "voják", "chirurg", "chemik", "programátor", "správce linuxových serverů", "hydrolog", "jeřábník", "kadeřník", "knihovník",
            "kuchař", "malíř", "logoped", "matematik", "pilot", "učitel", "řidič", "veterinář", "osvětlovač", "řezník", "sklář", "zahradník"
        };
        StartovniDatum = new DateTime(2067, 4, 6);
        SanceSkrzDny = new List<int>()
        {
            8, -5, 15, 3, -20, 13, -4, 6, -2, 7, -12, 6, 3, -7, 22, -6, 18, -19, 4, -8, 15, -25
        };
        TypyUdaje = new List<TypUdaje>()
        {
            new TypUdaje("JmenaNeshoduji", false),
            new TypUdaje("PohlaviNeshoduji", false),
            new TypUdaje("NarozeniNeshoduji", false),
            new TypUdaje("IdNeshoduji", false),
            //new TypUdaje("FotkyNeshoduji", false),
            new TypUdaje("PlanetyNeshoduji", false),
            new TypUdaje("PlatnostVyprsela", true),
            new TypUdaje("MestoNevydava", true),
            new TypUdaje("MestoJinaPlaneta", true),
            //new TypUdaje("SpatnaFotka", true),
            //new TypUdaje("JeZlocinec", true),
            new TypUdaje("NeplatnaPrace", true),
            new TypUdaje("ChybiDokument", false),
        };
    }
}
