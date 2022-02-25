using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- DataStructure ---
// Struktury dat, které jsou přednahrané a mění se minimálně. (procenta padělání)

public class Jmeno
{
    public readonly string Text;
    public readonly TypPlanety Planeta;

    public Jmeno(string text, TypPlanety planeta)
    {
        Text = text;
        Planeta = planeta;
    }
}
public class Mesto
{
    public readonly string Text;
    public readonly TypPlanety Planeta;
    public readonly bool JePlatne;

    public Mesto(string text, TypPlanety planeta, bool jePlatne)
    {
        Text = text;
        Planeta = planeta;
        JePlatne = jePlatne;
    }
}
public class Planeta
{
    public readonly TypPlanety TypPlanety;
    public int ProcentaPadelani;
    //public List<TypDokumentu> PotrebneDokumenty;

    public Planeta(TypPlanety typPlanety, int procentaPadelani)
    {
        TypPlanety = typPlanety;
        ProcentaPadelani = procentaPadelani;
    }
}
public class TypUdaje
{
    public readonly string NazevUdaje;
    public readonly bool OvlivnujeZdejsi;

    public TypUdaje(string nazevUdaje, bool ovlivnujeZdejsi)
    {
        NazevUdaje = nazevUdaje;
        OvlivnujeZdejsi = ovlivnujeZdejsi;
    }
}
