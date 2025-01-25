using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // El objeto que queremos mirar
    public Transform target;

    // Ajuste opcional para suavizar la rotación
    public float rotationSpeed = 5f;

    void Update()
    {
        if (target != null)
        {
            // Calcula la dirección hacia el objetivo
            Vector3 direction = target.position - transform.position;

            // Obtiene la rotación deseada hacia el objetivo
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Suaviza la rotación
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
