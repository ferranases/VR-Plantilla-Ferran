using UnityEngine;

public class Aro : MonoBehaviour
{
    private GameObject personaje;
    // Start is called before the first frame update
    void Start()
    {
        personaje = GameObject.FindObjectOfType<IronMan>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(personaje.transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
