using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispararArma : MonoBehaviour
{
    [SerializeField] private GameObject burbujaPrefab;
    [SerializeField] private Transform puntoDeDisparo;
    [SerializeField] private Camera camaraPrincipal;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DispararBurbuja();
        }
    }

    private void DispararBurbuja()
    {
        // Crear un Ray desde el centro de la cámara
        Ray ray = camaraPrincipal.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 direccionDisparo;

        // Si el Raycast golpea algo, calcula la dirección hacia el punto de impacto
        if (Physics.Raycast(ray, out hit))
        {
            direccionDisparo = (hit.point - puntoDeDisparo.position).normalized;
        }
        else
        {
            // Si no golpea nada, dispara hacia adelante desde la cámara
            direccionDisparo = camaraPrincipal.transform.forward;
        }

        // Instanciar la burbuja en el punto de disparo
        GameObject burbuja = Instantiate(burbujaPrefab, puntoDeDisparo.position, Quaternion.identity);

        // Asignar velocidad a la burbuja en la dirección del disparo
        Rigidbody rb = burbuja.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direccionDisparo * 10f; // Cambia "10f" por la velocidad deseada
        }
    }
}
