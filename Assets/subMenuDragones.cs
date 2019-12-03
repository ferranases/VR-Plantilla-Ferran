using UnityEngine;

public class subMenuDragones : MonoBehaviour
{

    public GameObject subMenuDragonOpciones;
    private int dragonSeleccionado = -1;


    public void activar()
    {
        subMenuDragonOpciones.SetActive(true);
    }

}
