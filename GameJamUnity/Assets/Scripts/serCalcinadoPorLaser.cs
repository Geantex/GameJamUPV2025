using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mathf = UnityEngine.Mathf;

public class SerCalcinadoPorLaser : MonoBehaviour
{
    // Materiales del objeto y sus hijos
    private List<Material> materials = new List<Material>();

    // Colores originales de los materiales
    private List<Color> originalColors = new List<Color>();

    // Contador de veces que ha sido quemado
    [SerializeField]
    public int burnCounter = 0;
    [SerializeField] private GameObject efectoExplosionBurbuja; // Prefab del efecto de explosión


    // Variable para verificar si el objeto está completamente calcinado
    private bool isCompletelyBurned = false;

    // Referencia al script de Vida
    private Vida vida;

    [Header("Configuración de recuperación de color")]
    // Velocidad de recuperación del color
    [SerializeField]
    private float recoverySpeed = 0.5f;

    // Booleano para habilitar o deshabilitar la recuperación
    public bool isRecovery = false;

    [Header("Configuración de quemado")]
    // Velocidad de quemado
    public float burnSpeed = 1f;

    void Start()
    {
        // Recopilar todos los materiales del objeto y sus hijos
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material != null)
            {
                materials.Add(renderer.material);
                originalColors.Add(renderer.material.color); // Guardar color original
            }
        }

        // Obtener referencia al componente "Vida"
        if(gameObject.tag != "Burbuja"){
            vida = GetComponent<Vida>();
            if (vida == null)
            {
                Debug.LogError("No se encontró el componente Vida en " + gameObject.name);
            }
        }
        
    }

    void Update()
    {
        // Si isRecovery está activado, permitir que los materiales se recuperen
        if (isRecovery)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i] != null)
                {
                    Color currentColor = materials[i].color;
                    Color originalColor = originalColors[i];
                    // Interpola entre el color actual y el color original
                    Color newColor = Color.Lerp(currentColor, originalColor, recoverySpeed * Time.deltaTime);
                    materials[i].color = newColor;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Verificar si el objeto dentro del trigger tiene el tag "laser"
        if (other.CompareTag("laser"))
        {
            statsLaser statsLaserImpacto = other.GetComponent<statsLaser>();
            int damagePerFrame = statsLaserImpacto.damagePerFrame;
            float burnPerFrame = statsLaserImpacto.burnPerFrame * burnSpeed; // Ajustar quemado según burnSpeed
            int extraDamageOnBurned = statsLaserImpacto.extraDamageOnBurned;
            GameObject efectoDeMuerte = statsLaserImpacto.efectoDeMuerte;

            // Define el color objetivo al que quieres llegar (en este caso, #00CEFF)
            Color targetColor = new Color(0f, 0.807f, 1f); // Esto es #00CEFF convertido a RGB

            foreach (Material material in materials)
            {
                if (material != null)
                {
                    Color currentColor = material.color;
                    // Interpola entre el color actual y el color objetivo
                    Color newColor = Color.Lerp(currentColor, targetColor, burnPerFrame * Time.deltaTime);
                    material.color = newColor;
                }
            }

            // Aplicar daño por frame
            if (vida != null)
            {
                vida.QuitarVida(damagePerFrame, efectoDeMuerte);
            }

            // Incrementar contador de quemado
            burnCounter = burnCounter + Mathf.RoundToInt(burnPerFrame);


            if(vida != null){
                // Verificar si está completamente quemado
                if (burnCounter >= 100 && !isCompletelyBurned && vida.MirarVida() > 0 && gameObject.tag != "Burbuja")
                {
                    isCompletelyBurned = true;
                    if (vida != null)
                    {
                        vida.QuitarVida(extraDamageOnBurned, efectoDeMuerte);
                    }
                    //Debug.Log("El objeto está completamente ENJABONADO y recibió daño extra.");
                }
            }
            
            if(gameObject.tag == "Burbuja" && burnCounter >= 100){
                // Si la burbuja está completamente quemada, explotar
                BubbleBlast();
            }
        }
    }
    public void BubbleBlast(){
        Debug.Log("Voy a explotar - el goblin suicida");
        // es hora de burbujear (explosion burbuja!!!) - el goblin burbuja
        GameObject explosionBurbuja = Instantiate(efectoExplosionBurbuja, transform.position, Quaternion.identity);
        BurbujaExplosionArea explosionArea = explosionBurbuja.GetComponent<BurbujaExplosionArea>();
        explosionArea.ExplosionBurbuja();
        Destroy(gameObject);
    }
}
