using UnityEngine;

public class apareceCasa : MonoBehaviour
{
    [SerializeField]
    public int costeCompra = 100;  // Ajusta en el Inspector el costo de la casa
    private GameObject casaModelo;  // Referencia al objeto CasaModelo

    private void Start()
    {
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
                    // Gastamos el dinero (opcional, si quieres que se descuente)
                    playerMoney.SpendMoney(costeCompra);

                    // Activamos el objeto CasaModelo si lo encontramos
                    if (casaModelo != null)
                    {
                        casaModelo.SetActive(true);
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
}
