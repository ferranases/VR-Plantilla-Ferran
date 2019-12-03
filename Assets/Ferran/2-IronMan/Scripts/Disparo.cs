using UnityEngine;

public class Disparo : MonoBehaviour
{
    public OVRInput.Controller controlador;
    public Animator animator;
    public GameObject proyectil;
    public Transform salidaDisparo;

    private AudioSource audioSource;
    private float carga = 0;
    private bool recarga = true;
    private bool municion = true;

    private int animacionEstado = 0;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controlador))
        {
            if (animacionEstado == 0)
            {
                animator.SetBool("propulsando", true);
                animacionEstado = 1;
            }
            checkDispararo();

        }
        else
        {
            recarga = true;
            municion = true;

            if (animacionEstado == 1)
            {
                animator.SetBool("propulsando", false);
                animacionEstado = 0;
            }


            if (carga < 0.5f)
            {
                audioSource.Stop();
            }
        }
    }

    void checkDispararo()
    {
        if (recarga)
        {
            carga = 0;
            recarga = false;
        }

        if (carga == 0)
        {
            audioSource.Stop();
            audioSource.volume = 1;
            audioSource.Play();
        }
        carga += Time.deltaTime;
        if (carga < 0.5f)
        {
            //Vibracion Mando
            VibracionManager.vibracion(20, 2, 255, controlador);
        }
        if (carga > 0.5f)
        {

            disparar();
        }
        if (carga > 2)
        {
            carga = 0;
            municion = true;
        }


    }

    void disparar()
    {
        if (municion)
        {
            municion = false;
            GameObject nuevoObjeto = Instantiate(proyectil, salidaDisparo.position, Quaternion.identity);
            nuevoObjeto.GetComponent<Rigidbody>().AddForce(transform.up * 10000);
            VibracionManager.vibracion(255, 3, 255, controlador);
            Destroy(nuevoObjeto, 5);
        }

    }
}
