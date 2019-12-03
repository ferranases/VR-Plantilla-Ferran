using System.Collections;
using UnityEngine;

public class Minigun : MonoBehaviour
{

    public GameObject casquete;
    public Transform salidaBala;
    public Transform salidaCasquete;
    public ParticleSystem fuegoDisparo;
    public ParticleSystem chispasImpacto;
    public AudioSource audioSource;

    private objetoCogible oC;
    private bool armaCargada;

    private bool limitador;

    // Start is called before the first frame update
    void Start()
    {
        oC = GetComponent<objetoCogible>();
        limitador = true;
        //TODO ARMA CARGADA
        //armacargada;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, oC.parentController) && limitador)
        {
            Disparar();
            limitador = false;
        }
        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, oC.parentController))
        //{
        //    Disparar();

        //}
    }





    void Disparar()
    {


        //TODO CHECK ARMA CARGADA

        //EMPUJE OBJETO COLISIONADO CON BALA
        if (Physics.Raycast(salidaBala.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 500))
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
            }
        }


        //CASQUETES
        GameObject casqueteNuevo = Instantiate(casquete, salidaCasquete.position, Quaternion.identity, salidaCasquete);
        Rigidbody rbCn = casqueteNuevo.GetComponent<Rigidbody>();
        rbCn.AddForce(salidaCasquete.forward * Random.Range(225f, 325f));
        rbCn.maxAngularVelocity = 1000;
        casqueteNuevo.transform.SetParent(null);
        Destroy(casqueteNuevo, 4);




        //Efectos
        fuegoDisparo.Play();
        audioSource.Play();
        StartCoroutine(limitadorRutina());
    }

    private IEnumerator limitadorRutina()
    {
        yield return new WaitForSeconds(0.05f);
        limitador = true;
        yield break;
    }

}
