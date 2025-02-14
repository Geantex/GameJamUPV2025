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

        if(dificultadDropdown != null)
        dificultadDropdown.value = PlayerPrefs.GetInt("Dificultad", 1); // Por defecto: Normal

        if(dificultadDropdown != null)
        Debug.Log("Valores cargados: " +
                  "\nVolumen Efectos: " + volumenEfectosSlider.value +
                  "\nVolumen Música: " + volumenMusicaSlider.value +
                  "\nSensibilidad: " + sensibilidadSlider.value +
                  "\nDificultad: " + dificultadDropdown.value);

        AplicarConfiguraciones();
    }

    // Volumen de efectos
    public void CambiarVolumenEfectos(float valor)
    {
        PlayerPrefs.SetFloat("VolumenEfectos", valor);
        PlayerPrefs.Save();
        // Debug.Log("Volumen de efectos actualizado a: " + valor);
        AplicarConfiguraciones();
    }

    // Volumen de música
    public void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("VolumenMusica", valor);
        PlayerPrefs.Save();
        // Debug.Log("Volumen de música actualizado a: " + valor);
        AplicarConfiguraciones();
    }

    // Sensibilidad del ratón
    public void CambiarSensibilidad(float valor)
    {
        PlayerPrefs.SetFloat("Sensibilidad", valor);
        PlayerPrefs.Save();
        // Debug.Log("Sensibilidad del ratón actualizada a: " + valor);

        AplicarConfiguraciones();
    }

    // Dificultad (0 = Fácil, 1 = Normal, 2 = Difícil)
    public void CambiarDificultad(int valor)
    {
        PlayerPrefs.SetInt("Dificultad", valor);
        PlayerPrefs.Save();
        // Debug.Log("Dificultad actualizada a: " + (valor == 0 ? "Fácil" : valor == 1 ? "Normal" : "Difícil"));
    }

    void AplicarConfiguraciones()
    {
        // Ajustar la sensibilidad en el sistema de control del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<playerController>().ActualizarSensibilidad();
            // Debug.Log("Sensibilidad aplicada al jugador: " + player.GetComponent<playerController>().mouseSensitivity);
        }
        try
        {
            GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>().ActualizarVolumenGeneral();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("No se encontró el administrador de audio. Posiblemente estamos en la escena de inicio.");
        }
    }
}
