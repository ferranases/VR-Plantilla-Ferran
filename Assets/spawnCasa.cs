using UnityEngine;

public class spawnCasa : MonoBehaviour
{

    public GameObject casa;
    private void Start()
    {
        casa.SetActive(true);
    }
    //HISTORIA A NIVEL 1 cuan entres
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            casa.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {

            casa.SetActive(false);

            if (GAME.historia == 1)
            {
                //Subimos nivel de historia a 2
                GAME.subirNivelHistoria();
                GameObject.FindObjectOfType<Partida>().crearDragonActual();
            }
        }
    }
}
