using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispararArma : MonoBehaviour
{
    [SerializeField] private GameObject burbujaPrefab;
    [SerializeField] private Transform puntoDeDisparo;
    [SerializeField] private Camera camaraPrincipal;
    public AdministradorAudio administradorAudio;
    private CooldownBurbuja cooldownBurbuja;
    private void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
        cooldownBurbuja = GetComponent<CooldownBurbuja>();
    }
    //hola soy yo el goblin que anima aqui esta el animador jijiji!




    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DispararBurbuja();
        }
    }

    private void DispararBurbuja()
    {
        playerController playerController = GetComponentInParent<playerController>();
        if (cooldownBurbuja.isCoolingDown || playerController.isCursorLocked == false) return;
        // Crear un Ray desde el centro de la cámara
        Ray ray = camaraPrincipal.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        
        Vector3 direccionDisparo;
        Quaternion rotacionDisparo;

        // Si el Raycast golpea algo y la distancia es mayor a 0.8, calcula la dirección hacia el punto de impacto
        if (Physics.Raycast(ray, out hit) && Vector3.Distance(puntoDeDisparo.position, hit.point) > 1.2f)
        {
            direccionDisparo = (hit.point - puntoDeDisparo.position).normalized;
            rotacionDisparo = Quaternion.LookRotation(direccionDisparo);
            //Debug.Log($"Impacto con: {hit.collider.gameObject.name}"); // Log del objeto impactado
        }
        else
        {
            // Si no golpea nada o está demasiado cerca, dispara hacia adelante desde la cámara
            direccionDisparo = camaraPrincipal.transform.forward;
            rotacionDisparo = Quaternion.LookRotation(direccionDisparo);
        }

        GameObject burbuja = Instantiate(burbujaPrefab, puntoDeDisparo.position, rotacionDisparo);
        cooldownBurbuja.Cooldown();
        GetComponent<girar_municion>().BoostSpeed();
        administradorAudio.ReproducirSonidoDisparoBurbuja();
        Destroy(burbuja, 15f);
        disparoMover();

    }
    public void disparoMover()
    {
      Retroceso retroceso = this.GetComponent<Retroceso>();
      // Verificar si se encontró el componente
        if (retroceso != null)
        {
            // Llamar a la función AplicarRetroceso pasando el GameObject actual
            retroceso.AplicarRetroceso(this.gameObject);
        }
        else
        {
            Debug.LogError("Mete el script RETROCESO, en el arma.. -el goblin cansado");
        }
    }
}