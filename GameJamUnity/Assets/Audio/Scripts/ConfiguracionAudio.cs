//=======================================================================================================================================//
//=======================================================================================================================================//
//  Script:
//  ConfiguracionAudio
//
//  Descripci�n:
//  Este ScriptableObject almacena configuraciones globales de sonidos para el juego.
//
//=======================================================================================================================================//
//=======================================================================================================================================//

using UnityEngine;

//=======================================================================================================================================//
//=======================================================================================================================================//

[CreateAssetMenu(fileName = "ConfiguracionAudio", menuName = "Audio/Configuraci�n")]
public class ConfiguracionAudio : ScriptableObject
{
    [Header("Configuraci�n Adicional")]
    [Tooltip("Volumen global que se aplicar� al AudioSource.")]
    [Range(0f, 1f)] public float volumenGlobal = 1f;

  
    //=========================================================================//

    //Crear mas para cada sonido

    [Header("Sonidos")]
    public AudioClip sonido;
    public AudioClip charlaTortosa;
    public AudioClip idol;

    public AudioClip tortosa1;
    public AudioClip tortosa2;
    public AudioClip tortosa3;

    public AudioClip burbuja;
    public AudioClip laser;

    public AudioClip danyoJugador;
    public AudioClip danyoEnemigo;
    public AudioClip muerteJugador;
    public AudioClip muerteEnemigo;

    public AudioClip construccion;










}
