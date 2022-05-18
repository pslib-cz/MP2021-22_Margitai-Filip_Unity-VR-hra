using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamp : MonoBehaviour
{
    public bool isRed;
    public GameObject ruka1;
    public GameObject ruka2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dokument")
        {
            var dokument = new GameObject();
            if (isRed)
            {
                dokument = other.transform.GetChild(3).gameObject;
                ruka1.GetComponent<ButtonPress>().jeZeleny = false;
                ruka2.GetComponent<ButtonPress>().jeZeleny = false;
            }
            else
            {
                dokument = other.transform.GetChild(2).gameObject;
                ruka1.GetComponent<ButtonPress>().jeZeleny = true;
                ruka2.GetComponent<ButtonPress>().jeZeleny = true;
            }
            dokument.SetActive(true);
            ruka1.GetComponent<ButtonPress>().jeOznaceny = true;
            ruka2.GetComponent<ButtonPress>().jeOznaceny = true;
            //razitko.transform.localPosition = new Vector3(,0,);
        }
    }
}
