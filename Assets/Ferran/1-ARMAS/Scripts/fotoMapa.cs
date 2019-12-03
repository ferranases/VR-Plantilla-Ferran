using UnityEngine;

public class fotoMapa : ElementoUi
{

    public int numeroMapa;
    public controlEscenarios controlEscenarios;

    private Color coloAnterior;
    private bool haEntradoEnDesinteractuar;

    private void Start()
    {
        haEntradoEnDesinteractuar = true;
    }

    public override void Accionar()
    {
        controlEscenarios.cambiarMapa(numeroMapa);
    }

    public override void Interactuar()
    {
        if (haEntradoEnDesinteractuar)
        {
            haEntradoEnDesinteractuar = false;
            coloAnterior = GetComponent<SpriteRenderer>().material.color;
        }

        GetComponent<SpriteRenderer>().material.color = Color.yellow;
    }
    public override void Desinteractuar()
    {
        haEntradoEnDesinteractuar = true;
        GetComponent<SpriteRenderer>().material.color = coloAnterior;
    }

    public void setColorAnterior(Color color)
    {
        coloAnterior = color;
    }

}
