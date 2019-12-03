using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class botonMenuPrincipal : MonoBehaviour
{
    public int numeroBoton;
    public MenuReloj menuReloj;

    [Space(3)]
    [Header("Cambiar de color")]
    public Image sprite;
    public TextMeshProUGUI texto;

    private Animation animation;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "dedo" && !menuReloj.getBotonesBloqueados())
        {
            animation.Play("botonPulsado");
            if (sprite != null)
            {
                sprite.color = Color.yellow;
            }
            else if (texto != null)
            {
                texto.color = Color.yellow;
            }
            GAME.vibrarMano(30, 1, 30);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "dedo" && !menuReloj.getBotonesBloqueados())
        {
            animation.Play("botonDespulsado");

            if (sprite != null)
            {
                sprite.color = Color.white;
            }
            else if (texto != null)
            {
                texto.color = Color.white;
            }
            GAME.vibrarMano(30, 2, 60);
            menuReloj.cambiarMenu(numeroBoton);
        }

    }
}
