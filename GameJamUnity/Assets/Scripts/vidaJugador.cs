using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class VidaJugador : MonoBehaviour
{
    // Vida máxima del jugador
    private const int vidaMaxima = 100;
    // Vida actual del jugador
    private int vidaActual;

    // Referencia al texto del Canvas
    public Text vidaText;

    void Start()
    {
        // Inicializar la vida del jugador
        vidaActual = vidaMaxima;

        // Actualizar el texto en el Canvas
        ActualizarTextoVida();
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

        // Actualizar el texto en el Canvas
        ActualizarTextoVida();

        // Verificar si el jugador ha muerto
        if (vidaActual <= 0)
        {
            MuerteJugador();
        }
    }

    private void ActualizarTextoVida()
    {
        // Actualizar el texto en el Canvas con la vida actual
        vidaText.text = "Vida: " + vidaActual;
    }

    private void MuerteJugador()
    {
        // Mostrar mensaje en consola al morir
        Debug.Log("El jugador ha muerto.");

        // Puedes añadir lógica adicional aquí, como reiniciar el nivel o mostrar un menú de derrota
        // Cambiar a la escena de muerte
        SceneManager.LoadScene("muerte");
    }
}
