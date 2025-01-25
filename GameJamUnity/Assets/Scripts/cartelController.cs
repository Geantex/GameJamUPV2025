using UnityEngine;
using TMPro; // Necesario si usas TextMeshPro

public class CartelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPrecio; // Aasigna el componente de texto desde el Inspector
    [SerializeField] private apareceCasa scriptCasa; // Referencia al script que contiene costeCompra

    void Start()
    {
        if (scriptCasa != null && textoPrecio != null)
        {
            // Actualiza el texto con el valor de costeCompra
            textoPrecio.text = $"Esta casucha cuesta: {scriptCasa.costeCompra}";
        }
        else
        {
            Debug.LogError("Faltan referencias al script o al componente de texto.");
        }
    }
}
