using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growAtSpawn : MonoBehaviour
{

    [SerializeField] private float growTime;
    [SerializeField] private float desiredSize;
    private float size;
    [SerializeField] private GameObject thoseWhoGrow;

    // Start is called before the first frame update
    void Start()
    {
        size = 0.0f;
        thoseWhoGrow.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(size < desiredSize && thoseWhoGrow.activeSelf) // si el tamaño actual es menor que el deseado y el objeto está activo
        {
            //Debug.Log("El proyectil es del tamaño de: " + size);
            size += desiredSize/growTime*Time.deltaTime; // timeToReachDesiredSize is the time IN SECONDS to reach the desired size
            
            if(size > 0.0f) // los frames de espera se han acabado y ya aparece el mago
            {
                thoseWhoGrow.transform.localScale = new Vector3(0.2f * size, 0.2f * size, 0.2f * size);
            }
        }
    }
}
