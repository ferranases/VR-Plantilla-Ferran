using UnityEngine;

public class Reloj : MonoBehaviour
{

    public Transform posicionManoIzq;
    public Transform posicionManoDer;
    public ParticleSystem ps_holograma;
    public MenuReloj menuReloj;

    private Animator animator;
    private bool activo;
    private bool particulasHolograma;



    private void Start()
    {
        ponerEnMano(GAME.zurdo);
        animator = GetComponent<Animator>();
        activo = false;
        particulasHolograma = false;
    }

    void Update()
    {
        float valorX = Mathf.Abs(transform.parent.rotation.eulerAngles.x);
        float valorZ = Mathf.Abs(transform.parent.rotation.eulerAngles.z);

        if ((valorX < 50 || valorX > 300) && (valorZ < 50 || valorZ > 300) && GAME.historia > 0)
        {
            animator.SetBool("relojActivo", true);
            activo = true;
        }
        else
        {
            animator.SetBool("relojActivo", false);
            activo = false;
            particulasHolograma = false;
            menuReloj.desactivar();
            ps_holograma.Stop();
        }
    }

    public void ponerEnMano(bool valor)
    {
        Transform posicion;
        if (valor)
        {
            posicion = posicionManoDer;
        }
        else
        {
            posicion = posicionManoIzq;
        }
        transform.SetParent(posicion.parent);
        transform.localPosition = posicion.localPosition;
        transform.localRotation = posicion.localRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo" && activo)
        {
            if (!particulasHolograma)
            {
                ps_holograma.Play();
                menuReloj.activar();
                particulasHolograma = true;
                GAME.vibrarManoContraria(30, 2, 60);
            }
            else
            {
                particulasHolograma = false;
                ps_holograma.Stop();
                menuReloj.desactivar();
            }
        }
    }
}
