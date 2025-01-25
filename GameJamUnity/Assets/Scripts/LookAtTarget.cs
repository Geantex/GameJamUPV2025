using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // El objeto que queremos mirar
    public Transform target;

    // Ajuste opcional para suavizar la rotaci�n
    public float rotationSpeed = 5f;

    void Update()
    {
        if (target != null)
        {
            // Calcula la direcci�n hacia el objetivo
            Vector3 direction = target.position - transform.position;

            // Obtiene la rotaci�n deseada hacia el objetivo
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Suaviza la rotaci�n
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
