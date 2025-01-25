using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // Nombre de la escena a cargar (puedes configurarlo desde el Inspector)
    [SerializeField] private GameObject panelCerrar; // Panel que se muestra antes de cerrar el juego

    // Método público para cargar una escena asignada
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("El nombre de la escena no está asignado o está vacío.");
        }
    }

    // Método público para cerrar el juego
    public void CloseGame()
    {
        if (panelCerrar != null)
        {
            panelCerrar.SetActive(true); // Muestra el panel
            StartCoroutine(CloseGameAfterDelay(0.03f)); // Inicia la corrutina para cerrar el juego después de un retraso
        }
        else
        {
            Debug.LogError("PanelCerrar no está asignado en el Inspector.");
        }
    }

    // Corrutina para manejar el retraso antes de cerrar el juego
    private System.Collections.IEnumerator CloseGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        Application.Quit(); // Cierra el juego
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo para el modo Editor de Unity
#endif
    }
}
