using UnityEngine;

public class MuerteController : MonoBehaviour
{
    private void Start()
    {
        // Aseg�rate de que el cursor sea visible y est� desbloqueado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
