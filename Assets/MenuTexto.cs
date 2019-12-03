using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuTexto : MonoBehaviour
{
    private string[][] frases = new string[][] {
        new string[]{
        "Hola bienvenido a pokedragon",
        "Yo sere tu instructor, aparte soy tu tio Ferran",
        "Aqui encima de la mesa hay 3 huevos",
        "Deberas elegir uno, de el nacera el dragon que ira contigo",
        "Nos vemos mas adelante, crack!"},
        new string[]{
        "Espera un momento que no sabes ni que hora es",
        "Te acabo de equipar en tu mano contraria un reloj",
        "y si, da de todo menos la hora, contradictorio,lose",
        "levanta la mano para 'mirar la hora' y con tu mano buena pulsalo",
        "se me olvidaba! ahora vendra tu dragon!",
        },new string[]{
        "Has llegado al mundo de los dragones!",
        "En esta aldea hay una tienda donde podras comprar todo tipo de cosas",
        } };
    public Image[] imagenes;

    public TextMeshProUGUI textoPrincipal;
    public TextMeshProUGUI textoContinuar;

    private string textoAMostrar;
    private int indexTexto;
    private int indexActual;
    private int indexChar;
    private int longitudTotal;

    private bool puedeContinuar = false;

    private void Update()
    {
        if (!GAME.manosOcupadas && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) && puedeContinuar)
        {
            siguienteTexto();
        }
    }
    void activar()
    {
        gameObject.SetActive(false);
        textoPrincipal.text = "";
        textoContinuar.gameObject.SetActive(false);
    }

    public void mostrarTexto(int numero)
    {
        activar();
        GAME.player_movimintoPermitido(false);
        gameObject.SetActive(true);
        indexTexto = numero;
        indexActual = 0;
        longitudTotal = frases[numero].Length;
        StartCoroutine(rutinaMostrarTexto());
    }

    private IEnumerator rutinaMostrarTexto()
    {
        puedeContinuar = false;
        textoPrincipal.text = "";
        textoContinuar.gameObject.SetActive(false);

        textoAMostrar = frases[indexTexto][indexActual];
        for (int i = 0; i < textoAMostrar.Length; i++)
        {
            textoPrincipal.text += textoAMostrar[i];
            yield return new WaitForSeconds(0.015f);
        }

        yield return new WaitForSeconds(0.25f);
        textoContinuar.gameObject.SetActive(true);
        puedeContinuar = true;
    }

    void siguienteTexto()
    {
        if (indexActual + 1 < longitudTotal)
        {
            indexActual++;
            StartCoroutine(rutinaMostrarTexto());
        }
        else
        {
            gameObject.SetActive(false);
            GAME.player_movimintoPermitido(true);
            GAME.subirIndiceTextoGlobal();
        }
    }
}
