using UnityEngine;

public class MENUS : MonoBehaviour
{
    public GameObject menuPreferencias;
    public GameObject menuTexto;

    private void Start()
    {
        menuTexto.SetActive(false);
        menuPreferencias.SetActive(true);
    }
}
