using MalbersAnimations;
using MalbersAnimations.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    public Transform Player;
    public Transform target;
    public Transform textoRotar;
    public Transform posicionPreMontadoGinete;
    public Transform posicionMonturaEnDragon;

    public NavMeshAgent nav;
    public MalbersInput malbersInput;
    public StepsManager stepsManager;

    public float Age = 1f;

    [Space(5)]
    public GameObject d_skinDragon;
    public GameObject d_ojos;

    [Space(5)]
    [Header("Montura")]
    public GameObject[] monturas;

    public GameObject bola;

    private float distanciaAVistaPersonaje;
    private LookAt lookAt;
    private bool movimiento;
    private bool personajeMontado;
    private bool visible;
    private bool activo;

    private Vector3 nuevaPosicion;

    private void Start()
    {
        lookAt = GetComponent<LookAt>();
        lookAt.Target = Player;
        malbersInput = GetComponent<MalbersInput>();
        Age = GAME.dragonAge;

        //Prueba
        //Age = 1;
        //GAME.dragonMontura = true;
        //Age = 0.15f;

        transform.localScale = new Vector3(Age, Age, Age);

        malbersInput.movimiento = false;
        personajeMontado = false;
        visible = true;
        activo = true;
        checkColocarMontura();


        StartCoroutine(rutinaMovimiento());
    }

    void Update()
    {
        if (!personajeMontado && activo && nav.enabled && visible)
        {
            controlDistanciaCorrer();
        }
        //TextoRotar
        if (Age == 1 && GAME.dragonMontura && visible && activo)
        {
            textoRotar.gameObject.SetActive(true);
            textoRotar.LookAt(Player);
        }
        else
        {
            textoRotar.gameObject.SetActive(false);
        }

        controlSubida();
        controlDistanciaAlJugador();
    }

    void controlDistanciaCorrer()
    {
        distanciaAVistaPersonaje = Vector3.Distance(transform.position, nuevaPosicion);
        Vector3 velocidad = nav.velocity;

        if (distanciaAVistaPersonaje < 5)
        {
            nav.velocity = Vector3.zero;
            malbersInput.velocidadDragon(0, 0);
        }
        else
        {
            nav.velocity = velocidad;
            if (distanciaAVistaPersonaje < 1.75)
            {
                malbersInput.velocidadDragon(1, 3);
            }
            else if (distanciaAVistaPersonaje > 1.75)
            {
                malbersInput.velocidadDragon(1, 4);
            }
        }
    }


    void controlSubida()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            if (personajeMontado == false && GAME.dragonMontura && Age == 1 && visible && activo)
            {
                montar();
            }
            else if (personajeMontado)
            {
                desmontar();
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            if (personajeMontado)
            {
                malbersInput.volar();
            }
        }

        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (personajeMontado)
            {
                malbersInput.subirAltura();
            }
        }

        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if (personajeMontado)
            {
                malbersInput.bajarAltura();
            }
        }
    }

    void montar()
    {
        //Script movDragon
        malbersInput.movimiento = true;
        malbersInput.velocidadDragon(0, 0);

        nav.enabled = false;
        personajeMontado = true;
        Player.gameObject.GetComponent<PlayerCazadragones>().montar();

        textoRotar.gameObject.SetActive(false);
    }
    void desmontar()
    {
        malbersInput.movimiento = false;

        nav.enabled = true;
        personajeMontado = false;
        Player.gameObject.GetComponent<PlayerCazadragones>().desmontar();

        textoRotar.gameObject.SetActive(true);
    }

    public void setDragonVisible(bool activo, bool valor)
    {
        this.activo = activo;
        this.visible = valor;
        if (visible && activo)
        {
            d_skinDragon.SetActive(true);
            d_ojos.SetActive(true);
            nav.enabled = true;
            stepsManager.Active = true;
        }
        else
        {
            d_skinDragon.SetActive(false);
            d_ojos.SetActive(false);
            nav.enabled = false;
            malbersInput.velocidadDragon(0, 0);
            stepsManager.Active = false;
        }
        checkColocarMontura();
    }

    public void construirDragon(Material skin, Material ojos)
    {
        d_skinDragon.GetComponent<Renderer>().material = skin;
        d_ojos.GetComponent<Renderer>().material = ojos;
    }

    public void setTamanioEdad()
    {
        Age = GAME.dragonAge;
        transform.localScale = new Vector3(Age, Age, Age);
    }

    public void checkColocarMontura()
    {
        if (Age == 1 && GAME.dragonMontura && visible)
        {
            for (int i = 0; i < monturas.Length; i++)
            {
                monturas[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < monturas.Length; i++)
            {
                monturas[i].SetActive(false);
            }
        }
    }

    void controlDistanciaAlJugador()
    {
        if (Vector3.Distance(Player.position, transform.position) > 30)
        {
            nav.enabled = false;
            transform.position = Player.position;
            nav.enabled = true;
        }
    }

    private IEnumerator rutinaMovimiento()
    {
        while (true)
        {
            NavMesh.SamplePosition(target.position + Random.insideUnitSphere * Random.Range(1, 10), out NavMeshHit hit, 300, 1);
            nuevaPosicion = hit.position;
            //Instantiate(bola, nuevaPosicion, Quaternion.identity);

            nav.speed = 1;
            nav.angularSpeed = 200;
            if (nav.isOnNavMesh)
            {
                nav.SetDestination(nuevaPosicion);
            }

            yield return new WaitForSeconds(Random.Range(4, 15));
        }
    }
}
