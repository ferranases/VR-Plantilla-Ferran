using UnityEngine;
using UnityEngine.UI;
public class TiendaObjetoUI : MonoBehaviour
{
    [SerializeField]
    private TiendaUI tienda;

    public TiendaUI.tipo tipo;
    public Image imagen;
    public string titulo;

    [System.Serializable]
    public class costeMaterial
    {
        public Inventario.tipos tipo;
        public int cantidad;
    }

    public costeMaterial[] costes;


    public bool activo = true;

    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo" && activo)
        {
            animation.Play("objetoMenuPulsar");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "dedo" && activo)
        {
            animation.Play("objetoMenuDespulsar");
            tienda.subMenu(this);
        }
    }


}
