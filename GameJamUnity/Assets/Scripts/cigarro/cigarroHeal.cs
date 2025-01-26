using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cigarroHeal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
          VidaJugador vidaScript = other.GetComponent<VidaJugador>();
          vidaScript.RestarVida(-30);
          Destroy(this.gameObject);
        }
    }
}
