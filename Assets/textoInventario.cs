using TMPro;
using UnityEngine;

public class textoInventario : MonoBehaviour
{
    public Transform camara = null;
    public TextMeshPro texto;


    private void Start()
    {
        camara = GameObject.Find("CenterEyeAnchor").transform;
    }
    public void constructor(int cantidad)
    {
        texto.text = "+" + cantidad.ToString();
        Destroy(gameObject, 0.5f);
    }

    private void Update()
    {

        transform.Translate((Vector3.up / 2) * Time.deltaTime);
        if (camara)
        {
            texto.transform.LookAt(2 * texto.transform.position - camara.position);
        }

    }
}
