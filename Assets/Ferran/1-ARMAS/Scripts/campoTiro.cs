using System.Collections;
using TMPro;
using UnityEngine;

public class campoTiro : MonoBehaviour
{
    public GameObject[] dianas;
    public TextMeshPro textoInicio;
    public TextMeshPro textoTiempoActual;
    public TextMeshPro mejorTiempo;
    public int numeroDianasABatir;

    private int numeroDianasRestantes;
    private bool activo;
    private float contadorTiempoActual;
    private float mejorTiempoActual;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("mejorTiempoDiana"))
        {
            mejorTiempoActual = PlayerPrefs.GetFloat("mejorTiempoDiana");
        }
        else
        {
            PlayerPrefs.SetFloat("mejorTiempoDiana", 9999);
            mejorTiempoActual = 9999;
        }
    }

    private void Start()
    {
        activo = false;
        textoInicio.text = "";
        textoTiempoActual.text = "";
        mejorTiempo.text = mejorTiempoActual.ToString();
    }

    private void Update()
    {
        if (activo)
        {
            contadorTiempoActual += Time.deltaTime;
            textoTiempoActual.text = contadorTiempoActual.ToString("F2");
        }
    }

    public void activar()
    {
        if (!activo)
        {
            for (int i = 0; i < dianas.Length; i++)
            {
                dianas[i].GetComponent<diana>().reseteo();
            }

            StartCoroutine(contador());
        }

    }

    private IEnumerator contador()
    {
        textoInicio.text = "3";
        textoTiempoActual.text = "0";
        yield return new WaitForSeconds(1);
        textoInicio.text = "2";
        yield return new WaitForSeconds(1);
        textoInicio.text = "1";
        yield return new WaitForSeconds(1);
        textoInicio.text = "";
        empezar();
    }

    void empezar()
    {
        numeroDianasRestantes = numeroDianasABatir;
        contadorTiempoActual = 0;
        activo = true;
        levantarDianaAleatoria("");
    }

    public void dianaBajada(string name)
    {
        numeroDianasRestantes -= 1;
        if (numeroDianasRestantes > 0)
        {
            levantarDianaAleatoria(name);
        }
        else
        {
            parar();
        }

    }

    void levantarDianaAleatoria(string ultimoNombre)
    {
        int numAleatorio = Random.Range(0, dianas.Length);
        if (dianas[numAleatorio].name != ultimoNombre)
        {
            dianas[numAleatorio].GetComponent<diana>().subir();
        }
        else
        {
            levantarDianaAleatoria(ultimoNombre);
        }
    }

    void parar()
    {
        activo = false;
        for (int i = 0; i < dianas.Length; i++)
        {
            dianas[i].GetComponent<diana>().reseteo();
        }

        if (contadorTiempoActual < mejorTiempoActual)
        {
            mejorTiempoActual = float.Parse(textoTiempoActual.text);
            mejorTiempo.text = mejorTiempoActual.ToString();
            PlayerPrefs.SetFloat("mejorTiempoDiana", mejorTiempoActual);
        }
    }
}
