using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SimpleCoin : MonoBehaviour
{
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}