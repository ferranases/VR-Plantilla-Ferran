using UnityEngine;
using UnityEngine.UI;

public class seleccionManoPredominante : MonoBehaviour
{
    public int mano;

    private bool activo = false;
    private Image imagen;
    private menuManoPredominante menuManoPredominante;
    private OVRInput.Controller controlador;

    void Start()
    {
        if (PlayerPrefs.HasKey("zurdo"))
        {
            transform.parent.gameObject.SetActive(false);
        }
        else
        {
            transform.parent.gameObject.SetActive(true);
            GAME.player_movimintoPermitido(false);
        }

        imagen = GetComponent<Image>();
        menuManoPredominante = transform.parent.GetComponent<menuManoPredominante>();
        controlador = OVRInput.Controller.None;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controlador) && activo)
        {
            if (mano == 0)
            {
                GAME.setZurdo(true);
            }
            else
            {
                GAME.setZurdo(false);
            }
            Destroy(transform.parent.gameObject);
            GAME.player_movimintoPermitido(true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.name == "CogerObjetos" && menuManoPredominante.indiceSeleccionado == -1)
        {
            menuManoPredominante.indiceSeleccionado = mano;
            activo = true;
            imagen.color = Color.green;
            controlador = other.transform.gameObject.GetComponent<CogerObjetos>().BotonAgarre;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.name == "CogerObjetos")
        {
            activo = false;
            imagen.color = Color.white;
            menuManoPredominante.indiceSeleccionado = -1;
            controlador = OVRInput.Controller.None;
        }
    }
}
