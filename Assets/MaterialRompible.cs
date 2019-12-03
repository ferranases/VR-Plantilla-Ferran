using OVRTouchSample;
using UnityEngine;

public class MaterialRompible : MonoBehaviour
{

    public int vida;
    public int recompensaCadaX;
    public int cantidadRecompensaManos;
    public int cantidadRecompensaPico;
    public GameObject prefabMadera;
    public Inventario.tipos tipo;

    private int golpes;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        golpes = 0;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "CogerObjetos" || other.transform.tag == "pico")
        {
            if (other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>())
            {
                if (other.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
                {
                    if (other.gameObject.GetComponent<CogerObjetos>())
                    {
                        VibracionManager.vibracion(60, 3, 255, other.gameObject.GetComponent<CogerObjetos>().BotonAgarre);
                    }
                    else
                    {
                        VibracionManager.vibracion(60, 3, 255, other.transform.parent.gameObject.GetComponent<Hand>().m_controller);
                    }

                    audioSource.Play();
                    checkDrop(other);
                }
            }
        }
    }

    void checkDrop(Collider other)
    {
        golpes++;
        if ((golpes % recompensaCadaX) == 0)
        {
            GameObject nuevoItem = Instantiate(prefabMadera, other.transform.position, other.transform.rotation);
            if (other.transform.tag == "pico")
            {
                nuevoItem.GetComponent<Item3D>().constructor((int)tipo, cantidadRecompensaPico);
            }
            else
            {
                nuevoItem.GetComponent<Item3D>().constructor((int)tipo, cantidadRecompensaManos);
            }
        }

        if (golpes == vida)
        {
            Destroy(gameObject);
        }
    }

}
