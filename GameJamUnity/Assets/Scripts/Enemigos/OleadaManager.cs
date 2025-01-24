using System.Collections;
using UnityEngine;

public class OleadaManager : MonoBehaviour
{
    public int numeroOleadas = 5;
    public int enemigosMinPorOleada = 5;
    public int enemigosMaxPorOleada = 15;
    public float tiempoEntreOleadas = 10f; // Tiempo entre oleadas
    public Transform jugador;

    private int enemigosRestantes; // Número de enemigos vivos
    private RespawnManager respawnManager;
    private bool oleadaEnCurso = false; // Bandera para evitar mensajes no deseados

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
        // Sincronización del contador con los enemigos activos en caso de desincronización
        int enemigosActivos = respawnManager.ObtenerEnemigosActivos();
        if (enemigosRestantes != enemigosActivos)
        {
            Debug.LogWarning("El contador de enemigos restantes estaba desincronizado. Ajustando...");
            enemigosRestantes = enemigosActivos;
        }
    }

    private IEnumerator IniciarOleadas()
    {
        for (int i = 1; i <= numeroOleadas; i++)
        {
            Debug.Log($"Oleada {i} comenzando...");
            oleadaEnCurso = true; // Marca que la oleada está en curso

            int enemigosPorOleada = Random.Range(enemigosMinPorOleada, enemigosMaxPorOleada + 1);
            enemigosRestantes = enemigosPorOleada;

            StartCoroutine(respawnManager.SpawnOleada(enemigosPorOleada, jugador, ActualizarEnemigosRestantes));

            while (enemigosRestantes > 0)
            {
                yield return null; // Espera hasta que todos los enemigos sean eliminados
            }

            oleadaEnCurso = false; // Marca que la oleada ha terminado
            Debug.Log($"Oleada {i} completada.");
            yield return StartCoroutine(EsperarConContador(tiempoEntreOleadas)); // Espera entre oleadas con un contador
        }

        Debug.Log("¡Todas las oleadas completadas!");
    }

    public void ActualizarEnemigosRestantes(int cambio)
    {
        int enemigosPrevios = enemigosRestantes;
        enemigosRestantes += cambio;

        // Solo muestra "¡Oleada completada!" si la oleada realmente estaba en curso y había enemigos
        if (enemigosRestantes <= 0 && enemigosPrevios > 0 && oleadaEnCurso)
        {
            Debug.Log("¡Oleada completada!");
        }
    }

    // Método para esperar y mostrar un contador en el log
    private IEnumerator EsperarConContador(float tiempoEspera)
    {
        float tiempoRestante = tiempoEspera;

        while (tiempoRestante > 0)
        {
            Debug.Log($"Tiempo restante para la próxima oleada: {tiempoRestante:F1} segundos");
            yield return new WaitForSeconds(1f); // Actualiza cada segundo
            tiempoRestante -= 1f;
        }

        Debug.Log("¡Nueva oleada comenzando!");
    }
}
