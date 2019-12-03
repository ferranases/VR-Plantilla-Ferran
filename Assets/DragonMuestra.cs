using UnityEngine;

public class DragonMuestra : MonoBehaviour
{

    public float Age = 1f;

    [Space(5)]
    public GameObject d_skinDragon;
    public GameObject d_ojos;

    public Partida partida;
    private Gabia gabia;

    private void Start()
    {
        gabia = GameObject.FindObjectOfType<Gabia>();
        Age = GAME.dragonAge;


        transform.localScale = new Vector3(Age, Age, Age);
    }

    public void activar()
    {
        construirDragon(partida.getSkinDragon(GAME.dragonInicial), partida.getSkinOjos(GAME.dragonInicial));
    }


    public void construirDragon(Material skin, Material ojos)
    {
        d_skinDragon.GetComponent<Renderer>().material = skin;
        d_ojos.GetComponent<Renderer>().material = ojos;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "item")
        {
            crecer();
            Inventario.instancia.removeCantity((int)Inventario.tipos.carne, 1);
            gabia.carneComida();

        }
    }

    void crecer()
    {
        GAME.dragonAumentarEdad();
        Age = GAME.dragonAge;
        transform.localScale = new Vector3(Age, Age, Age);
    }
}
