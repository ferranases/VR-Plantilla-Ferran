using UnityEngine;

public class plantaVeneno : MonoBehaviour
{

    public Transform[] posicionesFrutas;
    public GameObject prefabFruta;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < posicionesFrutas.Length; i++)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                GameObject nuevaFruta = Instantiate(prefabFruta, posicionesFrutas[i].position, Quaternion.identity, transform);
                nuevaFruta.GetComponent<Item3D>().constructor((int)Inventario.tipos.baya, 1);
            }
        }
    }

}
