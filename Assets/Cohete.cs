using UnityEngine;
using UnityEngine.SceneManagement;

public class Cohete : MonoBehaviour
{
    public enum escena { BASE = 0, Armas = 1, IronMan = 2, Dragones = 3 }
    public Cohete.escena escenaCargar;
    public ParticleSystem[] particulas;
    private new ConstantForce constantForce;

    private bool activado = false;

    private void Start()
    {
        constantForce = GetComponent<ConstantForce>();
    }

    private void Update()
    {
        if (transform.position.y > 15 && !activado)
        {
            cambiarEscena();
        }
    }
    public void activar()
    {
        for (int i = 0; i < particulas.Length; i++)
        {
            particulas[i].Play();
        }
        constantForce.force = new Vector3(0, 15, 0);
    }

    void cambiarEscena()
    {
        activado = true;
        string nombre = "";
        switch ((int)escenaCargar)
        {
            case 0:
                nombre = "BASE";
                break;
            case 1:
                nombre = "Armas";
                break;
            case 2:
                nombre = "IronMan";
                break;
            case 3:
                nombre = "Dragones";
                break;
        }

        SceneManager.LoadScene(nombre);
    }
}
