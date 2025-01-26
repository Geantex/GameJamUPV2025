//=======================================================================================================================================//
//=======================================================================================================================================//
//  Script:
//  AdministradorAudio
//
//  Descripci�n:
//  Este script centraliza la reproducci�n de sonidos en el proyecto.
//  Maneja sonidos globales y personalizados mediante un sistema de AudioSource.
//
//=======================================================================================================================================//
//=======================================================================================================================================//

using UnityEngine;

public class AdministradorAudio : MonoBehaviour
{
    //----------------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------- PROPIEDADES ----------------------------------------------------------//

    public static AdministradorAudio Instancia;

    [SerializeField] private AudioSource fuenteAudio;
    [SerializeField] private ConfiguracionAudio configuracionAudio;

    private AudioSource fuenteAudioLoop; // Nueva fuente para sonidos en bucle

    //----------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------- M�TODOS ------------------------------------------------------------//

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            // Crear una fuente de audio adicional para sonidos en bucle
            fuenteAudioLoop = gameObject.AddComponent<AudioSource>();
            fuenteAudioLoop.loop = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReproducirSonido(AudioClip clip)
    {
        if (clip != null)
        {
            fuenteAudio.PlayOneShot(clip, configuracionAudio.volumenGlobal);
        }
    }

    public void ReproducirSonidoEnLoop(AudioClip clip)
    {
        if (clip != null && fuenteAudioLoop != null)
        {
            fuenteAudioLoop.clip = clip;
            fuenteAudioLoop.volume = configuracionAudio.volumenGlobal;
            fuenteAudioLoop.Play();
        }
    }

    public void DetenerSonidoEnLoop()
    {
        if (fuenteAudioLoop != null && fuenteAudioLoop.isPlaying)
        {
            fuenteAudioLoop.Stop();
        }
    }


    //===============================================================================================//
    //                                      SONIDOS JUGADOR                                          //
    //===============================================================================================//

    public void ReproducirSonidoDanyoJugador()
    {
        AudioClip clip = configuracionAudio.sonido;
        ReproducirSonido(clip);
    }

}

//=======================================================================================================================================//
//=======================================================================================================================================//
//=======================================================================================================================================//
//=======================================================================================================================================//
