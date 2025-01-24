using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burbujaMoverAdelante : MonoBehaviour
{
    // Update is called once per frame
    public float speed;
    void Update()
    {
        // quiero que se mueva hacia adelante a una velocidad cada frame
        transform.position += transform.forward * speed * Time.deltaTime;
        
    }
}
