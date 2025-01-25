using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathf = UnityEngine.Mathf;

public class SerCalcinadoPorLaser : MonoBehaviour
{
    // Materiales del objeto y sus hijos
    private List<Material> materials = new List<Material>();

    // Contador de veces que ha sido quemado
    [SerializeField]
    private int burnCounter = 0;

    // Variable para verificar si el objeto está completamente calcinado
    private bool isCompletelyBurned = false;

    // Referencia al script de Vida
    private Vida vida;

    void Start()
    {
        // Recopilar todos los materiales del objeto y sus hijos
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material != null)
            {
                materials.Add(renderer.material);
            }
        }

        // Obtener referencia al componente "Vida"
        vida = GetComponent<Vida>();
        if (vida == null)
        {
            Debug.LogError("No se encontró el componente Vida en " + gameObject.name);
        }
    }

    void Update()
    {
        // El oscurecimiento ahora se maneja directamente en OnTriggerStay
    }

    void OnTriggerStay(Collider other)
    {
        // Verificar si el objeto dentro del trigger tiene el tag "laser"
        if (other.CompareTag("laser"))
        {
            statsLaser statsLaserImpacto = other.GetComponent<statsLaser>();
            int damagePerFrame = statsLaserImpacto.damagePerFrame;
            float burnPerFrame = statsLaserImpacto.burnPerFrame;
            int extraDamageOnBurned = statsLaserImpacto.extraDamageOnBurned;
            GameObject efectoDeMuerte = statsLaserImpacto.efectoDeMuerte;
            // Oscurecer los materiales
            foreach (Material material in materials)
            {
                if (material != null)
                {
                    Color currentColor = material.color;
                    Color darkerColor = Color.Lerp(currentColor, Color.black, burnPerFrame * Time.deltaTime);
                    material.color = darkerColor;
                }
            }


            // Aplicar daño por frame
            if (vida != null)
            {
                vida.QuitarVida(damagePerFrame, efectoDeMuerte);
                
            }

            // Incrementar contador de quemado
            burnCounter = burnCounter + Mathf.RoundToInt(burnPerFrame);

            // Verificar si está completamente quemado
            if (burnCounter >= 100 && !isCompletelyBurned && vida.MirarVida() > 0)
            {
                isCompletelyBurned = true;
                if (vida != null)
                {
                    vida.QuitarVida(extraDamageOnBurned, efectoDeMuerte);
                }
                Debug.Log("El objeto está completamente calcinado y recibió daño extra.");
            }
        }
    }
}
