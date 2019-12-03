using UnityEngine;

public class controlPuerta : MonoBehaviour
{
    public ParticleSystem[] particulas;
    public ParticleSystem humoColision;

    private Animator puertaAnimator;
    public bool abierta;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        abierta = false;
        puertaAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void accionarPuerta()
    {
        if (abierta)
        {
            cerrarPuerta();
        }
        else
        {
            abrirPuerta();
        }

    }

    public void abrirPuerta()
    {
        audioSource.Play();
        for (int i = 0; i < particulas.Length; i++)
        {
            particulas[i].Play();
        }
        abierta = true;
        puertaAnimator.SetBool("puertaAbrir", true);
    }

    public void cerrarPuerta()
    {

        humoColision.Play();
        abierta = false;
        puertaAnimator.SetBool("puertaAbrir", false);
    }



}
