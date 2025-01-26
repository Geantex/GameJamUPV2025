using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    // GameObjects que definen los puntos A y B
    public Transform pointA;
    public Transform pointB;

    // Velocidad de movimiento
    public float speed = 5f;

    void Update()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA o PointB no están asignados en el script.");
            return;
        }

        // Mover el objeto hacia el punto B
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);

        // Comprobar si ha llegado al punto B
        if (Vector3.Distance(transform.position, pointB.position) < 0.01f)
        {
            // Detener el movimiento
            enabled = false;
        }
    }
}
