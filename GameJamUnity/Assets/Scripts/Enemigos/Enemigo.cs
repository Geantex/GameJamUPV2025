using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public delegate void MuerteHandler();
    public event MuerteHandler OnMuerte;
    public GameObject efectoDeMuerte; // Prefab del efecto de muerte

    private bool yaMuerto = false;

    public AdministradorAudio administradorAudio;
    public GameObject cigarro;
    private void Start()
    {
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
        if(administradorAudio){
          administradorAudio.ReproducirSonidoRandomAparicionEnemigos();
        }
        
    }

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

        administradorAudio.ReproducirSonidoRandomMuerteEnemigos();
        GameObject instanciaEfectoDeMuerte = Instantiate(efectoDeMuerte, transform.position, Quaternion.identity);
        int randomValue = Random.Range(1,10);
        if(randomValue == 2)
        {
         Instantiate(cigarro, transform.position, Quaternion.identity);
        }
        Destroy(instanciaEfectoDeMuerte, 3f);
        Destroy(gameObject); // Destruye todo el objeto padre, asegurando que se elimine por completo
    }
}