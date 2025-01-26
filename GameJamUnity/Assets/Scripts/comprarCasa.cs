using UnityEngine;
using TMPro;

public class apareceCasa : MonoBehaviour
{
    [SerializeField]
    public int costeCompra = 100; // Ajusta en el Inspector el costo de la casa
    private GameObject casaModelo; // Referencia al objeto CasaModelo
    public AdministradorAudio administradorAudio;
    public TextMeshProUGUI textomejora; // Texto para mostrar la mejora
    public float tiempoMostrarTexto = 5f; // Tiempo que el texto estará visible (en segundos)

    private void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();

        // Encontramos el padre del padre del objeto que tenga este script (el Cylinder)
        Transform parentOfParent = transform.parent?.parent;

        if (parentOfParent != null)
        {
            // Buscamos el objeto "CasaModelo" entre sus hijos
            casaModelo = parentOfParent.Find("CasaModelo")?.gameObject;

            if (casaModelo == null)
            {
                Debug.LogWarning("No se encontró el objeto CasaModelo en la jerarquía.");
            }
        }
        else
        {
            Debug.LogError("El padre del padre del objeto no existe.");
        }

        // Aseguramos que el texto de mejora esté desactivado al inicio
        if (textomejora != null)
        {
            textomejora.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMoney playerMoney = other.GetComponent<PlayerMoney>();

            if (playerMoney != null)
            {
                if (playerMoney.CurrentMoney >= costeCompra)
                {
                    contadorCasa contadorcasa = GameObject.FindGameObjectWithTag("GameManager").GetComponent<contadorCasa>();

                    playerMoney.SpendMoney(costeCompra);
                    if (contadorcasa != null)
                    {
                        contadorcasa.SumarContador();
                        MejorarJugador();
                    }
                    else
                    {
                        Debug.Log("¡Es null!");
                    }

                    if (casaModelo != null)
                    {
                        casaModelo.SetActive(true);
                        administradorAudio.ReproducirSonidoRandomTortosa();
                        administradorAudio.ReproducirSonidoConstruccion();
                    }
                    else
                    {
                        Debug.LogWarning("CasaModelo no fue asignado o no se encontró en la jerarquía.");
                    }

                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("No tienes suficiente dinero para comprar la casa.");
                }
            }
            else
            {
                Debug.LogWarning("El objeto Player no tiene el script PlayerMoney.");
            }
        }
    }

    private void MejorarJugador()
    {
        string nombreCasa = transform.parent?.parent?.name;
        string titulo = "";
        string descripcion = "";

        switch (nombreCasa)
        {
            case "CasaCorrer":
                GameObject.FindGameObjectWithTag("Player").GetComponent<reliquiasEquipadas>().setEsprintar(true);
                titulo = "Correr";
                descripcion = "Ahora puedes correr con SHIFT";
                break;
            case "CasaPocoCooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CooldownBurbuja>().cooldownTime -= 0.35f;
                titulo = "Jabón de burbujas mejorado";
                descripcion = "Cooldown reducido para disparar burbujas";
                break;
            case "CasaLaser":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LaserMagico>().enabled = true;
                titulo = "Láser de jabón";
                descripcion = "Click derecho. ¡Dispara a las burbujas para un combo!";
                break;
            case "CasaSinCooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CooldownBurbuja>().cooldownTime = 0.05f;
                titulo = "Super ultra jabón Fairy";
                descripcion = "¡Dispara todas las burbujas que quieras!";
                break;
            case "CasaVidaExtra":
                GameObject.FindGameObjectWithTag("Player").GetComponent<VidaJugador>().MejoraVida();
                titulo = "Vida extra";
                descripcion = "¡Gracias, servicios médicos de Bellus!";
                break;
            default:
                Debug.LogWarning("No se encontró el nombre de la casa.");
                return;
        }

        MostrarTextoMejora(titulo, descripcion);
    }

    private void MostrarTextoMejora(string titulo, string descripcion)
    {
        if (textomejora != null)
        {
            textomejora.text = $"{titulo}\n{descripcion}";
            textomejora.gameObject.SetActive(true);

            // Usamos Invoke para llamar a OcultarTexto después del tiempo especificado
            Invoke(nameof(OcultarTextoMejora), tiempoMostrarTexto);
        }
    }

    private void OcultarTextoMejora()
    {
        if (textomejora != null)
        {
            textomejora.text = "";
            textomejora.gameObject.SetActive(false);
        }
    }
}
