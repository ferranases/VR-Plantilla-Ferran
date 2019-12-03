using System.Collections;
using UnityEngine;

public class IronMan : MonoBehaviour
{
    public Animator animator;
    public Material[] texturasManos;
    public GameObject[] manosTextura;
    public CentralPropulsores centralPropulsores;

    public GameObject[] mandosTutorial;

    public OVRInput.Controller controladorIzquierdo;
    public OVRInput.Controller controladorDerecho;

    public GameObject[] propulsores;
    public GameObject Hud;
    public GameObject corazon;

    public bool activadorIzq;
    public bool activadorDer;

    private bool IRONMAN;
    private bool bloqueo;
    private AudioSource audioSource;

    private void Start()
    {
        IRONMAN = false;
        bloqueo = false;
        colocarTexturasManos();
        colocarPropulsores();
        corazon.SetActive(false);
        Hud.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < 2; i++)
        {
            mandosTutorial[i].SetActive(true);
        }
    }
    void Update()
    {
        if (activadorIzq && activadorDer && !bloqueo)
        {
            ActivarTraje();
        }
    }
    void colocarTexturasManos()
    {
        int textura = 0;

        if (IRONMAN) textura = 1;

        for (int i = 0; i < manosTextura.Length; i++)
        {
            manosTextura[i].GetComponent<Renderer>().material = texturasManos[textura];
        }
    }
    void colocarPropulsores()
    {
        bool activar = false;

        if (IRONMAN) activar = true;

        for (int i = 0; i < propulsores.Length; i++)
        {
            propulsores[i].SetActive(activar);
        }
    }

    void ActivarTraje()
    {
        if (!IRONMAN)
        {
            Hud.SetActive(true);
            animator.SetBool("hudActivo", true);
            StartCoroutine(ActivarTrajeRutina());
            centralPropulsores.Activo = true;
            bloqueo = true;
            corazon.SetActive(true);
            audioSource.Play();
            StartCoroutine(coldownTrajeActivacio(4));

            for (int i = 0; i < 2; i++)
            {
                mandosTutorial[i].SetActive(false);
            }
        }
        else
        {
            animator.SetBool("hudActivo", false);
            IRONMAN = false;
            centralPropulsores.Activo = false;
            colocarTexturasManos();
            colocarPropulsores();
            bloqueo = true;
            corazon.SetActive(false);
            StartCoroutine(coldownTrajeActivacio(1));
        }

    }

    private IEnumerator coldownTrajeActivacio(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        bloqueo = false;
    }
    private IEnumerator ActivarTrajeRutina()
    {
        yield return new WaitForSeconds(3);
        IRONMAN = true;
        colocarPropulsores();
        colocarTexturasManos();

    }
}
