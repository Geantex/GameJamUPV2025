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
        if (Input.GetMouseButtonDown(1))
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
        Quaternion rotacionDisparo;

        // Si el Raycast golpea algo, calcula la dirección hacia el punto de impacto
        if (Physics.Raycast(ray, out hit))
        {
            direccionDisparo = (hit.point - puntoDeDisparo.position).normalized;
            rotacionDisparo = Quaternion.LookRotation(direccionDisparo);
            //Debug.Log($"Impacto con: {hit.collider.gameObject.name}"); // Log del objeto impactado
        }
        else
        {
            // Si no golpea nada, dispara hacia adelante desde la cámara
            direccionDisparo = camaraPrincipal.transform.forward;
            rotacionDisparo = Quaternion.LookRotation(direccionDisparo);
        }

        // Instanciar la burbuja en el punto de disparo con la rotación correcta
        GameObject burbuja = Instantiate(burbujaPrefab, puntoDeDisparo.position, rotacionDisparo);

        // No es necesario asignar velocidad si la burbuja se mueve automáticamente
    }
}
