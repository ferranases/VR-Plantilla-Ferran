using UnityEngine;

public class tiendaBotonSalirSubmenu : MonoBehaviour
{
    public TiendaUI tienda;
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            animation.Play("objetoMenuPulsar");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            animation.Play("objetoMenuDespulsar");
            tienda.cerrarSubmenu();
        }
    }
}
