using System.Collections;
using UnityEngine;

public class Arco : MonoBehaviour
{
    public Transform pos_puntoCentralInicio;
    public Transform centroCuerda;
    public Transform pos_arcoDerecha;
    public Transform pos_arcoIzquierda;

    [Space(5)]
    public int potenciaMaxima;

    [Space(5)]
    public GameObject[] manos;

    //Flecha
    [Space(5)]
    public Transform pos_FlechaArco;
    public Transform pos_FlechaMano;

    [Space(2)]
    private int flechaSelecionada = 0;
    public GameObject[] flechas;
    private GameObject flechaActual = null;
    private bool flechaEnArco = false;

    //Control
    private bool arcoAgarrado = false;
    private objetoCogible objetoCogible;
    private OVRInput.Controller controlador = OVRInput.Controller.None;
    private OVRInput.Controller controladorContrario;
    private float distanciaManos;
    private bool manoEnFlecha = false;
    private float limiteDistanciaManosCuerda = 0.65f;

    private void Start()
    {
        //set
        transform.SetParent(GameObject.FindObjectOfType<PlayerCazadragones>().transform);

        pos_arcoDerecha = GameObject.Find("guardarArcoDer").transform;
        pos_arcoIzquierda = GameObject.Find("guardarArcoIzq").transform;

        manos = new GameObject[2];
        manos[0] = GameObject.Find("CustomHandLeft");
        manos[1] = GameObject.Find("CustomHandRight");

        pos_FlechaMano = GameObject.Find("posicionFlechaEnMano").transform;

        objetoCogible = GetComponent<objetoCogible>();
        GuardarArco();
    }

    private void Update()
    {
        //Miramos la clase objetoCogible para ver si este arco ha sido cogido por una de las manos
        if (objetoCogible.parent != null && !arcoAgarrado)
        {
            Coger();
        }
        else if (objetoCogible.parent == null && arcoAgarrado)
        {
            Soltar();
        }

        //Control de el centro de la cuerda
        //resetear en el caso de soltar la flecha con la mano contraria
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, controladorContrario) && flechaEnArco && manoEnFlecha && arcoAgarrado)
        {
            moverCentroCuerda();
        }
        else
        {
            resetCentroCuerda();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controladorContrario) && flechaEnArco && manoEnFlecha && arcoAgarrado)
        {
            Disparo();
        }
    }

    //Preparacion de el arco
    void Coger()
    {
        GAME.manosOcupadas = true;
        arcoAgarrado = true;
        controlador = objetoCogible.parentController;

        if (controlador == OVRInput.Controller.LTouch)
        {
            controladorContrario = OVRInput.Controller.RTouch;
        }
        else
        {
            controladorContrario = OVRInput.Controller.LTouch;
        }
        crearFlechaManoContraria();
    }

    void Soltar()
    {
        GAME.manosOcupadas = false;
        arcoAgarrado = false;
        GuardarArco();
        controlador = OVRInput.Controller.None;

        if (flechaActual != null)
        {
            Destroy(flechaActual);
        }
    }


    public void crearFlechaManoContraria()
    {
        flechaEnArco = false;
        if (flechaActual == null)
        {
            if (Inventario.instancia.checkCantidadObjeto(4 + flechaSelecionada, 1))
            {
                flechaActual = Instantiate(flechas[flechaSelecionada]);
            }

        }

        if (flechaActual != null)
        {
            flechaActual.GetComponent<Flecha>().controlador = controlador;

            if (controlador == OVRInput.Controller.LTouch)
            {
                flechaActual.transform.SetParent(manos[1].transform);
            }
            else
            {
                flechaActual.transform.SetParent(manos[0].transform);
            }

            flechaActual.transform.localPosition = pos_FlechaMano.localPosition;
            flechaActual.transform.localRotation = pos_FlechaMano.localRotation;
        }

    }

    //Cuando esta la flecha cerca de el punto central la bloquearemos en el centro 
    //de el arco para que este preparada para dispararse
    public void cambiarFlechaAArco()
    {
        flechaEnArco = true;
        flechaActual.transform.SetParent(centroCuerda);
        flechaActual.transform.localPosition = pos_FlechaArco.localPosition;
        flechaActual.transform.localRotation = pos_FlechaArco.localRotation;
    }


    //Control de el movimiento de la cuerda
    void moverCentroCuerda()
    {
        distanciaManos = (manos[1].transform.position - manos[0].transform.position).magnitude;
        if (distanciaManos > limiteDistanciaManosCuerda) distanciaManos = limiteDistanciaManosCuerda;
        centroCuerda.localPosition = pos_puntoCentralInicio.localPosition - new Vector3(0, distanciaManos * 325, 0);

        //Vibracion
        //Debug.Log("<color=purple>" + distanciaManos + "</color>");
        int porciento = (int)((distanciaManos * 100) / 0.65f);
        int vibracion = (int)((porciento * 255) / 100);
        VibracionManager.vibracion(1, 1, vibracion / 2, controlador);
        VibracionManager.vibracion(1, 1, vibracion, controladorContrario);
    }

    void resetCentroCuerda()
    {
        centroCuerda.position = pos_puntoCentralInicio.position;
    }

    //Control de si la flecha esta en la mano o ya ha sido disparada
    //La flecha acciona este metodo si no esta disparada aun o si ha sido disparada
    public void setManoEnFlecha(bool valor)
    {
        manoEnFlecha = valor;
    }

    void Disparo()
    {
        Inventario.instancia.removeCantity(4 + flechaSelecionada, 1);
        float porciento = ((distanciaManos * 100) / limiteDistanciaManosCuerda);
        float fuerza = (porciento * potenciaMaxima) / 100;
        flechaActual.GetComponent<Flecha>().disparar(fuerza);
        resetearFlecha();
    }


    void resetearFlecha()
    {
        flechaActual = null;
        flechaEnArco = false;
        manoEnFlecha = false;
        StartCoroutine(crearFlechaRetardo());
    }

    private IEnumerator crearFlechaRetardo()
    {
        yield return new WaitForSeconds(0.5f);
        if (arcoAgarrado)
        {
            crearFlechaManoContraria();
        }
        yield break;
    }

    //Guarda el arco en una de las dos posiciones de detras de la espalda
    void GuardarArco()
    {
        Transform posicionGuardad;
        if (GAME.zurdo)
        {
            posicionGuardad = pos_arcoDerecha;
        }
        else
        {
            posicionGuardad = pos_arcoIzquierda;
        }

        objetoCogible.padrePosicion = posicionGuardad;

        transform.SetParent(posicionGuardad.parent);
        transform.localPosition = posicionGuardad.localPosition;
        transform.localRotation = posicionGuardad.localRotation;
    }

    //Si estamos tocando el arco en la espalada vibra para notar que lo tenemos cerca
    private void OnTriggerEnter(Collider other)
    {
        if (!arcoAgarrado && other.transform.gameObject.name == "CogerObjetos")
        {
            VibracionManager.vibracion(40, 2, 255, other.transform.gameObject.GetComponent<CogerObjetos>().BotonAgarre);
        }
    }

    //Si dejamos de tocar el arco en la espalada vibra poco para notar que ya no lo tenemos cerca
    private void OnTriggerExit(Collider other)
    {
        if (!arcoAgarrado && other.transform.gameObject.name == "CogerObjetos")
        {
            VibracionManager.vibracion(20, 1, 200, other.transform.gameObject.GetComponent<CogerObjetos>().BotonAgarre);
        }
    }


}
