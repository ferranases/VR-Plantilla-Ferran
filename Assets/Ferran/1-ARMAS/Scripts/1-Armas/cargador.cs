using UnityEngine;

public class cargador : MonoBehaviour
{
    public int balasMax;
    public int balasActuales;
    public GameObject bala;

    // Start is called before the first frame update
    void Start()
    {
        balasActuales = balasMax;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gastarBala()
    {
        balasActuales--;
        if (balasActuales == 0)
        {
            Destroy(bala);
        }
    }
}
