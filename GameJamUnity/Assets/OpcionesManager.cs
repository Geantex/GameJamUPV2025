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
        Debug.Log("Volumen de efectos actualizado a: " + valor);
    }

    // Volumen de música
    public void CambiarVolumenMusica(float valor)
    {
        PlayerPrefs.SetFloat("VolumenMusica", valor);
        PlayerPrefs.Save();
        Debug.Log("Volumen de música actualizado a: " + valor);
    }

    // Sensibilidad del ratón
    public void CambiarSensibilidad(float valor)
    {
        PlayerPrefs.SetFloat("Sensibilidad", valor);
        PlayerPrefs.Save();
        Debug.Log("Sensibilidad del ratón actualizada a: " + valor);

        AplicarConfiguraciones();
    }

    // Dificultad (0 = Fácil, 1 = Normal, 2 = Difícil)
    public void CambiarDificultad(int valor)
    {
        PlayerPrefs.SetInt("Dificultad", valor);
        PlayerPrefs.Save();
        Debug.Log("Dificultad actualizada a: " + (valor == 0 ? "Fácil" : valor == 1 ? "Normal" : "Difícil"));
    }

    void AplicarConfiguraciones()
    {
        // Ajustar la sensibilidad en el sistema de control del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float sensibilidadGuardada = PlayerPrefs.GetFloat("Sensibilidad", 1.0f);
            player.GetComponent<playerController>().mouseSensitivity *= sensibilidadGuardada;
            Debug.Log("Sensibilidad aplicada al jugador: " + player.GetComponent<playerController>().mouseSensitivity);
        }
    }
}
