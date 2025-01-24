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
            // cogemos el script de PlayerMoney del jugador, le buscamos con tag y le sumamos 100 monedas
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().AddMoney(100);
            Destroy(other.gameObject);
        }
        Destroy(gameObject);

    }
}
