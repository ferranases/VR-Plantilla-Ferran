using UnityEngine;

public class Flecha : MonoBehaviour
{
    public OVRInput.Controller controlador;

    private Arco arco;
    private bool flechaColisionMano;
    private bool flechaEnCuerda;
    private bool lanzada = false;
    private bool colisionada = false;
    private Rigidbody rb;

    private void Start()
    {
        arco = GameObject.FindObjectOfType<Arco>();
        flechaEnCuerda = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!lanzada)
        {
            if (flechaColisionMano && flechaEnCuerda)
            {
                arco.setManoEnFlecha(true);
            }
            else
            {
                arco.setManoEnFlecha(false);
            }
        }
    }


    void FixedUpdate()
    {
        RayCastColision();
    }

    void RayCastColision()
    {
        // Does the ray intersect any objects excluding the player layer
        if (!colisionada && lanzada && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out RaycastHit hit, 3))
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
            if (hit.distance < 0.4f)
            {
                stopArrow(hit.transform);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Si tocamos el centro de el arco ponemos la flecha en posicion
        if (other.transform.tag == "centroCuerda" && !lanzada)
        {
            arco.cambiarFlechaAArco();
            flechaEnCuerda = true;
        }

        //Si notamos una mano cerca preparamos el poder tirar hacia atra la mano y cargar la cuerda
        if (other.transform.gameObject.name == "CogerObjetos" && !lanzada)
        {
            flechaColisionMano = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "centroCuerda" && !lanzada)
        {
            arco.cambiarFlechaAArco();
            flechaEnCuerda = true;
        }
    }


    public void disparar(float fuerza)
    {
        lanzada = true;
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        transform.SetParent(null);
        rb.AddForce(transform.up * fuerza);
    }

    ////Pequeña colision fisica que se activa al colisionar con objetos
    ////haremos que la flecha se quede clavada tal cual ha tocado el objeto colisionado
    //private void OnCollisionEnter(Collision collision)
    //{


    //    //rb.isKinematic = true;
    //    //rb.velocity = Vector3.zero;
    //    //rb.angularVelocity = Vector3.zero;

    //    //rb.constraints = RigidbodyConstraints.FreezeAll;

    //    //rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

    //    transform.SetParent(collision.transform);
    //    Destroy(gameObject, 5);
    //}

    void stopArrow(Transform objeto)
    {
        colisionada = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(objeto.transform);
        Destroy(gameObject, 5);
    }
}
