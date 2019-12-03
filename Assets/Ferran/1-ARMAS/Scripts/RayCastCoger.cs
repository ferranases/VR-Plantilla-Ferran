using UnityEngine;

public class RayCastCoger : MonoBehaviour
{
    public OVRInput.Controller BotonAgarre;
    public int layer;
    public GameObject laserHijo;

    private Transform hijoRayCast;
    private bool activo;
    private GameObject objetoACoger;
    private GameObject objetoCogido;
    private Rigidbody rb;
    private GameObject ultimoObjetoRayCastSinPulsar;
    // Start is called before the first frame update
    void Start()
    {
        activo = true;
        objetoACoger = null;
        rb = transform.parent.GetComponent<Rigidbody>();
        hijoRayCast = transform.GetChild(0).transform;
        ultimoObjetoRayCastSinPulsar = null;
    }



    // Update is called once per frame
    void Update()
    {
        InteractuarSinPulsar();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, BotonAgarre) && objetoCogido == null)
        {
            Interactuar();
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, BotonAgarre) && objetoCogido == null && objetoACoger != null)
        {
            Coger();
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, BotonAgarre) && objetoCogido != null)
        {
            Soltar();
        }



    }
    //Este metodo esta para cuando pasas el raycast por algun elemento UI que haga algo
    void InteractuarSinPulsar()
    {
        //Colisiona con solo el ultimo numero
        int layerMask = 1 << 15;
        //Invierte y da a todos menos al ultimo numero
        layerMask = ~layerMask;
        if (Physics.Raycast(hijoRayCast.position, hijoRayCast.transform.right, out RaycastHit hit, 500, layerMask))
        {
            if (ultimoObjetoRayCastSinPulsar != null)
            {
                if (hit.transform.gameObject.name != ultimoObjetoRayCastSinPulsar.name)
                {
                    ultimoObjetoRayCastSinPulsar.GetComponent<ElementoUi>().Desinteractuar();
                }
            }

            if (hit.transform.tag == "ui")
            {
                ultimoObjetoRayCastSinPulsar = hit.transform.gameObject;
                hit.transform.gameObject.GetComponent<ElementoUi>().Interactuar();
            }
            else
            {
                ultimoObjetoRayCastSinPulsar = null;
            }
        }
    }
    void Interactuar()
    {
        //Colisiona con solo el ultimo numero
        int layerMask = 1 << 15;
        //Invierte y da a todos menos al ultimo numero
        layerMask = ~layerMask;
        if (Physics.Raycast(hijoRayCast.position, hijoRayCast.transform.right, out RaycastHit hit, 500, layerMask))
        {
            //Instantiate(bola, hit.point, Quaternion.identity);            
            if (hit.transform.tag == "ui")
            {
                hit.transform.gameObject.GetComponent<ElementoUi>().Accionar();
            }
        }
    }
    void Coger()
    {
        objetoCogido = objetoACoger;
        objetoCogido.GetComponent<objetoCogible>().coger(BotonAgarre, transform.parent);
        laserHijo.SetActive(false);
        activo = false;


    }

    void Soltar()
    {
        objetoCogido.GetComponent<objetoCogible>().soltar(rb.velocity);
        objetoCogido = null;
        laserHijo.SetActive(true);
        activo = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layer && activo)
        {
            objetoCogible scriptObjetoOther = other.gameObject.GetComponent<objetoCogible>();
            if (scriptObjetoOther.parent == null)
            {
                scriptObjetoOther.setOutlineEstado(true);
                objetoACoger = other.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layer && activo)
        {
            objetoCogible scriptObjetoOther = other.gameObject.GetComponent<objetoCogible>();
            if (scriptObjetoOther.parent == null)
            {
                scriptObjetoOther.setOutlineEstado(true);
                objetoACoger = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<objetoCogible>() && activo)
        {
            other.GetComponent<objetoCogible>().setOutlineEstado(false);
        }
        objetoACoger = null;
    }

}
