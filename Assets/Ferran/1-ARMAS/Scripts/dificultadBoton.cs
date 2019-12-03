using UnityEngine;

public class dificultadBoton : ElementoUi
{

    public int numeroDificultad;
    public controlEscenarios controlEscenarios;

    private Color coloAnterior;
    private bool haEntradoEnDesinteractuar;

    private void Start()
    {
        haEntradoEnDesinteractuar = true;
    }

    public override void Accionar()
    {
        controlEscenarios.cambiarDificultad(numeroDificultad);
    }

    public override void Interactuar()
    {
        if (haEntradoEnDesinteractuar)
        {
            haEntradoEnDesinteractuar = false;
            coloAnterior = GetComponent<Renderer>().material.color;
        }

        GetComponent<Renderer>().material.color = Color.yellow;
    }
    public override void Desinteractuar()
    {
        haEntradoEnDesinteractuar = true;
        GetComponent<Renderer>().material.color = coloAnterior;
    }

    public void setColorAnterior(Color color)
    {
        coloAnterior = color;
    }

}
