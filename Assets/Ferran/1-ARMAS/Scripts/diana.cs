using System.Collections;
using UnityEngine;

public class diana : MonoBehaviour
{
    public bool usada;
    public float tiempoRecarga = 0;

    private Animator animator;
    private bool activa;

    // Start is called before the first frame update
    void Start()
    {
        usada = false;
        animator = GetComponent<Animator>();
        reseteo();
        if (tiempoRecarga > 0)
        {
            subir();
        }
    }

    public void golpeo()
    {
        if (activa)
        {
            usada = true;
            activa = false;
            animator.SetBool("dianaGolpeada", true);

            if (tiempoRecarga > 0)
            {
                StartCoroutine(recarga());
            }
            else
            {
                GameObject.FindObjectOfType<campoTiro>().dianaBajada(gameObject.name);
            }
        }
    }

    public void subir()
    {
        activa = true;
        animator.SetBool("dianaGolpeada", false);
    }

    public void reseteo()
    {
        usada = false;
        activa = false;
        animator.SetBool("dianaGolpeada", true);
    }

    private IEnumerator recarga()
    {
        yield return new WaitForSeconds(tiempoRecarga);
        subir();
        yield break;
    }
}
