using System.Collections;
using UnityEngine;
using TMPro;

public class OleadaManager : MonoBehaviour
{
    // Fijamos 6 oleadas con la cantidad de enemigos exacta en cada una.
    private int[] enemigosPorOleada = { 5, 10, 20, 30, 40, 50 };

    public float tiempoEntreOleadas = 10f; // Tiempo de espera entre oleadas.
    public Transform jugador;

    // Referencias a los textos de la UI
    public TextMeshProUGUI textoOleada;    // Texto para mostrar la oleada actual
    public TextMeshProUGUI textoTiempo;   // Texto para mostrar el tiempo restante (solo entre oleadas)

    private int enemigosRestantes;        // N�mero de enemigos vivos en la oleada actual.
    private RespawnManager respawnManager;
    private bool oleadaEnCurso = false;   // Bandera para saber si la oleada est� activa.

    void Start()
    {
        respawnManager = GetComponent<RespawnManager>();

        if (jugador == null)
        {
            jugador = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Aseguramos que el texto del tiempo est� inicialmente desactivado.
        if (textoTiempo != null)
        {
            textoTiempo.gameObject.SetActive(false);
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
            int numeroOleada = i + 1;               // Para mostrar un n�mero de oleada a partir de 1
            int enemigosEstaOleada = enemigosPorOleada[i];

            Debug.Log($"Oleada {numeroOleada} comenzando...");
            oleadaEnCurso = true;

            // Actualiza el texto de la oleada
            ActualizarTextoOleada(numeroOleada);

            // Aseguramos que el texto del tiempo no est� visible durante la oleada.
            if (textoTiempo != null)
            {
                textoTiempo.gameObject.SetActive(false);
            }

            enemigosRestantes = enemigosEstaOleada;

            // Iniciamos el spawn de la oleada
            StartCoroutine(respawnManager.SpawnOleada(enemigosEstaOleada, jugador, ActualizarEnemigosRestantes));

            // Esperamos hasta que todos los enemigos de la oleada actual mueran.
            while (enemigosRestantes > 0)
            {
                yield return null;
            }

            oleadaEnCurso = false;
            Debug.Log($"Oleada {numeroOleada} completada.");

            // Si no es la �ltima oleada, mostramos el contador y esperamos.
            if (numeroOleada < enemigosPorOleada.Length)
            {
                yield return StartCoroutine(EsperarConContador(tiempoEntreOleadas));
            }
        }

        Debug.Log("�Todas las oleadas completadas!");
    }

    // Se llama cada vez que muere un enemigo (normalmente con cambio = -1).
    public void ActualizarEnemigosRestantes(int cambio)
    {
        int enemigosPrevios = enemigosRestantes;
        enemigosRestantes += cambio;

        // Aviso final de oleada completada solo si la oleada est� en curso y hab�a enemigos.
        if (enemigosRestantes <= 0 && enemigosPrevios > 0 && oleadaEnCurso)
        {
            Debug.Log("�Oleada completada!");
        }
    }

    // Espera un tiempo mostrando un contador en la consola y actualizando el texto de tiempo.
    private IEnumerator EsperarConContador(float tiempoEspera)
    {
        float tiempoRestante = tiempoEspera;

        // Activamos el texto de tiempo.
        if (textoTiempo != null)
        {
            textoTiempo.gameObject.SetActive(true);
        }

        while (tiempoRestante > 0)
        {
            ActualizarTextoTiempo(tiempoRestante); // Actualiza el texto de tiempo restante
            //Debug.Log($"Tiempo restante para la pr�xima oleada: {tiempoRestante:F1} segundos");
            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        ActualizarTextoTiempo(0); // Aseg�rate de mostrar "0" cuando termine el contador

        // Ocultamos el texto del tiempo despu�s de la espera.
        if (textoTiempo != null)
        {
            textoTiempo.gameObject.SetActive(false);
        }

        Debug.Log("�Nueva oleada comenzando!");
    }

    // Actualiza el texto de la oleada actual
    private void ActualizarTextoOleada(int numeroOleada)
    {
        if (textoOleada != null)
        {
            textoOleada.text = $"Oleada: {numeroOleada}";
        }
    }

    // Actualiza el texto del tiempo restante
    private void ActualizarTextoTiempo(float tiempoRestante)
    {
        if (textoTiempo != null)
        {
            textoTiempo.text = $"Tiempo para pr�xima oleada: {tiempoRestante:F1} s";
        }
    }
}
