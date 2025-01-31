using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burbujaMoverAdelante : MonoBehaviour
{
    // Update is called once per frame
    public float speed;
    private AdministradorAudio administradorAudio;

    void Start(){
        administradorAudio = GameObject.FindGameObjectWithTag("administradorAudio").GetComponent<AdministradorAudio>();
    }
    void Update()
    {
        // quiero que se mueva hacia adelante a una velocidad cada frame
        transform.position += transform.forward * speed * Time.deltaTime;
        if(speed > 0.5f){
            speed -= 0.04f;
        } else{
            if(transform.localScale.x < 2f){
                transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
            } else{
                administradorAudio.ReproducirSonidoBurbujaPop();
                Destroy(gameObject);
            }
            
        }
        
    }
}
