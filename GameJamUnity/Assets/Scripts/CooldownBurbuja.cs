using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBurbuja : MonoBehaviour
{
    [Header("Cooldown")]
    public float cooldownTime = 0.5f;
    public bool isCoolingDown = false;

    [Header("Gun Part")]
    [SerializeField] private GameObject gunPart;
    private Material originalMaterial;
    private Material tempMaterial;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el material original del objeto
        originalMaterial = gunPart.GetComponent<Renderer>().material;

        // Crear un material temporal basado en el original
        tempMaterial = new Material(originalMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        // Aquí podrías llamar a Cooldown() según tu lógica
        // Por ejemplo, con un botón:
        // if (Input.GetKeyDown(KeyCode.Space)) Cooldown();
    }

    public void Cooldown()
    {
        if (!isCoolingDown)
        {
            StartCoroutine(CooldownCoroutine());
        }
    }

    private IEnumerator CooldownCoroutine()
    {
        isCoolingDown = true;

        float transitionDuration = cooldownTime * 0.1f; // Ejemplo: las transiciones toman el 20% del cooldown
        float holdDuration = cooldownTime * 0.8f;      // Ejemplo: mantener rojo toma el 60% del cooldown

        float elapsedTime = 0f;
        Color originalColor = originalMaterial.color;
        Color targetColor = Color.red;

        // Cambiar el color de forma progresiva a rojo durante la primera transición
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            tempMaterial.color = Color.Lerp(originalColor, targetColor, elapsedTime / transitionDuration);
            gunPart.GetComponent<Renderer>().material = tempMaterial;
            yield return null;
        }

        // Mantener el material rojo durante el tiempo restante
        gunPart.GetComponent<Renderer>().material = tempMaterial;
        yield return new WaitForSeconds(holdDuration);

        elapsedTime = 0f;

        // Cambiar el color de forma progresiva de rojo al original en la segunda transición
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            tempMaterial.color = Color.Lerp(targetColor, originalColor, elapsedTime / transitionDuration);
            gunPart.GetComponent<Renderer>().material = tempMaterial;
            yield return null;
        }

        gunPart.GetComponent<Renderer>().material = originalMaterial;
        isCoolingDown = false;
    }
}

