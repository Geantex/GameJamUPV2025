using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    // Vida m�xima del jugador
    private const int vidaMaxima = 100;
    // Vida actual del jugador
    private int vidaActual;

    void Start()
    {
        // Inicializar la vida del jugador
        vidaActual = vidaMaxima;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene la etiqueta "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Reducir la vida del jugador
            RestarVida(20);
        }
    }

    private void RestarVida(int cantidad)
    {
        // Reducir la vida y asegurarse de que no sea menor a 0
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);

        Debug.Log("Vida actual del jugador: " + vidaActual);

        // Verificar si el jugador ha muerto
        if (vidaActual <= 0)
        {
            MuerteJugador();
        }
    }

    private void MuerteJugador()
    {
        // Mostrar mensaje en consola al morir
        Debug.Log("El jugador ha muerto.");

        // Puedes a�adir l�gica adicional aqu�, como reiniciar el nivel o mostrar un men� de derrota
    }
}
