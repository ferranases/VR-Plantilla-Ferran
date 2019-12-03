using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CentralPropulsores : MonoBehaviour
{
    public bool Activo;
    public float gravity = 20.0F;
    public int fuerzaMaximaPropulsor = 200;
    public int multiplicadorVelocidad = 1;
    public Image imagenPotencia;
    public TextMeshPro textoAltura;
    public Transform rodilloAltura;

    public IronManMano[] ironManManos;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 direccionFinal = Vector3.zero;
    CharacterController controller;

    private bool propulsorIzq;
    private bool propulsorDer;

    private float fuerzaPropulsionIzq;
    private float fuerzaPropulsionDer;
    Vector3 direccionIzq;
    Vector3 direccionDer;

    private float alturaInicial;

    private float fuerzaTotal;
    private bool posicionReset;

    private void Start()
    {
        Activo = true;
        controller = GetComponent<CharacterController>();
        propulsorIzq = false;
        propulsorDer = false;
        fuerzaPropulsionIzq = 0;
        fuerzaPropulsionDer = 0;
        direccionDer = Vector3.zero;
        direccionIzq = Vector3.zero;
        posicionReset = false;
        alturaInicial = transform.position.y;

    }
    void Update()
    {
        fuerzaTotal = fuerzaPropulsionDer + fuerzaPropulsionIzq;
        imagenPotencia.fillAmount = fuerzaTotal / (fuerzaMaximaPropulsor * 2f);

        //Altura texto
        textoAltura.text = (transform.position.y - alturaInicial).ToString("F0");

        //Combinar posiciones propulsores
        if (propulsorDer && propulsorIzq)
        {
            moveDirection = direccionDer + direccionIzq;
        }
        else
        {
            if (propulsorIzq)
            {
                moveDirection = direccionIzq + Vector3.forward * 3;
            }
            else if (propulsorDer)
            {
                moveDirection = direccionDer + Vector3.back * 3;
            }
        }

        //Aplicamos la fuerza y la posicion;
        if (propulsorIzq || propulsorDer)
        {
            direccionFinal.x = (moveDirection.x * (fuerzaTotal / 30)) / 10f;
            direccionFinal.z = (moveDirection.z * (fuerzaTotal / 30)) / 10f;
            direccionFinal.y = (moveDirection.y * (fuerzaTotal / 30)) / 10f;
            rodilloAltura.Rotate(Vector3.up * fuerzaTotal / 20f);
            posicionReset = true;
        }

        //Si los propulsores se apagan quitar velocidad de posicion ya que nos hacia arrastrarnos
        if (!propulsorIzq && !propulsorDer && posicionReset)
        {
            posicionReset = false;
        }
        if (!propulsorIzq && !propulsorDer)
        {
            rodilloAltura.Rotate(-Vector3.up * fuerzaTotal / 20f);

            //La velocidad que afecta a la caida y al desplazamiento hacia X e Z
            if (!controller.isGrounded)
            {
                float valorX = (moveDirection.x * fuerzaTotal / 100) / 10;
                float valorZ = (moveDirection.z * fuerzaTotal / 100) / 10;
                if (direccionFinal.x > 0)
                {

                    direccionFinal.x += valorX * Time.deltaTime;
                }
                else
                {
                    direccionFinal.x -= valorX * Time.deltaTime;
                }

                if (direccionFinal.z > 0)
                {
                    direccionFinal.z += valorZ * Time.deltaTime;
                }
                else
                {
                    direccionFinal.z -= valorZ * Time.deltaTime;
                }
            }
        }

        direccionFinal.y -= gravity * Time.deltaTime;

        controller.Move(direccionFinal * Time.deltaTime);

        //Despropulsacion
        if (!propulsorIzq)
        {
            fuerzaPropulsionIzq -= 3;
            if (fuerzaPropulsionIzq < 0) fuerzaPropulsionIzq = 0;
        }
        if (!propulsorDer)
        {
            fuerzaPropulsionDer -= 3;
            if (fuerzaPropulsionDer < 0) fuerzaPropulsionDer = 0;
        }

        if (controller.isGrounded)
        {
            direccionFinal = Vector3.zero;
        }

        //Control del sonido y particulas de cada propulsor
        ironManManos[0].controladorSonido(propulsorIzq, fuerzaPropulsionIzq, fuerzaMaximaPropulsor);
        ironManManos[1].controladorSonido(propulsorDer, fuerzaPropulsionDer, fuerzaMaximaPropulsor);
    }

    public float propulsar(Vector3 direccion, int propulsor)
    {
        if (Activo)
        {
            if (propulsor == 0)
            {
                //fuerzaPropulsionIzq += 1 * multiplicadorVelocidad;
                fuerzaPropulsionIzq = sumarPropulsion(fuerzaPropulsionIzq);
                direccionIzq = direccion;
                if (fuerzaPropulsionIzq > fuerzaMaximaPropulsor) fuerzaPropulsionIzq = fuerzaMaximaPropulsor;
                propulsorIzq = true;
                return fuerzaPropulsionIzq;
            }
            else
            {
                //fuerzaPropulsionDer += 1 * multiplicadorVelocidad;
                fuerzaPropulsionDer = sumarPropulsion(fuerzaPropulsionDer);
                direccionDer = direccion;
                if (fuerzaPropulsionDer > fuerzaMaximaPropulsor) fuerzaPropulsionDer = fuerzaMaximaPropulsor;
                propulsorDer = true;
                return fuerzaPropulsionDer;
            }
        }
        return 0;
    }
    public float despropulsar(int numeroPropulsor)
    {
        if (numeroPropulsor == 0)
        {
            propulsorIzq = false;
            return fuerzaPropulsionIzq;
        }
        else
        {
            propulsorDer = false;
            return fuerzaPropulsionDer;
        }
    }

    float sumarPropulsion(float propulsionActual)
    {
        float propulsionNueva = propulsionActual;
        if (propulsionActual < (fuerzaMaximaPropulsor / 10))
        {
            propulsionNueva += 0.5f;
        }
        else if (propulsionActual < (fuerzaMaximaPropulsor / 6))
        {
            propulsionNueva += 1f;
        }
        else
        {
            propulsionNueva += 2F;
        }

        return propulsionNueva;
    }


}
