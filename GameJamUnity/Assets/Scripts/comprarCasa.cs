using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apareceCasa : MonoBehaviour
{
    private GameObject casaModelo; // Referencia al objeto CasaModelo

    private void Start()
    {
        // Encontramos el padre del padre del Cylinder
        Transform parentOfParent = transform.parent?.parent;

        if (parentOfParent != null)
        {
            // Buscamos el objeto CasaModelo entre los hijos del padre del padre
            casaModelo = parentOfParent.Find("CasaModelo")?.gameObject;

            if (casaModelo == null)
            {
                Debug.LogWarning("No se encontr� el objeto CasaModelo en la jerarqu�a.");
            }
        }
        else
        {
            Debug.LogError("El padre del padre del Cylinder no existe.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Activamos el objeto CasaModelo si lo encontramos
            if (casaModelo != null)
            {
                casaModelo.SetActive(true);
            }
            else
            {
                Debug.LogWarning("CasaModelo no fue asignado o no se encontr� en la jerarqu�a.");
            }

            // Desactivamos el objeto Cylinder
            gameObject.SetActive(false);
        }
    }
}