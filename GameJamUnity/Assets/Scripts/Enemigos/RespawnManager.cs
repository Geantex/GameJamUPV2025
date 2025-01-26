using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RespawnManager : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float distanciaMinimaJugador = 10f;
    public float distanciaMaximaJugador = 30f;
    public int enemigosCercanosMax = 5;


    private List<GameObject> enemigosActivos = new List<GameObject>();

    public IEnumerator SpawnOleada(int cantidadEnemigos, Transform jugador, System.Action<int> callbackEnemigos)
    {
        int spawnCount = 0;

        while (spawnCount < cantidadEnemigos)
        {
            if (CantidadEnemigosCercanos(jugador) < enemigosCercanosMax)
            {
                Vector3 spawnPos = GenerarPosicionSpawn(jugador.position);
                GameObject enemigo = Instantiate(enemigoPrefab, spawnPos, Quaternion.identity);
                

                if (enemigo != null)
                {
                    MovimientoEnemigo enemigoMovimiento = enemigo.GetComponent<MovimientoEnemigo>();
                    if (enemigoMovimiento != null)
                    {
                        enemigoMovimiento.jugador = jugador;
                    }
                    else
                    {
                        Debug.LogError("El prefab enemigo no tiene el script 'MovimientoEnemigo' adjunto.");
                    }

                    enemigosActivos.Add(enemigo);

                    // Registrar el evento de muerte del enemigo
                    Enemigo enemigoScript = enemigo.GetComponent<Enemigo>();
                    if (enemigoScript != null)
                    {
                        enemigoScript.OnMuerte += () =>
                        {
                            if (enemigosActivos.Contains(enemigo))
                            {
                                enemigosActivos.Remove(enemigo);
                                callbackEnemigos?.Invoke(-1); // Reducir el contador de enemigos restantes
                            }
                        };
                    }
                    else
                    {
                        Debug.LogError("El prefab enemigo no tiene el script 'Enemigo' adjunto.");
                    }

                    // Notificar que se ha generado un nuevo enemigo
                    callbackEnemigos?.Invoke(1);
                }
                else
                {
                    Debug.LogError("No se pudo instanciar el enemigo. Revisa el prefab asignado.");
                }

                spawnCount++;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public int ObtenerEnemigosActivos()
    {
        return enemigosActivos.Count;
    }

    private Vector3 GenerarPosicionSpawn(Vector3 posicionJugador)
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        float distancia = Random.Range(distanciaMinimaJugador, distanciaMaximaJugador);
        Vector3 spawnPos = posicionJugador + new Vector3(randomDir.x, 0, randomDir.y) * distancia;

        // Asegurar que la posici�n est� en el nivel del suelo
        spawnPos.y = 0;
        return spawnPos;
    }

    private int CantidadEnemigosCercanos(Transform jugador)
    {
        int count = 0;
        foreach (var enemigo in enemigosActivos)
        {
            if (enemigo == null)
            {
                
            }
            else if (Vector3.Distance(jugador.position, enemigo.transform.position) <= distanciaMaximaJugador)
            {
                count++;
            }
        }
        return count;
    }
}
