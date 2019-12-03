using UnityEngine;

public class subMenuInventario : MonoBehaviour
{
    Inventario inventario;
    public Transform padreObjetos;

    private ItemUI[] items;

    public void activar()
    {
        if (inventario == null)
        {
            inventario = Inventario.instancia;
            inventario.onItemChangedCallback += actualizarObjetos;

            items = padreObjetos.GetComponentsInChildren<ItemUI>();
        }
        actualizarObjetos();
    }

    //Prueba
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            inventario.removeCantity((int)Inventario.tipos.madera, 1);
        }

    }
    void actualizarObjetos()
    {
        for (int i = 0; i < padreObjetos.childCount; i++)
        {
            if (i < inventario.items.Count)
            {
                items[i].setItem(inventario.items[i]);
            }
            else
            {
                items[i].deleteItem();
            }
        }
    }



}
