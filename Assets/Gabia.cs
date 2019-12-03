using UnityEngine;

public class Gabia : MonoBehaviour
{
    public botonVolverGabia botonVolverGabia;
    public GameObject dragonMuestra;

    public GameObject prefabCarne;
    public Transform spawnCarne;

    private GameObject carneActual = null;

    private void Start()
    {
        desactivar();
    }
    public void activar()
    {
        gameObject.SetActive(true);
        dragonMuestra.SetActive(true);
        dragonMuestra.GetComponent<DragonMuestra>().activar();
        instanciarCarne();
    }

    public void desactivar()
    {
        gameObject.SetActive(false);
        dragonMuestra.SetActive(false);
    }

    public void instanciarCarne()
    {
        if (Inventario.instancia.checkCantidadObjeto((int)Inventario.tipos.carne, 1) && carneActual == null)
        {
            Item3D carne = Instantiate(prefabCarne, spawnCarne.position, spawnCarne.rotation).GetComponent<Item3D>();
            carne.stopRutinaMorir();
            carne.constructor((int)Inventario.tipos.carne, 1);
            carneActual = carne.gameObject;
        }
    }

    public void carneComida()
    {
        carneActual = null;
        instanciarCarne();
    }
}
