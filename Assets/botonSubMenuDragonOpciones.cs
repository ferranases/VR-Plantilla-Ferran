using UnityEngine;

public class botonSubMenuDragonOpciones : MonoBehaviour
{
    public int numeroBoton;
    public MenuReloj menuReloj;
    public Gabia gabia;
    public Transform posGabia;

    private Animation animation;



    private void Start()
    {
        animation = GetComponent<Animation>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            animation.Play("botonPulsado");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            animation.Play("botonDespulsado");
            activarBoton();
        }

    }

    void activarBoton()
    {
        if (numeroBoton == -1)
        {
            menuReloj.activar();
        }
        else
        {
            //TODO ir a visitar al dragon

            gabia.activar();
            GAME.setDragonVisible(GAME.dragonActivo, false);
            GameObject.FindObjectOfType<PlayerCazadragones>().setPositionMesh(posGabia.position);

        }
    }
}
