using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burbujaHit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("laser") || other.gameObject.CompareTag("Burbuja"))
        {
            return;
        }
        if(other.gameObject.CompareTag("BurbujaBlast"))
        {
            SerCalcinadoPorLaser burbujaCadena = GetComponent<SerCalcinadoPorLaser>();
            burbujaCadena.BubbleBlast();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Llama al m�todo Muerte del script Enemigo para destruir completamente el objeto
            Enemigo enemigoScript = other.GetComponentInParent<Enemigo>();
            if (enemigoScript != null)
            {
                enemigoScript.Muerte();
            }
            else
            {
                Debug.LogError("El objeto golpeado no tiene el script Enemigo adjunto al padre.");
            }

            // Suma monedas al jugador
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().AddMoney(100);
        }
        Destroy(gameObject);
    }
}

