using UnityEngine;

public class colisionadorEventos : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.name.Contains("CTEXTO"))
        {
            other.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
            char caracterNumerico = other.transform.gameObject.name[other.transform.gameObject.name.Length - 1];
            int numeroTexto = int.Parse(caracterNumerico.ToString());

            if (numeroTexto >= GAME.textoIndexGlobal)
            {
                GameObject.FindObjectOfType<MENUS>().menuTexto.GetComponent<MenuTexto>().mostrarTexto(numeroTexto);
            }
        }
    }
}
