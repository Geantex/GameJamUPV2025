using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaExplosionArea : MonoBehaviour
{
    public int explosionDamage = 200; // Fuerza de la explosión
    [SerializeField] private GameObject efectoExplosionBurbuja; // Prefab del efecto de explosión
    [SerializeField] private SphereCollider explosionCollider; // Collider de la explosión

    public void ExplosionBurbuja()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    private IEnumerator ExplosionCoroutine()
    {
        // Aquí puedes agregar las cosas que quieras hacer al inicio de la explosión
        Debug.Log("Inicio de la explosión");

        // Esperar 0.5 segundos
        yield return new WaitForSeconds(0.5f);

        // Aquí puedes agregar las cosas que quieras hacer después del tiempo de espera
        Debug.Log("Final de la explosión");
    }
}

