using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidotosssy : MonoBehaviour
{
    [Header("Configuración Sonidos")]
    [SerializeField] public AdministradorAudio administradorAudio;
    // Start is called before the first frame update
    void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio1").GetComponent<AdministradorAudio>();
        //Sonido aparicion
        if (administradorAudio != null)
        {
            administradorAudio.ReproducirSonidoTortosaInicial();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
