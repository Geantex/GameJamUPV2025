using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCerrarOpciones : MonoBehaviour
{
    [Header ("Panel Principal)")]
    public GameObject panelPrincipal;
    public GameObject advertenciaSuperIdol;
    private bool isSuperIdolPlaying = false;
    [Header ("Panel Opciones")]
    public GameObject panelOpciones;
    public bool isPauseMenu;
    
    

    void Start(){
        if(advertenciaSuperIdol != null){
            advertenciaSuperIdol.SetActive(false);
            StartCoroutine(SuperIdolPlaying());
        }
    }
    

    public void AbrirOpciones()
    {
        //if(false){
        if(isSuperIdolPlaying){
            // decir que hay que esperar a que acabe super-idol
            advertenciaSuperIdol.SetActive(true);
        } 
        else {
            panelOpciones.SetActive(true);
            panelPrincipal.SetActive(false);
        }

        if(isPauseMenu){
            Time.timeScale = 0;
        }
    }

    public void CerrarOpciones()
    {

        if(isPauseMenu){
            Time.timeScale = 1f;
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>().LockCursor();
        }
        panelPrincipal.SetActive(true);
        panelOpciones.SetActive(false);
    }

    // corutina para esperar a que acabe super-idol
    public IEnumerator SuperIdolPlaying(){
        isSuperIdolPlaying = true;
        yield return new WaitForSeconds(15f);
        isSuperIdolPlaying = false;
        advertenciaSuperIdol.SetActive(false);
    }
}
