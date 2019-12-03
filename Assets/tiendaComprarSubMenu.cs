using UnityEngine;

public class tiendaComprarSubMenu : MonoBehaviour
{
    public TiendaSubMenu subMenu;
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

            subMenu.comprar();
        }
    }
}
