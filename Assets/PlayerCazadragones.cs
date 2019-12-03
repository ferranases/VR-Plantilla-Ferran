using UnityEngine;
using UnityEngine.AI;

public class PlayerCazadragones : MonoBehaviour
{
    public Dragon dragon;
    public Transform posicionDragon;
    public GameObject Arco;
    public GameObject Pico;

    private OVRPlayerController OVRplayerController;
    private CharacterController characterController;

    private bool montado = false;
    private GameObject arcoActual = null;
    private GameObject picoActual = null;

    void Start()
    {
        OVRplayerController = GetComponent<OVRPlayerController>();
        characterController = GetComponent<CharacterController>();
        //UnityEngine.XR.InputTracking.Recenter();

        if (PlayerPrefs.HasKey("playerPosition"))
        {
            string[] posicion = PlayerPrefs.GetString("playerPosition").Split('$');
            transform.position = new Vector3(float.Parse(posicion[0]), float.Parse(posicion[1]), float.Parse(posicion[2]));
            setPositionMesh(transform.position);

            string[] rotacion = PlayerPrefs.GetString("playerRotation").Split('$');
            transform.rotation = Quaternion.Euler(float.Parse(rotacion[0]), float.Parse(rotacion[1]), float.Parse(rotacion[2]));
        }

        checkArco();
        checkPico();
    }


    private void Update()
    {
        if (montado)
        {
            transform.localPosition = dragon.posicionMonturaEnDragon.localPosition;
            transform.localRotation = dragon.posicionMonturaEnDragon.localRotation;
        }

        if (transform.position.y < -10)
        {
            string[] posicion = PlayerPrefs.GetString("playerPosition").Split('$');
            transform.position = new Vector3(float.Parse(posicion[0]), float.Parse(posicion[1]), float.Parse(posicion[2]));
            setPositionMesh(transform.position);
        }
    }

    public void montar()
    {
        montado = true;

        transform.SetParent(dragon.gameObject.transform);

        OVRplayerController.EnableLinearMovement = false;
        OVRplayerController.GravityModifier = 0;
        OVRplayerController.HmdRotatesY = false;
    }

    public void desmontar()
    {
        montado = false;

        transform.SetParent(null);

        OVRplayerController.EnableLinearMovement = true;
        OVRplayerController.GravityModifier = 1;
        OVRplayerController.HmdRotatesY = true;
    }

    public void checkArco()
    {
        if (GAME.player_arco && arcoActual == null)
        {
            arcoActual = Instantiate(Arco);
        }
    }

    public void checkPico()
    {
        if (GAME.player_pico && picoActual == null)
        {
            picoActual = Instantiate(Pico);
            picoActual.GetComponent<objetoCogible>().padrePosicion = GameObject.Find("posicionPico").transform;
        }
    }
    public void setPositionMesh(Vector3 posicionAntigua)
    {
        NavMesh.SamplePosition(posicionAntigua, out NavMeshHit hit, 5, 1);
        transform.position = hit.position + new Vector3(0, 2, 0);
    }
}
