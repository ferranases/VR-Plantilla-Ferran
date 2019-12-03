using UnityEngine;

public class TiendaUI : MonoBehaviour
{
    public enum tipo { pico, flechaNormal, flechaVeneno, arco, montura };
    public TiendaSubMenu tiendaSubMenu;

    private TiendaObjetoUI[] objetos;

    // Start is called before the first frame update
    void Start()
    {
        objetos = new TiendaObjetoUI[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objetos[i] = transform.GetChild(i).GetComponent<TiendaObjetoUI>();
        }
        tiendaSubMenu.desactivar();
    }

    public void reset()
    {
        cerrarSubmenu();
    }



    public void subMenu(TiendaObjetoUI objeto)
    {
        foreach (TiendaObjetoUI item in objetos)
        {
            item.activo = false;

        }

        tiendaSubMenu.activar(objeto);

    }

    public void cerrarSubmenu()
    {
        foreach (TiendaObjetoUI item in objetos)
        {
            item.activo = true;
        }
        tiendaSubMenu.gameObject.SetActive(false);
    }
}
