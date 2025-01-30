using UnityEngine;
using TMPro; // Necesario para TextMeshPro
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class VidaJugador : MonoBehaviour
{
    // Vida m�xima del jugador
    private int vidaMaxima = 100;
    // Vida actual del jugador
    private int vidaActual;

    // Referencia al texto del Canvas
    public TMP_Text vidaText;

    public AdministradorAudio administradorAudio;

  

    void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
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
            RestarVida(collision.gameObject.GetComponent<Enemigo>().damage);
        }
    }

    public void RestarVida(int cantidad)
    {
        // Reducir la vida y asegurarse de que no sea menor a 0
        vidaActual -= cantidad;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);


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
        if (vidaText != null)
        {
            vidaText.text = "Vida: " + vidaActual;
        }
    }

    private void MuerteJugador()
    {
        // Mostrar mensaje en consola al morir
        administradorAudio.ReproducirSonidoRandomMuerteEnemigos();
        Debug.Log("El jugador ha muerto.");

        // Puedes a�adir l�gica adicional aqu�, como reiniciar el nivel o mostrar un men� de derrota
        // Cambiar a la escena de muerte
        SceneManager.LoadScene("muerte");
    }

    public void MejoraVida(){
        vidaMaxima += 100;
        vidaActual = vidaMaxima;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarTextoVida();
    }
}
