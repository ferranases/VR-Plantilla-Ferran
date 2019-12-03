using UnityEngine;

public class objetoControlHistoria : MonoBehaviour
{
    public int puntoHistoria;
    public Collider colisionador;

    // Update is called once per frame
    void Update()
    {
        if (GAME.historia < puntoHistoria)
        {
            colisionador.enabled = false;
        }
        else
        {
            colisionador.enabled = true;
        }
    }
}
