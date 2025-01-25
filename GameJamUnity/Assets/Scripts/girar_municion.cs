using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girar_municion : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad normal de rotaci�n
    public float boostedSpeed = 150f; // Velocidad aumentada
    public float boostDuration = 2f; // Duraci�n del aumento de velocidad en segundos
    [SerializeField] ParticleSystem bubbleParticles; // Part�culas
    [SerializeField] GameObject cilindroGiratorio;
    public float emissionRate = 2f; // Ratio normal de emisi�n
    public float boostedEmissionRate = 100f; // Ratio de emisi�n durante el boost

    private float currentSpeed; // Velocidad actual de rotaci�n

    private ParticleSystem.EmissionModule emissionModule; // M�dulo de emisi�n

    void Start()
    {
        // Inicializa la velocidad actual con la velocidad normal
        currentSpeed = rotationSpeed;

        // Obt�n el m�dulo de emisi�n de las part�culas
        emissionModule = bubbleParticles.emission;

        // Configura el ratio inicial de emisi�n
        emissionModule.rateOverTime = emissionRate;
    }

    void Update()
    {
        // Si la velocidad actual es mayor que la velocidad base, se reduce gradualmente
        if (currentSpeed > rotationSpeed)
        {
            currentSpeed -= currentSpeed / 100;
        }

        // Si el ratio de emisi�n actual es mayor que el normal, tambi�n se reduce gradualmente
        if (emissionModule.rateOverTime.constant > emissionRate)
        {
            float newEmissionRate = emissionModule.rateOverTime.constant - emissionModule.rateOverTime.constant / 100;
            emissionModule.rateOverTime = newEmissionRate;
        }

        // Gira el objeto continuamente en el eje Z
        cilindroGiratorio.transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Simula un boost al presionar la tecla Espacio
        if (Input.GetMouseButtonDown(0)) // 0 es el bot�n izquierdo del mouse
        {
            BoostSpeed();
        }
    }
    public void BoostSpeed()
    {
        currentSpeed = boostedSpeed;

        // Cambia el ratio de emisi�n al valor aumentado
        emissionModule.rateOverTime = boostedEmissionRate;
    }
}
