using UnityEngine;
using UnityEngine.SceneManagement;

public class volverBaseIronMan : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("BASE");
    }
}
