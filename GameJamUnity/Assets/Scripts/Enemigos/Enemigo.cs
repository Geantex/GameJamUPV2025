using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public delegate void MuerteHandler();
    public event MuerteHandler OnMuerte;

    private bool yaMuerto = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        if (yaMuerto) return;
        yaMuerto = true;
        OnMuerte?.Invoke();
        Destroy(gameObject);
    }
}
