using System.Collections;
using UnityEngine;

public class Item3D : MonoBehaviour
{
    private int tipo;
    private int cantidad;

    public ParticleSystem particulas;
    public GameObject prefabTexto;
    public bool morirSinCojer = true;

    private objetoCogible objetoCogible;

    private bool objetoCogido = false;
    private bool morir = false;
    private MeshRenderer mesh;
    private bool recompensaCanjeada = false;
    private Coroutine rutinaMorir;
    private bool poderGuardarlo = true;

    // Start is called before the first frame update
    void Start()
    {
        objetoCogible = GetComponent<objetoCogible>();
        if (morirSinCojer)
        {
            rutinaMorir = StartCoroutine(rutinaActivarMuerteTras(30));
        }

        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!objetoCogido && objetoCogible.parent != null)
        {
            objetoCogido = true;
            if (!morirSinCojer)
            {
                StartCoroutine(rutinaActivarMuerteTras(30));
            }
        }

        if (morir && objetoCogible.parent == null && mesh.enabled)
        {
            Instantiate(particulas, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (objetoCogible.parent == null && !mesh.enabled)
        {
            Destroy(gameObject);
        }
    }

    public void constructor(int nuevoTipo, int nuevaCantidad)
    {
        tipo = nuevoTipo;
        cantidad = nuevaCantidad;
    }


    private IEnumerator rutinaActivarMuerteTras(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        if (morirSinCojer)
        {
            morir = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "bolsaInventario" && objetoCogido && !recompensaCanjeada && poderGuardarlo)
        {
            recompensaCanjeada = true;
            mesh.enabled = false;
            Inventario.instancia.addCantity(tipo, cantidad);
            GameObject instanciaTexto = Instantiate(prefabTexto, transform.position, Quaternion.identity);
            instanciaTexto.GetComponent<textoInventario>().constructor(cantidad);
        }

        if (other.gameObject.name == "HeadDragon")
        {
            recompensaCanjeada = true;
            mesh.enabled = false;
        }
    }

    public void stopRutinaMorir()
    {
        morirSinCojer = false;
        poderGuardarlo = false;
    }

}
