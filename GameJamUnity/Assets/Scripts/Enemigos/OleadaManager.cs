using System.Collections;
using UnityEngine;

public class OleadaManager : MonoBehaviour
{
    // Puedes eliminar estas dos variables si ya no quieres generar aleatoriamente
    // public int enemigosMinPorOleada = 5;
    // public int enemigosMaxPorOleada = 15;

    // Ahora definimos un arreglo con el número de enemigos que tendrá cada oleada.
    // En este ejemplo tenemos 10 oleadas, pero puedes personalizarlo a tu gusto.
    private int[] enemigosPorOleada = { 5, 8, 12, 16, 20, 25, 30, 35, 40, 50 };

    public float tiempoEntreOleadas = 10f; // Tiempo de espera entre oleadas
    public Transform jugador;

    private int enemigosRestantes; // Número de enemigos vivos
    private RespawnManager respawnManager;
    private bool oleadaEnCurso = false; // Bandera para controlar el estado de la oleada

    void Start()
    {
        respawnManager = GetComponent<RespawnManager>();

        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(IniciarOleadas());
    }

    private void Update()
    {
        // Sincroniza el contador con los enemigos activos en caso de desincronización
        int enemigosActivos = respawnManager.ObtenerEnemigosActivos();
        if (enemigosRestantes != enemigosActivos)
        {
            Debug.LogWarning("El contador de enemigos restantes estaba desincronizado. Ajustando...");
            enemigosRestantes = enemigosActivos;
        }
    }

    private IEnumerator IniciarOleadas()
    {
        // Recorremos el arreglo de enemigosPorOleada para lanzar cada oleada
        for (int i = 0; i < enemigosPorOleada.Length; i++)
        {
            int numeroOleada = i + 1; // Para mostrar un número de oleada a partir de 1
            int enemigosEstaOleada = enemigosPorOleada[i];

            Debug.Log($"Oleada {numeroOleada} comenzando...");
            oleadaEnCurso = true;

            enemigosRestantes = enemigosEstaOleada;

            // Lanzamos la oleada
            StartCoroutine(respawnManager.SpawnOleada(enemigosEstaOleada, jugador, ActualizarEnemigosRestantes));

            // Esperamos a que todos los enemigos de la oleada mueran
            while (enemigosRestantes > 0)
            {
                yield return null;
            }

            oleadaEnCurso = false;
            Debug.Log($"Oleada {numeroOleada} completada.");

            // Si no es la última oleada, esperamos antes de lanzar la siguiente
            if (numeroOleada < enemigosPorOleada.Length)
            {
                yield return StartCoroutine(EsperarConContador(tiempoEntreOleadas));
            }
        }

        Debug.Log("¡Todas las oleadas completadas!");
    }

    public void ActualizarEnemigosRestantes(int cambio)
    {
        int enemigosPrevios = enemigosRestantes;
        enemigosRestantes += cambio;

        // Solo mostramos "¡Oleada completada!" si la oleada estaba en curso y había enemigos
        if (enemigosRestantes <= 0 && enemigosPrevios > 0 && oleadaEnCurso)
        {
            Debug.Log("¡Oleada completada!");
        }
    }

    // Método para esperar mostrando un contador en el log
    private IEnumerator EsperarConContador(float tiempoEspera)
    {
        float tiempoRestante = tiempoEspera;

        while (tiempoRestante > 0)
        {
            Debug.Log($"Tiempo restante para la próxima oleada: {tiempoRestante:F1} segundos");
            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        Debug.Log("¡Nueva oleada comenzando!");
    }
}
