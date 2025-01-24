using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dispararAnim : MonoBehaviour
{
    private Animator thoseWhoMove;

    void Start()
    {
        // Obtén el componente Animator adjunto al mismo GameObject
        thoseWhoMove = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica si el Animator no es nulo
        if (thoseWhoMove != null)
        {
            // Detecta si se ha hecho clic con el botón izquierdo del mouse
            if (Input.GetMouseButtonDown(0))
            {
                // Establece el parámetro "isDisparar" en true
                thoseWhoMove.SetBool("isDisparar", true);
            }

            // Verifica si la animación ha terminado
            if (thoseWhoMove.GetBool("isDisparar"))
            {
                // Obtén el estado actual de la animación
                AnimatorStateInfo stateInfo = thoseWhoMove.GetCurrentAnimatorStateInfo(0);

                // Si la animación ha terminado, establece "isDisparar" en false
                if (stateInfo.IsName("disparar"))
                {
                    thoseWhoMove.SetBool("isDisparar", false);
                }
            }
        }
    }
}