using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        if (jugador == null)
        {
            Debug.LogError("El jugador no está asignado en MovimientoEnemigo.");
        }
    }

    void Update()
    {
        if (jugador != null)
        {
            agente.SetDestination(jugador.position);
        }
    }
}
