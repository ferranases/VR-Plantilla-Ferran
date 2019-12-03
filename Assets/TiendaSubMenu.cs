using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaSubMenu : MonoBehaviour
{

    public TiendaUI tienda;
    public TextMeshProUGUI titulo;
    public Image imagen;
    public AudioClip[] clips;

    private AudioSource audioSource;
    private TiendaObjetoUI objeto;
    private bool compraPosible;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void activar(TiendaObjetoUI objetoNuevo)
    {
        objeto = objetoNuevo;
        gameObject.SetActive(true);
        titulo.text = objeto.titulo;
        imagen.sprite = objeto.imagen.sprite;
        checkSiPuedesComprarlo();
    }

    public void desactivar()
    {
        tienda.reset();
    }

    void checkSiPuedesComprarlo()
    {
        compraPosible = true;
        for (int i = 0; i < objeto.costes.Length; i++)
        {
            if (!Inventario.instancia.checkCantidadObjeto((int)objeto.costes[i].tipo, objeto.costes[i].cantidad))
            {
                compraPosible = false;
                break;
            }
        }
    }

    public void comprar()
    {
        if (compraPosible)
        {

            audioSource.clip = clips[0];
            audioSource.Play();

            switch (objeto.tipo)
            {
                case TiendaUI.tipo.pico:
                    ConsolaGrafica.instancia.crearTexto("Pico Desbloqueado", Color.cyan);
                    GAME.activarPico();
                    GameObject.FindObjectOfType<PlayerCazadragones>().checkPico();
                    break;

                case TiendaUI.tipo.flechaNormal:
                    ConsolaGrafica.instancia.crearTexto("+5 Flechas Normales", Color.cyan);
                    Inventario.instancia.addCantity((int)Inventario.tipos.flecha, 5);
                    break;

                case TiendaUI.tipo.flechaVeneno:
                    ConsolaGrafica.instancia.crearTexto("+5 Flechas Venenosas", Color.cyan);
                    Inventario.instancia.addCantity((int)Inventario.tipos.flechaVenenosa, 5);
                    break;

                case TiendaUI.tipo.arco:
                    ConsolaGrafica.instancia.crearTexto("Arco Desbloqueado", Color.cyan);
                    GAME.activarArco();
                    GameObject.FindObjectOfType<PlayerCazadragones>().checkArco();
                    break;

                case TiendaUI.tipo.montura:
                    ConsolaGrafica.instancia.crearTexto("MonturaDesbloqueada", Color.cyan);
                    GAME.desbloquearMontura();
                    GameObject.FindObjectOfType<Dragon>().checkColocarMontura();
                    break;
            }

            //Quitar cantidad materiales
            for (int i = 0; i < objeto.costes.Length; i++)
            {
                Inventario.instancia.removeCantity((int)objeto.costes[i].tipo, objeto.costes[i].cantidad);
            }

            StartCoroutine(rutinaDesactivar());
        }
        else
        {
            audioSource.clip = clips[1];
            audioSource.Play();

            if (GAME.zurdo)
            {
                VibracionManager.vibracion(50, 2, 100, OVRInput.Controller.RTouch);
            }
            else
            {
                VibracionManager.vibracion(50, 2, 100, OVRInput.Controller.LTouch);
            }


            Debug.Log("<color=red>NO</color>");
        }
    }

    private IEnumerator rutinaDesactivar()
    {
        yield return new WaitForSeconds(0.75f);
        desactivar();
        yield break;
    }
}
