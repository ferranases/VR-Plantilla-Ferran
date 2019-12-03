using UnityEngine;

public class Cuerda : MonoBehaviour
{
    public Transform destino;
    private LineRenderer lineRender;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.useWorldSpace = true;
    }

    void Update()
    {

        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, destino.position);
    }
}
