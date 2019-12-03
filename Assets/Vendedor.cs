using TMPro;
using UnityEngine;

public class Vendedor : MonoBehaviour
{

    public TiendaUI tienda;
    public TextMeshProUGUI texto;

    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        texto.gameObject.SetActive(false);
        animation = GetComponent<Animation>();

        animation.Play("TiendaDesactivar");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            texto.gameObject.SetActive(true);
            animation.Play("TiendaActivar");
            tienda.reset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            texto.gameObject.SetActive(false);
            animation.Play("TiendaDesactivar");
            tienda.reset();
        }
    }
}
