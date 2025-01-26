//=======================================================================================================================================//
//=======================================================================================================================================//
//  Script:
//  ConfiguracionAudio
//
//  Descripción:
//  Este ScriptableObject almacena configuraciones globales de sonidos para el juego.
//
//=======================================================================================================================================//
//=======================================================================================================================================//

using UnityEngine;

//=======================================================================================================================================//
//=======================================================================================================================================//

[CreateAssetMenu(fileName = "ConfiguracionAudio", menuName = "Audio/Configuración")]
public class ConfiguracionAudio : ScriptableObject
{
    [Header("Configuración Adicional")]
    [Tooltip("Volumen global que se aplicará al AudioSource.")]
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
    public AudioClip muerteEnemigo1;
    public AudioClip muerteEnemigo2;
    public AudioClip muerteEnemigo3;
    public AudioClip muerteEnemigo4;

    public AudioClip construccion;

    public AudioClip aparicionEnemigo1;
    public AudioClip aparicionEnemigo2;
    public AudioClip aparicionEnemigo3;
    public AudioClip aparicionEnemigo4;










}
