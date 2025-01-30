using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class contadorCasa : MonoBehaviour
{
    private int contador;
    private void Start()
    {
        contador = 0;
    }
    
    // Start is called before the first frame update

    void Update()
    {
        if (contador == 5)
        {
            SceneManager.LoadScene("ganar");
        }
    }
    public void SumarContador()
    {
        contador++;
    }
}
