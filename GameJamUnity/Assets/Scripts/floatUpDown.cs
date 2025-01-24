using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatUpDown : MonoBehaviour
{
    private float initialPosition;
    [SerializeField] private float floatSpeed = 0.5f;
    [SerializeField] private float floatAmplitude = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialPosition + floatAmplitude * Mathf.Sin(floatSpeed * Time.time), gameObject.transform.position.z);
    }
}
