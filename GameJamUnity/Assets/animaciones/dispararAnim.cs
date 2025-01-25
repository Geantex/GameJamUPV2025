using UnityEngine;

public class dispararAnim : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Método para activar/desactivar la animación de disparo
    public void disparoAnim(bool mango)
    {
        animator.SetBool("isDisparar", mango);
    }

    // Método para obtener la duración de la animación de disparo
    public float thoseWhoMove()
    {
        // Obtén la duración de la animación de disparo desde el Animator
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.length;
    }
}