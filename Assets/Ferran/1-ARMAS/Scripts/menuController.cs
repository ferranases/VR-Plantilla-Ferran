using System.Collections;
using TMPro;
using UnityEngine;

public class menuController : MonoBehaviour
{
    public controlPuerta controlPuerta;
    public TextMeshPro textoStart;
    public TextMeshPro textoRecord;
    private Animator animator;
    private OVRPlayerController OVRPlayerController;
    private controlEscenarios controlEscenarios;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        OVRPlayerController = GameObject.FindObjectOfType<OVRPlayerController>();
        controlEscenarios = GameObject.FindObjectOfType<controlEscenarios>();
        textoStart.text = "";
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            animator.SetBool("menuSubir", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            animator.SetBool("menuSubir", false);
        }
    }

    public void botonStartAccionar()
    {
        animator.SetBool("menuSubir", false);
        OVRPlayerController.EnableLinearMovement = false;
        StartCoroutine(cuentaAtras());
    }
    private IEnumerator cuentaAtras()
    {
        textoStart.text = "3";
        audioSource.Play();
        yield return new WaitForSeconds(1);
        textoStart.text = "2";
        audioSource.Play();
        yield return new WaitForSeconds(1);
        textoStart.text = "1";
        audioSource.Play();
        yield return new WaitForSeconds(1);
        textoStart.text = "";
        controlEscenarios.startGame();
    }

    public void setTextoCuentaAtras(string textoNuevo)
    {
        animator.SetBool("menuSubir", true);
        OVRPlayerController.EnableLinearMovement = true;
        textoStart.text = textoNuevo;
    }

    public void cambiarRecord(int recordActual, int numeroEnemigos)
    {
        if (numeroEnemigos != 0)
        {
            textoRecord.text = recordActual.ToString() + "/" + numeroEnemigos.ToString();
        }
        else
        {
            textoRecord.text = "";
        }

    }
}
