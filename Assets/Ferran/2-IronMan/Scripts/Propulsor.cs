using UnityEngine;

public class Propulsor : MonoBehaviour
{
    public OVRInput.Controller controlador;
    public Animator animator;

    private CentralPropulsores centralPropulsores;
    private int numeroPropulsor;
    private ParticleSystem pS_propulsor;
    private ParticleSystem.MainModule main;
    private int animacionEstado = 0;

    private void Start()
    {
        centralPropulsores = GameObject.FindObjectOfType<CentralPropulsores>();

        if (controlador == OVRInput.Controller.LTouch)
        {
            numeroPropulsor = 0;
        }
        else
        {
            numeroPropulsor = 1;
        }
        pS_propulsor = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        main = pS_propulsor.main;
        main.startSpeed = 0f;
    }


    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controlador))
        {
            if (animacionEstado == 0)
            {
                animacionEstado = 1;
                animator.SetBool("propulsando", true);
            }

            propulsar();
        }
        else
        {
            if (animacionEstado == 1)
            {
                animacionEstado = 0;
                animator.SetBool("propulsando", false);
            }

            despropulsar();
        }
    }


    void propulsar()
    {
        float valorPropulsion = centralPropulsores.propulsar(-transform.up * 5, numeroPropulsor);
        float nuevoValor = 0;
        if (valorPropulsion > 20)
        {
            nuevoValor = 400 - valorPropulsion * 2;
            if (nuevoValor > 100)
            {
                main.startSpeed = nuevoValor;
            }
            else
            {
                main.startSpeed = 100;
            }
        }
        else
        {
            main.startSpeed = 0;
        }
    }


    void despropulsar()
    {
        float valorPropulsion = centralPropulsores.despropulsar(numeroPropulsor);
        if (valorPropulsion > 20)
        {
            float nuevoValor = 400 - valorPropulsion * 2;
            if (nuevoValor > 100)
            {
                main.startSpeed = nuevoValor;
            }
            else
            {
                main.startSpeed = 100;
            }
        }
        else
        {
            main.startSpeed = 0;
        }
    }
}
