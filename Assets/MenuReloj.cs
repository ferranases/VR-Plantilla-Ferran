using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuReloj : MonoBehaviour
{
    public GameObject[] menus;

    private bool botonesBloqueados;

    void Start()
    {
        desactivar();
        botonesBloqueados = false;
    }

    public void activar()
    {
        desactivar();
        menus[0].SetActive(true);
    }

    public void desactivar()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    public void cambiarMenu(int boton)
    {
        botonesBloqueados = true;
        StartCoroutine(rutinaMostrarNuevoMenu(boton));
    }

    public bool getBotonesBloqueados()
    {
        return botonesBloqueados;
    }

    private IEnumerator rutinaMostrarNuevoMenu(int boton)
    {
        yield return new WaitForSeconds(0.1f);
        desactivar();
        if (boton == -2)
        {
            SceneManager.LoadScene("BASE");
        }
        else if (boton == -1)
        {
            menus[0].SetActive(true);

        }
        else
        {

            switch (boton)
            {
                case 1:
                    menus[1].SetActive(true);
                    menus[1].GetComponent<subMenuDragones>().activar();
                    break;

                case 2:
                    menus[2].SetActive(true);
                    menus[2].GetComponent<subMenuInventario>().activar();
                    break;

                case 3:
                    menus[3].SetActive(true);
                    menus[3].GetComponent<subMenuGuardar>().activar();
                    break;
                default:
                    break;
            }
        }
        yield return new WaitForSeconds(0.25f);
        botonesBloqueados = false;
    }

}
