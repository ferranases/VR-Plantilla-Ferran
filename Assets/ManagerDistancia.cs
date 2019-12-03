using UnityEngine;

public class ManagerDistancia : MonoBehaviour
{
    private ObjetoDistancia[] objetos;
    // Start is called before the first frame update
    void Start()
    {
        objetos = GameObject.FindObjectsOfType<ObjetoDistancia>();
    }

    // Update is called once per frame
    void Update()
    {

        if (objetos.Length > 0)
        {
            foreach (ObjetoDistancia item in objetos)
            {
                if (item != null)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) > item.distancia)
                    {
                        item.gameObject.SetActive(false);
                    }
                    else
                    {
                        item.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
