using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItem : MonoBehaviour
{
    void Update()
    {
        if(transform.position.y < 0.1)
        {
            Destroy(gameObject);
        }
    }
}
