using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite imagen;
    public string nombre;
    public int cantidad = 0;


    public bool usar()
    {
        if (cantidad > 0)
        {
            restarCantidad(1);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void sumarCantidad(int nuevaCantidad)
    {
        cantidad += nuevaCantidad;
        if (cantidad > 99)
        {
            cantidad = 99;
        }
    }

    public void restarCantidad(int nuevaCantidad)
    {
        cantidad -= nuevaCantidad;
    }
}
