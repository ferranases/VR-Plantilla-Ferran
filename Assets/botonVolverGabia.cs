using UnityEngine;

public class botonVolverGabia : MonoBehaviour
{
    public Transform posMundo;
    public Gabia gabia;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            GameObject.FindObjectOfType<PlayerCazadragones>().setPositionMesh(posMundo.position);
            gabia.desactivar();
            GAME.setDragonVisible(GAME.dragonActivo, true);
            GameObject.FindObjectOfType<Dragon>().setTamanioEdad();
        }
    }

}
