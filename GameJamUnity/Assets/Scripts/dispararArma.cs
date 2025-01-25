using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispararArma : MonoBehaviour
{
    [SerializeField] private GameObject burbujaPrefab;
    [SerializeField] private Transform puntoDeDisparo;
    [SerializeField] private Camera camaraPrincipal;

    //hola soy yo el goblin que anima aqui esta el animador jijiji!
    private dispararAnim animator;

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

        //animacion epica
        animator = GetComponent<dispararAnim>();
        if(animator != null)
        {
            animator.disparoAnim(true); // Activar la animación de disparo
            StartCoroutine(thoseWhoStop());
            
        }
        else
        {
            //el goblin animador dice: "este es un mensaje divertido espero que te rias"
            Debug.LogError("tienes que meter el animator! - el goblin animador");
        }

        // Instanciar la burbuja en el punto de disparo con la rotación correcta
        GameObject burbuja = Instantiate(burbujaPrefab, puntoDeDisparo.position, rotacionDisparo);

        // Desactivar la animación de disparo después de que termine
        
    }

    private IEnumerator thoseWhoStop()
    {
        // Obtener la duración de la animación de disparo desde el Animator
        float duracionAnimacion = animator.thoseWhoMove();

        // Esperar a que termine la animación
        //a saber porque esto funciona si alguien lo cambia te juro que le ##~### BOBBA #~~##
        // ups! - el goblin animador
        yield return new WaitForSeconds(duracionAnimacion/10);

        // Desactivar la animación de disparo
        animator.disparoAnim(false);
    }
}