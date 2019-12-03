using UnityEngine;

public class huevoMesaControl : MonoBehaviour
{

    private bool activo = false;
    private Animator animator;
    private Transform player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<OVRPlayerController>().transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Activar();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Desactivar();
        }
    }

    void Activar()
    {
        activo = true;
        animator.SetBool("roto", true);
    }

    void Desactivar()
    {
        activo = false;
        animator.SetBool("roto", false);
    }
}
