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
    public AudioSource musicaAudio;

    //----------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------- M�TODOS ------------------------------------------------------------//

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log("Burbujeante: Administrador de audio creado");
            // Obtener la fuente de audio principal
            
            // aplicar playerprefs
            fuenteAudio.volume = PlayerPrefs.GetFloat("VolumenEfectos", 1.0f);
            Debug.Log("Volumen efectos: " + fuenteAudio.volume);

            // Obtener la fuente de m�sica (que esta en el GameManager en vez del AudioManager)
            
            // aplicar playerprefs
            musicaAudio.volume = PlayerPrefs.GetFloat("VolumenMusica", 1.0f);
            Debug.Log("Volumen musica: " + musicaAudio.volume);



            // Crear una fuente de audio adicional para sonidos en bucle
            fuenteAudioLoop = gameObject.AddComponent<AudioSource>();
            fuenteAudioLoop.volume = PlayerPrefs.GetFloat("VolumenEfectos", 1.0f);
            Debug.Log("Volumen efectosLoop: " + fuenteAudioLoop.volume);
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
            // randomizar pitch para que no suene igual siempre de 90% a 110%
            fuenteAudio.pitch = Random.Range(0.9f, 1.1f);
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

    public void DetenerAudio()
    {
        // Detener tanto los sonidos normales como los sonidos en bucle
        if (fuenteAudio.isPlaying)
        {
            fuenteAudio.Stop();
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
    public void ReproducirSonidoTortosaInicial()
    {
        AudioClip clip = configuracionAudio.charlaTortosa;
        ReproducirSonido(clip);
    }

    public void ReproducirSuperIdol()
    {
        AudioClip clip = configuracionAudio.idol;
        ReproducirSonido(clip);
    }

    public void ReproducirSonidoDisparoBurbuja()
    {
        AudioClip clip = configuracionAudio.burbuja;
        ReproducirSonido(clip);
    }

    public void ReproducirSonidoDisparoLaser()
    {
        AudioClip clip = configuracionAudio.laser;
        ReproducirSonidoEnLoop(clip);
    }
    public void DetenerSonidoDisparoLaser()
    {
        DetenerSonidoEnLoop();
    }

    public void ReproducirSonidoConstruccion()
    {
        AudioClip clip = configuracionAudio.construccion;
        ReproducirSonido(clip);
    }


    public void ReproducirSonidoRandomTortosa()
    {
        // Crear un arreglo de AudioClip con los sonidos disponibles
        AudioClip[] clips = new AudioClip[]
        {
        configuracionAudio.tortosa1,
        configuracionAudio.tortosa2,
        configuracionAudio.tortosa3
        };

        // Seleccionar un sonido aleatorio
        int indexRandom = Random.Range(0, clips.Length);
        AudioClip clipSeleccionado = clips[indexRandom];

        // Reproducir el sonido seleccionado
        ReproducirSonido(clipSeleccionado);
    }

    public void ReproducirSonidoRandomMuerteEnemigos()
    {
        // Crear un arreglo de AudioClip con los sonidos disponibles
        AudioClip[] clips = new AudioClip[]
        {
        configuracionAudio.muerteEnemigo1,
        configuracionAudio.muerteEnemigo2,
        configuracionAudio.muerteEnemigo3,
        configuracionAudio.muerteEnemigo4
        };

        // Seleccionar un sonido aleatorio
        int indexRandom = Random.Range(0, clips.Length);
        AudioClip clipSeleccionado = clips[indexRandom];

        // Reproducir el sonido seleccionado
        ReproducirSonido(clipSeleccionado);
    }

    public void ReproducirSonidoRandomAparicionEnemigos()
    {
        // Crear un arreglo de AudioClip con los sonidos disponibles
        AudioClip[] clips = new AudioClip[]
        {
        configuracionAudio.aparicionEnemigo1,
        configuracionAudio.aparicionEnemigo2,
        configuracionAudio.aparicionEnemigo3,
        configuracionAudio.aparicionEnemigo4
        };

        // Seleccionar un sonido aleatorio
        int indexRandom = Random.Range(0, clips.Length);
        AudioClip clipSeleccionado = clips[indexRandom];

        // Reproducir el sonido seleccionado
        ReproducirSonido(clipSeleccionado);
    }


}

//=======================================================================================================================================//
//=======================================================================================================================================//
//=======================================================================================================================================//
//=======================================================================================================================================//
