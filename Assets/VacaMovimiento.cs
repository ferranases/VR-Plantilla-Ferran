using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VacaMovimiento : MonoBehaviour
{
    public int radio;
    public ParticleSystem particulasMuerte;
    public GameObject[] items_Prefabs;
    private Transform spawnItems;
    public GameObject bola;

    private Transform posicionJugador;
    private NavMeshAgent nav;
    private Vector3 nuevaPosicion;
    private Coroutine rutina;

    private bool moviendose = true;
    private bool perseguida = false;
    private bool morirControl = false;

    public void Start()
    {
        spawnItems = transform.GetChild(0).transform;
        nav = GetComponent<NavMeshAgent>();
        posicionJugador = GameObject.FindObjectOfType<PlayerCazadragones>().transform;

        crearNuevaPosicion();
    }
    private void Update()
    {
        if (moviendose && Vector3.Distance(transform.position, nuevaPosicion) < 3)
        {
            moviendose = false;
            perseguida = false;
            rutina = StartCoroutine(rutinaCambiarPosicion());
        }


        if (!perseguida && Vector3.Distance(transform.position, posicionJugador.position) < 15)
        {
            perseguida = true;
            moviendose = true;
            if (rutina != null)
            {
                StopCoroutine(rutina);
            }
            Vector3 heading = transform.position - posicionJugador.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            Vector3 direccionContraria = direction * radio;

            direccionContraria += transform.position;
            NavMesh.SamplePosition(direccionContraria, out NavMeshHit hit, radio, 1);
            nuevaPosicion = hit.position;

            nav.SetDestination(nuevaPosicion);
        }
        else if (perseguida && Vector3.Distance(transform.position, posicionJugador.position) > 15)
        {
            perseguida = false;
        }
    }

    void crearNuevaPosicion()
    {
        Vector3 randomDirection = Random.insideUnitSphere * radio;

        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radio, 1);
        nuevaPosicion = hit.position;

        nav.SetDestination(nuevaPosicion);
    }

    private IEnumerator rutinaCambiarPosicion()
    {
        Vector3 velocidad = nav.velocity;
        nav.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(3, 10));
        nav.velocity = velocidad;
        crearNuevaPosicion();

        moviendose = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!morirControl && other.transform.tag == "flecha")
        {
            morirControl = true;
            Destroy(other.gameObject);
            morir();
        }
    }

    void morir()
    {
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 30, 1);
        Vector3 nuevaPosicion = hit.position + new Vector3(0, 1, 0);
        //Instantiate(bola, nuevaPosicion, spawnItems.rotation);
        Instantiate(particulasMuerte, nuevaPosicion, spawnItems.rotation, transform);
        for (int i = 0; i < 2; i++)
        {
            int random = (int)Random.Range(1, 5);

            for (int e = 0; e < random; e++)
            {
                GameObject nuevoItem = Instantiate(items_Prefabs[i], nuevaPosicion, spawnItems.rotation);
                if (i == 0)
                {
                    nuevoItem.GetComponent<Item3D>().constructor((int)Inventario.tipos.carne, 1);
                }
                else
                {
                    nuevoItem.GetComponent<Item3D>().constructor((int)Inventario.tipos.piel, (int)Random.Range(1, 1));
                }

            }
        }


        Destroy(gameObject);
    }

}
