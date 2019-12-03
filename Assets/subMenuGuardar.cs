using UnityEngine;

public class subMenuGuardar : MonoBehaviour
{
    public void activar()
    {
        Inventario.instancia.guardar();
        GAME.guardaPosicionPlayer();
    }
}
