using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasaDeLaMuerte : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Llama al m�todo Muerte del script Enemigo para destruir completamente el objeto
            Enemigo enemigoScript = other.GetComponentInParent<Enemigo>();
            if (enemigoScript != null)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().AddMoney(100);

                enemigoScript.Muerte();
            }
            else
            {
                Debug.LogError("El objeto golpeado no tiene el script Enemigo adjunto al padre.");
            }

            // Suma monedas al jugador
        }
        
    }
}
