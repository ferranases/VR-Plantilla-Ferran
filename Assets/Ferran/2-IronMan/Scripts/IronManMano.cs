using OVRTouchSample;
using UnityEngine;

public class IronManMano : MonoBehaviour
{
    public OVRInput.Controller controlador;

    public AudioClip[] clips;

    private IronMan ironMan;
    private AudioSource audioSource;
    private int faseSonido;
    private bool audioControl;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Hand>().AnimacionPropia = false;
        ironMan = GameObject.FindObjectOfType<IronMan>();
        audioSource = GetComponent<AudioSource>();
        faseSonido = 0;
        audioControl = false;
        characterController = GameObject.FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One, controlador))
        {
            if (controlador == OVRInput.Controller.LTouch)
            {
                ironMan.activadorIzq = true;
            }
            else
            {
                ironMan.activadorDer = true;
            }

        }
        else
        {
            if (controlador == OVRInput.Controller.LTouch)
            {
                ironMan.activadorIzq = false;
            }
            else
            {
                ironMan.activadorDer = false;
            }
        }
    }

    public void controladorSonido(bool activo, float actual, float max)
    {
        if (activo)
        {
            if (actual < 5 && audioControl == false)
            {
                audioControl = true;
                audioSource.Stop();
                audioSource.clip = clips[0];
                audioSource.loop = false;
                audioSource.volume = 1;
                audioSource.Play();

                faseSonido = 1;
            }

            if (actual > (max / 8) && faseSonido == 1)
            {
                audioSource.Stop();
                faseSonido = 2;
                audioSource.clip = clips[1];
                audioSource.loop = true;
                audioSource.volume = 0.3f;
                audioSource.Play();

            }
            VibracionManager.vibracionAudio(audioSource.clip, controlador);
        }
        else
        {
            audioSource.volume -= 0.02f;
            faseSonido = 1;
            audioControl = false;
            VibracionManager.vibracion(0, 0, 0, controlador);
        }

    }
}
