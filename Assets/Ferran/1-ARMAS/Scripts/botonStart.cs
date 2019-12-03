using UnityEngine;
using UnityEngine.SceneManagement;

public class botonStart : ElementoUi
{
    public menuController menuController;
    public int numero;

    public override void Accionar()
    {
        if (numero == 0)
        {
            menuController.botonStartAccionar();
        }
        else
        {
            SceneManager.LoadScene("BASE");
        }

    }
    public override void Interactuar()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }
    public override void Desinteractuar()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
