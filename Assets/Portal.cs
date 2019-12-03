using UnityEngine;
using UnityEngine.AI;

public class Portal : MonoBehaviour
{

    public GameObject Mundo;
    public GameObject tutorial;

    public Transform posicionInicial;
    public Transform Player;



    private void Start()
    {
        Mundo.SetActive(false);
        tutorial.SetActive(true);

        if (GAME.historia >= 3)
        {
            ActivarMundo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            //Pasamos de 2 a 3
            GAME.subirNivelHistoria();

            NavMesh.SamplePosition(posicionInicial.position, out NavMeshHit hit, 3, 1);
            Vector3 nuevaPosicion = hit.position;
            Player.position = nuevaPosicion;
            Mundo.SetActive(true);
            GAME.guardaPosicionPlayer();

            ActivarMundo();
        }
    }

    void ActivarMundo()
    {
        Mundo.SetActive(true);
        tutorial.SetActive(false);
    }
}
