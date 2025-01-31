using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultadManager : MonoBehaviour
{

    public DificultadCambios dificultadFacil;
    public DificultadCambios dificultadNormal;
    public DificultadCambios dificultadDificil;
    // Start is called before the first frame update
    void Start()
    {
        // Cargar valores guardados o usar valores por defecto
        int dificultad = PlayerPrefs.GetInt("Dificultad", 1); // Por defecto: Normal

        Debug.Log("Dificultad cargada: " + dificultad);

        AveriguarDificultad();
    }

    private void AveriguarDificultad(){
        int dificultad = PlayerPrefs.GetInt("Dificultad", 1); // Por defecto: Normal
        switch(dificultad){
            case 0:
                Debug.Log("Dificultad: Fácil");
                AplicarDificultad(dificultadFacil);
                break;
            case 1:
                Debug.Log("Dificultad: Normal");
                AplicarDificultad(dificultadNormal);
                break;
            case 2:
                Debug.Log("Dificultad: Difícil");
                AplicarDificultad(dificultadDificil);
                break;
        }
    }

    private void AplicarDificultad(DificultadCambios dificultad){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>().AddMoney(dificultad.startingMoney);
        // realmente los otros cambios van en el script "Enemigo"
    }
}
