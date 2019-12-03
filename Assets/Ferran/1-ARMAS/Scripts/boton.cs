using UnityEngine;

public class boton : MonoBehaviour
{
    public campoTiro campoTiro;
    private Animator animator;
    private bool activado;
    private bool botonActivado;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        botonActivado = false;
        activado = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "mano" && activado)
        {
            animator.SetBool("botonActivado", true);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "cubiertaPlastico")
        {
            activado = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "mano" && activado)
        {
            animator.SetBool("botonActivado", false);
            campoTiro.activar();
        }

        if (other.gameObject.name == "cubiertaPlastico")
        {
            activado = true;
        }

    }
}
