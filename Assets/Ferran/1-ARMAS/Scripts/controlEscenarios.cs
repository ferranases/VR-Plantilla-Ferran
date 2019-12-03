using System.Collections;
using UnityEngine;

public class controlEscenarios : MonoBehaviour
{
    public GameObject[] prefabsEscenarios;
    public GameObject prefabPirata;

    public GameObject botonStart;
    public GameObject[] mapas;
    public GameObject[] dificultades;

    public AudioClip[] clipsAudio;

    private controlPuerta controlPuerta;
    private GameObject escenarioActual;
    private int dificultad;
    private int mapa;
    private Transform lastPosicionPirata;
    private menuController menuController;
    private AudioSource audioSource;

    private int recordActual;

    private int numeroEnemigos;
    private float segundosEspera;
    private int numeroMuertes;

    private void Start()
    {
        dificultad = -1;
        mapa = -1;
        botonStart.SetActive(false);
        escenarioActual = null;
        controlPuerta = GameObject.FindObjectOfType<controlPuerta>();
        menuController = GameObject.FindObjectOfType<menuController>();
        lastPosicionPirata = null;
        audioSource = GetComponent<AudioSource>();
    }


    public void cambiarMapa(int mapa)
    {
        Debug.Log("Mapa cambiado a: " + mapa);
        for (int i = 0; i < mapas.Length; i++)
        {
            mapas[i].GetComponent<Renderer>().material.color = Color.white;
        }
        mapas[mapa].GetComponent<fotoMapa>().setColorAnterior(Color.green);

        cambiarEscenario(mapa);
        this.mapa = mapa;
        checkSalirStart();
    }

    public void cambiarDificultad(int dificultad)
    {
        Debug.Log("Dificultad cambiada a: " + dificultad);
        for (int i = 0; i < dificultades.Length; i++)
        {
            dificultades[i].GetComponent<Renderer>().material.color = Color.white;
        }
        dificultades[dificultad].GetComponent<dificultadBoton>().setColorAnterior(Color.green);
        this.dificultad = dificultad;
        cambiarDificultadReaparicionEnemigos(dificultad);
        checkSalirStart();
    }
    void cambiarDificultadReaparicionEnemigos(int dificultad)
    {
        switch (dificultad)
        {
            case 0:
                numeroEnemigos = 5;
                segundosEspera = 3.5f;
                break;

            case 1:
                numeroEnemigos = 10;
                segundosEspera = 2.8f;
                break;

            case 2:
                numeroEnemigos = 15;
                segundosEspera = 2;
                break;
        }
    }

    void checkSalirStart()
    {
        if (dificultad > -1 && mapa > -1)
        {
            botonStart.SetActive(true);
            if (PlayerPrefs.HasKey(dificultad + "-" + mapa))
            {
                recordActual = PlayerPrefs.GetInt(dificultad + "-" + mapa);
            }
            else
            {
                recordActual = 0;
            }

            menuController.cambiarRecord(recordActual, numeroEnemigos);
        }
        else
        {
            menuController.cambiarRecord(0, 0);
        }
    }

    void cambiarEscenario(int mapaNuevo)
    {
        if (mapa != mapaNuevo)
        {
            if (controlPuerta.abierta)
            {
                controlPuerta.cerrarPuerta();
                StartCoroutine(crearEscenarioEnSegundoPlano(mapaNuevo));
            }
            else if (!controlPuerta.abierta)
            {
                controlPuerta.abrirPuerta();
                Destroy(escenarioActual);
                escenarioActual = Instantiate(prefabsEscenarios[mapaNuevo], transform);
            }
        }
    }
    private IEnumerator crearEscenarioEnSegundoPlano(int mapaNuevo)
    {
        yield return new WaitForSeconds(2);
        controlPuerta.abrirPuerta();
        Destroy(escenarioActual);
        escenarioActual = Instantiate(prefabsEscenarios[mapaNuevo], transform);
        yield break;
    }

    Transform getNuevaPosicion()
    {
        for (int i = 0; i < 5; i++)
        {
            int numeroPosiciones = escenarioActual.transform.GetChild(0).childCount;
            int numeroAleatorio = Random.Range(0, numeroPosiciones);
            Transform nuevaPosicion = escenarioActual.transform.GetChild(0).transform.GetChild(numeroAleatorio).transform;
            if (nuevaPosicion != lastPosicionPirata)
            {
                return nuevaPosicion;
            }
        }
        return null;

    }

    public void startGame()
    {
        numeroMuertes = 0;
        StartCoroutine(crearEnemigos());
    }

    public void sumarMuertePirata()
    {
        audioSource.clip = clipsAudio[1];
        audioSource.Play();
        numeroMuertes++;
    }
    public void restarMuertePirata()
    {
        audioSource.clip = clipsAudio[2];
        audioSource.Play();
    }

    void crearEnemigo()
    {
        Transform nuevaPosicion = getNuevaPosicion();
        GameObject nuevoPirata = Instantiate(prefabPirata, nuevaPosicion.position, nuevaPosicion.rotation);
        nuevoPirata.transform.GetChild(0).GetComponent<pirata>().morirDespuesDe(segundosEspera, this);
        if (mapa == 0)
        {
            nuevoPirata.transform.GetChild(0).gameObject.layer = 16;
        }
        else
        {
            nuevoPirata.transform.GetChild(0).gameObject.layer = 17;
        }
        audioSource.clip = clipsAudio[0];
        audioSource.Play();
    }

    private IEnumerator crearEnemigos()
    {
        for (int i = 0; i < numeroEnemigos; i++)
        {
            crearEnemigo();
            yield return new WaitForSeconds(segundosEspera);
        }

        PlayerPrefs.SetInt(dificultad + "-" + mapa, numeroMuertes);
        checkSalirStart();
        menuController.setTextoCuentaAtras(numeroMuertes.ToString());

    }
}
