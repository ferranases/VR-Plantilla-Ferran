using UnityEngine;

public class objetoCogible : MonoBehaviour
{
    public Vector3 posicionBloqueada;
    public Vector3 rotacionBloqueada;

    public Vector3 posicionBloqueada2;
    public Vector3 rotacionBloqueada2;

    public Transform parent;
    public OVRInput.Controller parentController;

    public int multiplicadorVelocidadSoltar = 1;
    public bool volverPosicionPadre = false;
    public Transform padrePosicion;
    public bool outlineEstado = true;

    private Rigidbody rb;
    private Outline outline;

    private Vector3 inicioPosicion;
    private Quaternion inicioRotacion;
    // Start is called before the first frame update
    void Start()
    {
        parent = null;
        rb = GetComponent<Rigidbody>();
        if (GetComponent<Outline>())
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }
        else
        {
            outline = null;
        }

        inicioPosicion = transform.position;
        inicioRotacion = transform.rotation;
    }

    private void Update()
    {
        if (parent == null && volverPosicionPadre)
        {
            transform.position = padrePosicion.position;
            transform.rotation = padrePosicion.rotation;
        }
    }


    public void coger(OVRInput.Controller parentController, Transform parent)
    {
        rb.isKinematic = true;
        transform.SetParent(parent);

        this.parent = parent;
        this.parentController = parentController;

        if (posicionBloqueada != new Vector3(0, 0, 0))
        {
            if (parentController == OVRInput.Controller.LTouch)
            {
                transform.localPosition = posicionBloqueada;
                transform.localEulerAngles = rotacionBloqueada;
            }
            else
            {
                transform.localPosition = posicionBloqueada2;
                transform.localEulerAngles = rotacionBloqueada2;
            }
        }
        else
        {
            transform.position = parent.position;
        }
        if (outline != null)
        {
            Destroy(outline);
            outline = null;
        }

    }

    public void soltar(Vector3 velocity)
    {
        if (outline == null)
        {

            this.parent = null;
            this.parentController = OVRInput.Controller.None;
            transform.SetParent(null);


            if (posicionBloqueada != new Vector3(0, 0, 0))
            {
                transform.localPosition = transform.position;
                transform.localRotation = transform.rotation;
            }
            if (outlineEstado)
            {
                outline = gameObject.AddComponent<Outline>();
                outline.enabled = false;
            }

            if (volverPosicionPadre)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            else
            {
                rb.isKinematic = false;
                rb.velocity = velocity * multiplicadorVelocidadSoltar;
            }
        }
    }

    //OUTLINE
    public void setOutlineEstado(bool activado)
    {
        if (outline != null)
        {
            outline.enabled = activado;
        }

    }

    //END OUTLINE
}
