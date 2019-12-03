using System.Collections;
using TMPro;
using UnityEngine;

public class Pistola : MonoBehaviour
{

    public GameObject casquete;
    public Transform salidaBala;
    public Transform salidaCasquete;
    public ParticleSystem fuegoDisparo;
    public ParticleSystem chispasImpacto;
    public AudioSource audioSource;

    public GameObject cargadorActual;
    public cargador cargadorScript;

    public bool laserActivo;

    public TextMeshPro balasTexto;

    //Tutos
    public GameObject[] tutoriales;

    private objetoCogible oC;
    private bool armaCargada;
    private bool armaCogidaCheckLocal;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        salidaBala.gameObject.SetActive(true);
        laserActivo = false;
        animator = GetComponent<Animator>();
        oC = GetComponent<objetoCogible>();
        //TODO ARMA CARGADA
        //armacargada;
        armaCogidaCheckLocal = false;

        for (int i = 0; i < tutoriales.Length; i++)
        {
            tutoriales[i].SetActive(false);
        }
        setBalasTexto(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, oC.parentController))
        {
            checkBalas(oC.parentController);
        }

        if (OVRInput.GetDown(OVRInput.Button.Three, oC.parentController) || OVRInput.GetDown(OVRInput.Button.One, oC.parentController))
        {
            if (cargadorActual != null)
            {
                soltarCargador();
            }

        }
        if (OVRInput.GetDown(OVRInput.Button.Two, oC.parentController) || OVRInput.GetDown(OVRInput.Button.Four, oC.parentController))
        {
            if (laserActivo)
            {
                laserActivo = false;
            }
            else
            {
                laserActivo = true;
            }
        }

        if (oC.parent != null && armaCogidaCheckLocal == false)
        {
            armaCogidaCheckLocal = true;
            lanzarTutorial(0, 2);
            if (oC.parentController == OVRInput.Controller.RTouch)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                balasTexto.transform.localScale = new Vector3(-balasTexto.transform.localScale.x, balasTexto.transform.localScale.y, balasTexto.transform.localScale.z);
                for (int i = 0; i < tutoriales.Length; i++)
                {
                    tutoriales[i].transform.GetChild(0).transform.Rotate(Vector3.up * 180);
                }
            }

            setBalasTexto(true);
        }
        else if (oC.parent == null && armaCogidaCheckLocal)
        {
            armaCogidaCheckLocal = false;
            setBalasTexto(false);
        }
    }

    void checkBalas(OVRInput.Controller controlador)
    {
        if (cargadorScript.balasActuales > 0)
        {
            cargadorScript.gastarBala();
            Disparar(controlador);
        }
        else
        {
            animator.SetBool("sinMunicion", true);
            lanzarTutorial(1, 0.75f);
        }
    }

    void Disparar(OVRInput.Controller controlador)
    {


        VibracionManager.vibracion(30, 2, 255, controlador);


        //Colisiona con solo el ultimo numero
        int layerMask = 1 << 14;
        //Invierte y da a todos menos al ultimo numero
        layerMask = ~layerMask;

        //EMPUJE OBJETO COLISIONADO CON BALA
        if (Physics.Raycast(salidaBala.position, transform.TransformDirection(Vector3.right), out RaycastHit hit, 500, layerMask))
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(salidaBala.forward * 500);

                if (hit.transform.gameObject.GetComponent<Rompible>())
                {
                    hit.transform.gameObject.GetComponent<Rompible>().cambiar();
                }
                else
                {
                    //PARTICULAS COLISION DECORADO
                    Instantiate(chispasImpacto, hit.point, Quaternion.identity);
                }

                if (hit.transform.gameObject.GetComponent<diana>())
                {
                    hit.transform.gameObject.GetComponent<diana>().golpeo();
                }
                else if (hit.transform.gameObject.GetComponent<pirata>())
                {
                    hit.transform.gameObject.GetComponent<pirata>().golpe();
                }
            }
        }


        //CASQUETES
        GameObject casqueteNuevo = Instantiate(casquete, salidaCasquete.position, Quaternion.identity, salidaCasquete);
        Rigidbody rbCn = casqueteNuevo.GetComponent<Rigidbody>();
        rbCn.AddForce(salidaCasquete.forward * Random.Range(125f, 225f));
        rbCn.maxAngularVelocity = 1000;
        casqueteNuevo.transform.SetParent(null);
        Destroy(casqueteNuevo, 4);


        //TExtoBalas
        setBalasTexto(true);


        //Animacion
        animator.Play("disparo", -1, 0f);

        //Efectos
        fuegoDisparo.Play();
        audioSource.Play();

    }

    private void setBalasTexto(bool activar)
    {
        if (activar)
        {
            if (cargadorScript.balasActuales > 0)
            {
                balasTexto.text = cargadorScript.balasActuales.ToString();
            }
            else
            {
                balasTexto.text = "- empty - ";
            }
        }

        else
        {
            balasTexto.text = "";
        }

    }

    private void soltarCargador()
    {

        animator.SetBool("cargadorFuera", true);
        cargadorScript.balasActuales = 0;
        StartCoroutine(recargar());
    }

    private IEnumerator recargar()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("sinMunicion", false);
        cargadorScript.balasActuales = cargadorScript.balasMax;
        animator.SetBool("cargadorFuera", false);
        setBalasTexto(true);
        yield break;
    }

    void lanzarTutorial(int indice, float segundos)
    {
        StartCoroutine(tutorial(indice, segundos));
    }
    private IEnumerator tutorial(int indice, float segundos)
    {
        tutoriales[indice].SetActive(true);
        yield return new WaitForSeconds(segundos);
        tutoriales[indice].SetActive(false);
    }
}
