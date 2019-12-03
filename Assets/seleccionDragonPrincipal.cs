using UnityEngine;

public class seleccionDragonPrincipal : MonoBehaviour
{
    public int numeroDragon;
    private Outline outline;
    private OVRInput.Controller controlador;

    private void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
        controlador = OVRInput.Controller.None;

        if (GAME.historia > 0)
        {
            destruirHuevos();
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controlador) && outline.enabled)
        {
            GAME.seleccionarDragonPrincipal(numeroDragon);
            destruirHuevos();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.name == "CogerObjetos")
        {
            outline.enabled = true;
            controlador = other.transform.gameObject.GetComponent<CogerObjetos>().BotonAgarre;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.name == "CogerObjetos")
        {
            outline.enabled = false;
            controlador = OVRInput.Controller.None;
        }
    }

    void destruirHuevos()
    {
        foreach (huevoMesaControl item in GameObject.FindObjectsOfType<huevoMesaControl>())
        {
            Destroy(item.gameObject);
        }
    }
}
