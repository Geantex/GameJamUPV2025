using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnKeyPress : MonoBehaviour
{
    // Nombre de la escena a cargar
    public string sceneName = "MapaBellus";
    public AdministradorAudio administradorAudio;

    void Update()
    {
        // Comprobar si se presiona la tecla F3
        if (Input.GetKeyDown(KeyCode.F3))
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Verificar si la escena existe (opcional pero útil en desarrollo)
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            // Detener el audio antes de cambiar de escena
            if (administradorAudio != null)
            {
                administradorAudio.DetenerAudio();
            }

            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"La escena '{sceneName}' no existe o no está incluida en la Build Settings.");
        }
    }
}
