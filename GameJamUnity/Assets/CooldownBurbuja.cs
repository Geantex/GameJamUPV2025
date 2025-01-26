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

        float elapsedTime = 0f;
        Color originalColor = originalMaterial.color;
        Color targetColor = Color.red;

        // Cambiar el color de forma progresiva a rojo durante 0.1 segundos
        while (elapsedTime < 0.1f)
        {
            elapsedTime += Time.deltaTime;
            tempMaterial.color = Color.Lerp(originalColor, targetColor, elapsedTime / 0.1f);
            gunPart.GetComponent<Renderer>().material = tempMaterial;
            yield return null;
        }

        // Mantener el material rojo por el tiempo restante del cooldown
        gunPart.GetComponent<Renderer>().material = tempMaterial;
        yield return new WaitForSeconds(cooldownTime - 0.1f);

        elapsedTime = 0f;

        // Cambiar el color de forma progresiva de rojo al original
        while (elapsedTime < 0.1f)
        {
            elapsedTime += Time.deltaTime;
            tempMaterial.color = Color.Lerp(targetColor, originalColor, elapsedTime / 0.1f);
            gunPart.GetComponent<Renderer>().material = tempMaterial;
            yield return null;
        }

        gunPart.GetComponent<Renderer>().material = originalMaterial;
        isCoolingDown = false;
    }
}
