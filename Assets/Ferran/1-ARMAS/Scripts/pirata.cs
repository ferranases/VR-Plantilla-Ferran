using System.Collections;
using UnityEngine;

public class pirata : MonoBehaviour
{
    private controlEscenarios controlEscenarios;
    public void golpe()
    {
        controlEscenarios.sumarMuertePirata();
        Destroy(transform.parent.gameObject);
    }

    public void morirDespuesDe(float segundos, controlEscenarios control)
    {
        controlEscenarios = control;
        StartCoroutine(destruirDespuesDe(segundos));

    }
    private IEnumerator destruirDespuesDe(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        controlEscenarios.restarMuertePirata();
        Destroy(transform.parent.gameObject);
        yield break;
    }
}
