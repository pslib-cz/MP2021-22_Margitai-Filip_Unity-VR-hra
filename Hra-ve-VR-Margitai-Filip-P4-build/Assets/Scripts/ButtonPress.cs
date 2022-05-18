using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameController gameController;
    private static System.Random random;
    private float casZmacknuti = 0;
    private float casPockani = 0.4f;
    public bool byloZmacknuto;
    public int strikes = 0;
    private int scoreCount = 0; 
    public bool jeOznaceny;
    public bool jeZeleny;
    public GameObject score;
    public GameObject x1;
    public GameObject x2;
    public GameObject x3;
    public GameObject postavy;
    public GameObject end;
    private bool noMore;
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Button" && (casZmacknuti + casPockani < Time.time))
        {
            other.GetComponent<ButtonPressAnim>().Press();
            if (jeOznaceny && !noMore)
            {
                if (!byloZmacknuto)
                {
                    byloZmacknuto = true;
                    for (int i = 0; i < 4; i++)
                    {
                        postavy.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if ((gameController.osoba.JePlatny == jeZeleny) || (!gameController.osoba.JePlatny == !jeZeleny))
                    {
                        scoreCount += random.Next(350, 500);
                        score.GetComponent<TextMeshPro>().text = scoreCount.ToString();
                    }
                    else
                    {
                        strikes++;
                        switch (strikes)
                        {
                            case 1:
                                x1.SetActive(true);
                                break;
                            case 2:
                                x2.SetActive(true);
                                break;
                            case 3:
                                x3.SetActive(true);
                                end.SetActive(true);
                                noMore = true;
                                break;
                        }
                    }
                }
                else
                {
                    gameController.DalsiClovek();
                    gameController.VytvorObjekty();
                    switch (gameController.osoba.Planeta.TypPlanety)
                    {
                        case TypPlanety.Zeme:
                            postavy.transform.GetChild(0).gameObject.SetActive(true);
                            break;
                        case TypPlanety.C212:
                            postavy.transform.GetChild(1).gameObject.SetActive(true);
                            break;
                        case TypPlanety.LaleloAa:
                            postavy.transform.GetChild(2).gameObject.SetActive(true);
                            break;
                        case TypPlanety.Ugandus:
                            postavy.transform.GetChild(3).gameObject.SetActive(true);
                            break;
                    }
                    byloZmacknuto = false;
                    jeOznaceny = false;
                }
            }
            casZmacknuti = Time.time;
        }
    }
    public void Start()
    {
        random = new System.Random();
        jeOznaceny = true;
        byloZmacknuto = true;
        noMore = false;
    }
}
