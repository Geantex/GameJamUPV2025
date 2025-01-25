using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public delegate void MuerteHandler();
    public event MuerteHandler OnMuerte;
    public GameObject efectoDeMuerte; // Prefab del efecto de muerte

    private bool yaMuerto = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Muerte();
        }
    }

    public void Muerte()
    {
        if (yaMuerto) return;
        yaMuerto = true;
        OnMuerte?.Invoke();

        
        GameObject instanciaEfectoDeMuerte = Instantiate(efectoDeMuerte, transform.position, Quaternion.identity);
        Destroy(instanciaEfectoDeMuerte, 3f);
        Destroy(gameObject); // Destruye todo el objeto padre, asegurando que se elimine por completo
    }
}