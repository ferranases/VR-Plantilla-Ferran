using UnityEngine;

public class Cinturon : MonoBehaviour
{
    public Transform trackingSpace;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, trackingSpace.rotation.z, transform.rotation.w);
    }
}
