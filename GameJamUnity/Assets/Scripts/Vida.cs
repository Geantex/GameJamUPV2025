using System.Collections;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Configuraci�n de Vida")]
    public int vidaInicial = 100; // Vida inicial del enemigo
    [SerializeField]
    private int vidaActual;
    //[Header("Configuración Sonidos")]
    //[SerializeField] public AdministradorAudio administradorAudio;

    void Start()
    {
        //administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
        // Inicializa la vida actual con la vida inicial
        vidaActual = vidaInicial;
    }

    /// <summary>
    /// Reduce la vida del enemigo en la cantidad especificada.
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a reducir.</param>
    public void QuitarVida(int cantidad, GameObject efectoDeMuerte)
    {
        vidaActual -= cantidad;
        //Debug.Log($"Enemigo {gameObject.name}: Vida actual = {vidaActual}");

        // Si la vida llega a 0 o menos, matar al enemigo
        if (vidaActual <= 0)
        {
            MatarEnemigo(efectoDeMuerte);
        }
    }

    public int MirarVida(){
        return vidaActual;
    }

    /// <summary>
    /// Destruye al enemigo.
    /// </summary>
    private void MatarEnemigo(GameObject efectoDeMuerte)
    {
        //Debug.Log($"Enemigo {gameObject.name} ha muerto.");
        // Ajustar la rotación para que siempre apunte hacia arriba
        Quaternion rotacionHaciaArriba = Quaternion.LookRotation(Vector3.up);
        GameObject instanciaEfectoDeMuerte = Instantiate(efectoDeMuerte, transform.position, rotacionHaciaArriba);
        /*if (administradorAudio != null)
        {
            administradorAudio.ReproducirSonidoExplosionMuerte();
        }*/
        Destroy(gameObject);
        Destroy(instanciaEfectoDeMuerte, 3f);

    }

}
