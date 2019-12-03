using UnityEngine;

public class controlZonaSinDragones : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" && GAME.dragonActualObj)
        {
            GAME.setDragonVisible(GAME.dragonActivo, false);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player" && GAME.dragonActualObj)
        {
            GAME.setDragonVisible(GAME.dragonActivo, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player" && GAME.dragonActualObj)
        {
            GAME.setDragonVisible(GAME.dragonActivo, true);
        }
    }
}
