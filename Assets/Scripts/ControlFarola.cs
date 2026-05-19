using UnityEngine;

public class ControlFarola : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // creamos un compnenete animator
        animator = GetComponent<Animator>();
    }

    // le decimos que se active (trigger) cuando este en el radio
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            animator.SetBool("estaCerca", true);
        }
    }

    // Se desactiva cuando sale
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("estaCerca", false);
        }
    }
}