using UnityEngine;
using UnityEngine.SceneManagement;

public class CogerObjetos : MonoBehaviour
{
    public OVRInput.Controller BotonAgarre;
    public int[] layers;

    private GameObject objetoACoger;
    private GameObject objetoCogido;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        objetoACoger = null;
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, BotonAgarre) && objetoCogido == null && objetoACoger != null)
        {
            Coger();
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, BotonAgarre) && objetoCogido != null)
        {
            Soltar();
        }

        if (OVRInput.GetDown(OVRInput.Button.Start, BotonAgarre))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Coger()
    {
        objetoCogido = objetoACoger;
        objetoCogido.GetComponent<objetoCogible>().coger(BotonAgarre, transform.parent);
        boxCollider.enabled = false;
    }

    void Soltar()
    {
        objetoCogido.GetComponent<objetoCogible>().soltar(rb.velocity);
        objetoCogido = null;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        RecogeObjeto(other);
    }

    private void OnTriggerStay(Collider other)
    {
        RecogeObjeto(other);
    }

    private void OnTriggerExit(Collider other)
    {
        objetoACoger = null;
    }

    void RecogeObjeto(Collider other)
    {
        foreach (int layer in layers)
        {
            if (other.gameObject.layer == layer)
            {
                objetoCogible scriptObjetoOther = other.gameObject.GetComponent<objetoCogible>();
                if (scriptObjetoOther.parent == null)
                {
                    objetoACoger = other.gameObject;
                }
            }
        }
    }

}
