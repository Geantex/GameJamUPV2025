using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public delegate void MuerteHandler();
    public event MuerteHandler OnMuerte;
    public GameObject efectoDeMuerte; // Prefab del efecto de muerte
    public int damage;

    private bool yaMuerto = false;

    public AdministradorAudio administradorAudio;
    public GameObject cigarro;
    private void Start()
    {
        int dificultad = PlayerPrefs.GetInt("Dificultad", 1); // Por defecto: Normal
        float velocidadEnemigo = 6.2f; // este es un valor estandar para que Unity no se queje - el goblin suicida (pronto todo acabara)
        switch(dificultad){
            case 0:
                damage = 5;
                velocidadEnemigo = 4f;
                break;
            case 1:
                damage = 15;
                velocidadEnemigo = 6.2f;
                break;
            case 2:
                damage = 20;
                velocidadEnemigo = 7.2f;
                break;
        }
        Debug.Log("Asignando velocidad de enemigo: " + velocidadEnemigo);
        gameObject.GetComponent<NavMeshAgent>().speed = velocidadEnemigo;


        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
        if(administradorAudio != null){
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