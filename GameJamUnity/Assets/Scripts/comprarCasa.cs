using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class apareceCasa : MonoBehaviour
{
    [SerializeField]
    public int costeCompra = 100;  // Ajusta en el Inspector el costo de la casa
    private GameObject casaModelo;  // Referencia al objeto CasaModelo
    public AdministradorAudio administradorAudio;

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
    }

    

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entra en el trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Obtenemos el script PlayerMoney del objeto que entró en el trigger
            PlayerMoney playerMoney = other.GetComponent<PlayerMoney>();
            

            if (playerMoney != null)
            {
                // Verificamos si el jugador tiene suficiente dinero
                if (playerMoney.CurrentMoney >= costeCompra)
                {
                    contadorCasa contadorcasa = GameObject.FindGameObjectWithTag("GameManager").GetComponent<contadorCasa>();

                    // Gastamos el dinero (opcional, si quieres que se descuente)
                    playerMoney.SpendMoney(costeCompra);
                    if(contadorcasa != null)
                    {
                        contadorcasa.SumarContador();
                        MejorarJugador();

                    }
                    else
                    {
                        Debug.Log("¡Es null!");
                    }

                    // Activamos el objeto CasaModelo si lo encontramos
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

                    // Desactivamos el objeto (Cylinder)
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

    private void MejorarJugador(){
        // Para saber que mejora corresponde a que casa, miraremos el nombre de la casa
        // Para conseguir el nombre de la casa, hay que mirar el nombre del padre del padre del objeto que tenga este script
        string nombreCasa = transform.parent?.parent?.name;
        switch(nombreCasa){
            case "CasaCorrer":
                GameObject.FindGameObjectWithTag("Player").GetComponent<reliquiasEquipadas>().setEsprintar(true);
                string titulo = "Correr";
                string descripcion = "Ahora puedes correr con SHIFT";

                break;
            case "CasaPocoCooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CooldownBurbuja>().cooldownTime -= 0.35f;
                string titulo = "Jabon de burbujas mejorado";
                string descripcion = "Cooldown reducido para disparar burbujas";
                break;
            case "CasaLaser":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<LaserMagico>().enabled = true;
                string titulo = "Laser de jabon";
                string descripcion = "Click derecho. ¡Dispara a las burbujas para un combo!";                
                break;
            case "CasaSinCooldown":
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CooldownBurbuja>().cooldownTime = 0.05f;
                string titulo = "Super ultra jabon Fairy";
                string descripcion = "¡Dispara todas las burbujas que quieras!";                
                break;
            case "CasaVidaExtra":
                GameObject.FindGameObjectWithTag("Player").GetComponent<VidaJugador>().MejoraVida();
                string titulo = "Vida extra";
                string descripcion = "¡Gracias, servicios medicos de Bellus!";                
                break;
            default:
                Debug.LogWarning("No se encontró el nombre de la casa.");
                break;
        }
    }
}
