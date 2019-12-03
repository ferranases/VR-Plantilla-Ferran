using UnityEngine;
using UnityEngine.AI;

public class seguirVistaPersonaje : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent nav;


    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nav.destination = target.transform.position;
    }
}
