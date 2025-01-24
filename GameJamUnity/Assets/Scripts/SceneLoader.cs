using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // Nombre de la escena a cargar (puedes configurarlo desde el Inspector)

    // Método público para ser asignado al botón
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
}
