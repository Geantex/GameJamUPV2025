using System.Collections;
using UnityEngine;

public class OleadaManager : MonoBehaviour
{
    // Fijamos 6 oleadas con la cantidad de enemigos exacta en cada una.
    private int[] enemigosPorOleada = { 5, 10, 20, 30, 40, 50 };

    public float tiempoEntreOleadas = 10f; // Tiempo de espera entre oleadas.
    public Transform jugador;

    private int enemigosRestantes;        // Número de enemigos vivos en la oleada actual.
    private RespawnManager respawnManager;
    private bool oleadaEnCurso = false;   // Bandera para saber si la oleada está activa.

    void Start()
    {
        respawnManager = GetComponent<RespawnManager>();

        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Iniciamos la secuencia de oleadas.
        StartCoroutine(IniciarOleadas());
    }

    private void Update()
    {
        // Sincroniza el contador con los enemigos activos, por si surge desajuste.
        int enemigosActivos = respawnManager.ObtenerEnemigosActivos();
        if (enemigosRestantes != enemigosActivos)
        {
            Debug.LogWarning("El contador de enemigos restantes estaba desincronizado. Ajustando...");
            enemigosRestantes = enemigosActivos;
        }
    }

    private IEnumerator IniciarOleadas()
    {
        // Recorremos las 6 oleadas definidas.
        for (int i = 0; i < enemigosPorOleada.Length; i++)
        {
            int numeroOleada = i + 1;               // Para mostrar un número de oleada a partir de 1
            int enemigosEstaOleada = enemigosPorOleada[i];

            Debug.Log($"Oleada {numeroOleada} comenzando...");
            oleadaEnCurso = true;

            enemigosRestantes = enemigosEstaOleada;

            // Iniciamos el spawn de la oleada; 
            // RespawnManager.SpawnOleada deberá instanciar tantos enemigos como 'enemigosEstaOleada'
            // y llamar a 'ActualizarEnemigosRestantes(-1)' cada vez que muera un enemigo.
            StartCoroutine(respawnManager.SpawnOleada(enemigosEstaOleada, jugador, ActualizarEnemigosRestantes));

            // Esperamos hasta que todos los enemigos de la oleada actual mueran.
            while (enemigosRestantes > 0)
            {
                yield return null;
            }

            oleadaEnCurso = false;
            Debug.Log($"Oleada {numeroOleada} completada.");

            // Si no es la última oleada, esperamos un tiempo antes de iniciar la siguiente.
            if (numeroOleada < enemigosPorOleada.Length)
            {
                yield return StartCoroutine(EsperarConContador(tiempoEntreOleadas));
            }
        }

        Debug.Log("¡Todas las oleadas completadas!");
    }

    // Se llama cada vez que muere un enemigo (normalmente con cambio = -1).
    public void ActualizarEnemigosRestantes(int cambio)
    {
        int enemigosPrevios = enemigosRestantes;
        enemigosRestantes += cambio;

        // Aviso final de oleada completada solo si la oleada está en curso y había enemigos.
        if (enemigosRestantes <= 0 && enemigosPrevios > 0 && oleadaEnCurso)
        {
            Debug.Log("¡Oleada completada!");
        }
    }

    // Espera un tiempo mostrando un contador en la consola.
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
