using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaExplosionArea : MonoBehaviour
{
    public int explosionDamage = 200; // Fuerza de la explosión
    [SerializeField] private SphereCollider explosionCollider; // Collider de la explosión


    private void OnTriggerEnter(Collider other){
        // si explota la burbuja, en el radio hay que hacer muerte y destruccion (daño a enemigos y explosion en cadena de otras burbujas)
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
    }


    public void ExplosionBurbuja()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        //Debug.Log("Inicio de la explosión");

        // Radio inicial del collider
        float initialRadius = explosionCollider.radius;

        // Radio final del collider
        float targetRadius = 3f;

        // Tiempo total de expansión
        float duration = 0.5f;

        // Variable para llevar el tiempo transcurrido
        float elapsedTime = 0f;

        // Gradualmente aumentar el radio del collider
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolar entre el radio inicial y el final
            explosionCollider.radius = Mathf.Lerp(initialRadius, targetRadius, elapsedTime / duration);

            // Esperar hasta el siguiente frame
            yield return null;
        }

        // Asegurar que el radio final sea exactamente el objetivo
        explosionCollider.radius = targetRadius;

        // Destruir la burbuja después de la explosión
        Destroy(gameObject);
    }
}


