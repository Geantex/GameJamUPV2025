using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sonidoIdol : MonoBehaviour
{
    public AdministradorAudio administradorAudio;

    // Start is called before the first frame update
    void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio3")?.GetComponent<AdministradorAudio>();

        // Sonido aparicion
        if (administradorAudio != null)
        {
            administradorAudio.ReproducirSuperIdol();
        }

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento al destruir el objeto
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        // Detener el sonido cuando la escena se descarga
        if (administradorAudio != null)
        {
            administradorAudio.DetenerAudio();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
