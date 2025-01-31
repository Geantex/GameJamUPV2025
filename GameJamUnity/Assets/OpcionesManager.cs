using UnityEngine;
using UnityEngine.UI;
using TMPro; // Para usar Dropdowns

public class OpcionesManager : MonoBehaviour
{
    [Header("Ajustes de Audio")]
    public Slider volumenEfectosSlider;
    public Slider volumenMusicaSlider;

    [Header("Ajustes de Sensibilidad")]
    public Slider sensibilidadSlider;

    [Header("Ajustes de Dificultad")]
    public TMP_Dropdown dificultadDropdown;

    void Start()
    {
        // Cargar valores guardados o usar valores por defecto
        volumenEfectosSlider.value = PlayerPrefs.GetFloat("VolumenEfectos", 1.0f);
        volumenMusicaSlider.value = PlayerPrefs.GetFloat("VolumenMusica", 1.0f);
        sensibilidadSlider.value = PlayerPrefs.GetFloat("Sensibilidad", 1.0f);
        dificultadDropdown.value = PlayerPrefs.GetInt("Dificultad", 1); // Por defecto: Normal

        // Aplicar configuraciones al juego
        AplicarConfiguraciones();
    }

    //  Volumen de efectos
    public void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("VolumenEfectos", valor);
        PlayerPrefs.Save();
        AplicarConfiguraciones();
    }

    //  Volumen de música
    public void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("VolumenMusica", valor);
        PlayerPrefs.Save();
        AplicarConfiguraciones();
    }

    //  Sensibilidad del ratón
    public void CambiarSensibilidad(float valor)
    {
        PlayerPrefs.SetFloat("Sensibilidad", valor);
        PlayerPrefs.Save();
        AplicarConfiguraciones();
    }

    //  Dificultad (0 = Fácil, 1 = Normal, 2 = Difícil)
    public void CambiarDificultad(int valor)
    {
        PlayerPrefs.SetInt("Dificultad", valor);
        PlayerPrefs.Save();
        AplicarConfiguraciones();
    }

    void AplicarConfiguraciones()
    {
        // Aplicar el volumen de efectos y música (ajusta esto según tu sistema de audio)
        //AudioListener.volume = PlayerPrefs.GetFloat("VolumenEfectos");

        // Aquí podrías ajustar la sensibilidad en el sistema de control del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Cargar sensibilidad guardada y aplicarla como multiplicador
            float sensibilidadGuardada = PlayerPrefs.GetFloat("Sensibilidad", 1.0f);
            player.GetComponent<playerController>().mouseSensitivity *= sensibilidadGuardada;
        }
        // También aplicar la dificultad si afecta a la IA, daño enemigo, etc.
    }
}
