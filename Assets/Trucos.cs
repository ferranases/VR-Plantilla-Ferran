using UnityEngine;

public class Trucos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo")
        {
            GAME.vibrarMano(50, 2, 100);
            GAME.vibrarManoContraria(50, 2, 100);

            Inventario.instancia.trucos();
        }
    }
}
