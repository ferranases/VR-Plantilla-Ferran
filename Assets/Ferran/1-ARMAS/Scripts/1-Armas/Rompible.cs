using UnityEngine;

public class Rompible : MonoBehaviour
{

    public GameObject prefabRoto;
    public AudioSource audioSource;


    public void cambiar()
    {
        GameObject instanciaRota = Instantiate(prefabRoto, transform.position, transform.rotation);
        instanciaRota.transform.localScale = transform.localScale;
        Destroy(instanciaRota, 10);

        audioSource.Play();

        //MiniExplosion

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 0.2f);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(1, explosionPos, 0.2f, 3.0F);
        }
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        Destroy(gameObject, audioSource.clip.length);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 11)
        {
            cambiar();
        }

    }

}
