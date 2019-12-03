using UnityEngine;

public class Partida : MonoBehaviour
{

    public GameObject dragonPrefab;

    public Material[] skinsDragones;
    public Material[] skinsHuevos;
    public Material[] skinsOjos;
    public Material[] skinsGuantes;


    private PlayerCazadragones playerCazadragones;
    private Dragon dragon = null;

    private void Awake()
    {
        GAME.START_GAME(this);
        playerCazadragones = GameObject.FindObjectOfType<PlayerCazadragones>();
    }


    private void Start()
    {
        if (GAME.historia >= 2)
        {
            crearDragonActual();
        }
    }

    public void crearDragonActual()
    {
        if (GAME.dragonActualObj == null)
        {
            GameObject nuevoDragon = Instantiate(dragonPrefab);
            GAME.dragonActualObj = nuevoDragon;
            dragon = nuevoDragon.GetComponent<Dragon>();
            dragon.Player = playerCazadragones.gameObject.transform;
            dragon.target = playerCazadragones.posicionDragon;

            nuevoDragon.transform.localScale = new Vector3(GAME.dragonAge, GAME.dragonAge, GAME.dragonAge);
            //nuevoDragon.transform.position = new Vector3(dragon.target.position.x, playerCazadragones.transform.position.y, dragon.target.position.z);

            playerCazadragones.dragon = dragon;
        }
        dragon.construirDragon(skinsDragones[GAME.dragonInicial], skinsOjos[GAME.dragonInicial]);
    }

    public Dragon getDragon()
    {
        if (dragon == null)
        {
            return null;
        }
        else
        {
            return dragon;
        }

    }

    public PlayerCazadragones getPlayerCazadragones()
    {
        return playerCazadragones;
    }
    public Material getSkinDragon(int indice)
    {
        return skinsDragones[indice];
    }
    public Material getSkinOjos(int indice)
    {
        return skinsOjos[indice];
    }
}
