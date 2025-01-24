using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burbujaHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Burbuja ha golpeado a un enemigo");
            Destroy(other.gameObject);
        }
        Destroy(gameObject);

    }
}
