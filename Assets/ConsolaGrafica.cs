using TMPro;
using UnityEngine;

public class ConsolaGrafica : MonoBehaviour
{

    public static ConsolaGrafica instancia;

    public GameObject prefabTexto;
    private void Awake()
    {
        if (instancia != null)
        {
            return;
        }
        instancia = this;
    }


    public void crearTexto(string mensaje, Color color)
    {
        GameObject nuevoTexto = Instantiate(prefabTexto, transform);
        nuevoTexto.GetComponent<TextMeshProUGUI>().text = mensaje;
        nuevoTexto.GetComponent<TextMeshProUGUI>().color = color;
        Destroy(nuevoTexto, 4);
    }

}
