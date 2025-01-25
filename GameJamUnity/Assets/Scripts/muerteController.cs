using UnityEngine;

public class MuerteController : MonoBehaviour
{
    private void Start()
    {
        // Asegúrate de que el cursor sea visible y esté desbloqueado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
