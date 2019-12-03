using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Item item;

    public TextMeshProUGUI nombre;
    public Image imagen;
    public TextMeshProUGUI cantidad;

    public void setItem(Item itemNuevo)
    {
        item = itemNuevo;

        nombre.text = item.nombre;
        imagen.sprite = item.imagen;
        cantidad.text = item.cantidad.ToString();
        setVisible(true);
    }

    public void deleteItem()
    {
        item = null;
        setVisible(false);
    }

    void setVisible(bool valor)
    {
        nombre.enabled = valor;
        imagen.enabled = valor;
        cantidad.enabled = valor;
    }

}
