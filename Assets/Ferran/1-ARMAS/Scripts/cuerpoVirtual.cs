using UnityEngine;

public class cuerpoVirtual : MonoBehaviour
{
    public Transform camaraCentral;
    private float distancia;
    // Start is called before the first frame update
    void Start()
    {
        distancia = camaraCentral.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(camaraCentral.position.x, camaraCentral.position.y - distancia, camaraCentral.position.z);
    }
}
