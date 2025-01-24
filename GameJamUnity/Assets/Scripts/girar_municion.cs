using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girar_municion : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad normal de rotación
    public float boostedSpeed = 150f; // Velocidad aumentada
    public float boostDuration = 2f; // Duración del aumento de velocidad en segundos
    [SerializeField] ParticleSystem bubbleParticles; // Partículas
    [SerializeField] GameObject cilindroGiratorio;
    public float emissionRate = 2f; // Ratio normal de emisión
    public float boostedEmissionRate = 100f; // Ratio de emisión durante el boost

    private float currentSpeed; // Velocidad actual de rotación

    private ParticleSystem.EmissionModule emissionModule; // Módulo de emisión

    void Start()
    {
        // Inicializa la velocidad actual con la velocidad normal
        currentSpeed = rotationSpeed;

        // Obtén el módulo de emisión de las partículas
        emissionModule = bubbleParticles.emission;

        // Configura el ratio inicial de emisión
        emissionModule.rateOverTime = emissionRate;
    }

    void Update()
    {
        // Si la velocidad actual es mayor que la velocidad base, se reduce gradualmente
        if (currentSpeed > rotationSpeed)
        {
            currentSpeed -= currentSpeed / 100;
        }

        // Si el ratio de emisión actual es mayor que el normal, también se reduce gradualmente
        if (emissionModule.rateOverTime.constant > emissionRate)
        {
            float newEmissionRate = emissionModule.rateOverTime.constant - emissionModule.rateOverTime.constant / 100;
            emissionModule.rateOverTime = newEmissionRate;
        }

        // Gira el objeto continuamente en el eje Z
        cilindroGiratorio.transform.Rotate(Vector3.forward * currentSpeed * Time.deltaTime);

        // Simula un boost al presionar la tecla Espacio
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del mouse
        {
            currentSpeed = boostedSpeed;

            // Cambia el ratio de emisión al valor aumentado
            emissionModule.rateOverTime = boostedEmissionRate;
        }
    }
}
