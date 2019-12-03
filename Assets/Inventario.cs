using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{

    #region Instancia
    public static Inventario instancia;

    private void Awake()
    {
        if (instancia != null)
        {
            return;
        }
        instancia = this;
    }
    #endregion

    public enum tipos { roca = 0, madera = 1, carne = 2, piel = 3, flecha = 4, flechaVenenosa = 5, baya = 6 };
    public Item[] itemsPrefabs;
    public List<Item> items = new List<Item>();

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("item-0"))
        {
            guardar();
        }
        cargar();
    }

    public void addCantity(int tipo, int cantidad)
    {
        if (!items.Contains(itemsPrefabs[tipo]))
        {
            items.Add(itemsPrefabs[tipo]);
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (itemsPrefabs[tipo].name == items[i].name)
            {
                items[i].sumarCantidad(cantidad);
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }
    }


    public void removeCantity(int tipo, int cantidad)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (itemsPrefabs[tipo].name == items[i].name)
            {
                items[i].restarCantidad(cantidad);

                if (items[i].cantidad < 1)
                {
                    itemsPrefabs[items[i].id].cantidad = 0;
                    items.Remove(items[i]);
                }

                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }
    }
    void cargar()
    {
        for (int i = 0; i < itemsPrefabs.Length; i++)
        {
            itemsPrefabs[i].cantidad = PlayerPrefs.GetInt("item-" + itemsPrefabs[i].id);
            if (itemsPrefabs[i].cantidad > 0)
            {
                int cantidadNueva = itemsPrefabs[i].cantidad;
                itemsPrefabs[i].cantidad = 0;
                addCantity(i, cantidadNueva);
            }
        }
    }
    public void guardar()
    {
        for (int i = 0; i < itemsPrefabs.Length; i++)
        {
            PlayerPrefs.SetInt("item-" + itemsPrefabs[i].id, itemsPrefabs[i].cantidad);
        }
    }

    public bool checkCantidadObjeto(int objeto, int cantidad)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == objeto)
            {
                if (items[i].cantidad >= cantidad)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public void trucos()
    {
        for (int i = 0; i < itemsPrefabs.Length; i++)
        {
            addCantity(i, 99);
        }
    }

}
