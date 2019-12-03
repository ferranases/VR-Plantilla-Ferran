using UnityEngine;

public static class GAME
{
    public static bool zurdo = true;
    public static bool manosOcupadas = false;
    public static int textoIndexGlobal = 0;

    public static int historia = 0;

    private static Partida partida;

    //PLAYER    

    public static int player_dinero;

    public static string player_position;
    public static string player_rotation;

    public static bool player_arco = false;
    public static bool player_pico = false;
    public static int player_cantidadFlechasMax;
    public static int player_cantidadFlechasActual;

    public static int player_cantidadCarneMax;
    public static int player_cantidadCarneActual;

    //DRAGONES
    public static bool dragonActivo = true;
    public static bool dragonVisible = true;
    public static GameObject dragonActualObj;
    public static float dragonAge = 0.15f;
    public static bool dragonMontura = false;
    public static int dragonInicial;


    // Debug.Log("<color=purple>aaaaaaa</color>");

    public static void START_GAME(Partida partidaNueva)
    {
        partida = partidaNueva;
        if (PlayerPrefs.HasKey("zurdo"))
        {
            zurdo = bool.Parse(PlayerPrefs.GetString("zurdo"));
            textoIndexGlobal = PlayerPrefs.GetInt("textoIndexGlobal");
            dragonInicial = PlayerPrefs.GetInt("dragonPrincipal");
            historia = PlayerPrefs.GetInt("historia");
            dragonAge = PlayerPrefs.GetFloat("dragonAge");
            dragonMontura = bool.Parse(PlayerPrefs.GetString("dragonMontura"));
            player_position = PlayerPrefs.GetString("playerPosition");
            player_rotation = PlayerPrefs.GetString("playerRotation");
            player_arco = bool.Parse(PlayerPrefs.GetString("arco"));
            player_pico = bool.Parse(PlayerPrefs.GetString("pico"));
            //Prueba
            //dragonMontura = true;
        }
        else
        {
            PlayerPrefs.SetInt("textoIndexGlobal", 0);
            PlayerPrefs.SetInt("dragonPrincipal", -1);
            PlayerPrefs.SetInt("historia", 0);
            PlayerPrefs.SetFloat("dragonAge", 0.15f);
            PlayerPrefs.SetString("dragonMontura", "false");
            PlayerPrefs.SetString("playerPosition", "1,2$-0,66$-1,4");
            PlayerPrefs.SetString("playerRotation", "0$-90$0");
            PlayerPrefs.SetString("arco", "false");
            PlayerPrefs.SetString("pico", "false");

            //Dragones Legendarios
            PlayerPrefs.SetInt("dragonLegendario1", 0);
            PlayerPrefs.SetInt("dragonLegendario2", 0);
            PlayerPrefs.SetInt("dragonLegendario3", 0);
        }
    }

    public static void player_movimintoPermitido(bool valor)
    {
        GameObject.FindObjectOfType<OVRPlayerController>().EnableLinearMovement = valor;
    }

    public static void setZurdo(bool valor)
    {
        zurdo = valor;
        PlayerPrefs.SetString("zurdo", valor.ToString());

        if (GameObject.FindObjectOfType<Reloj>())
        {
            GameObject.FindObjectOfType<Reloj>().ponerEnMano(valor);
        }
    }

    public static void subirIndiceTextoGlobal()
    {
        GAME.textoIndexGlobal++;
        PlayerPrefs.SetInt("textoIndexGlobal", textoIndexGlobal);
    }

    public static void seleccionarDragonPrincipal(int valor)
    {
        PlayerPrefs.SetInt("dragonPrincipal", valor);
        dragonInicial = valor;
        subirNivelHistoria();
    }
    public static void subirNivelHistoria()
    {
        historia++;
        PlayerPrefs.SetInt("historia", historia);
    }

    public static void guardaPosicionPlayer()
    {
        PlayerPrefs.SetString("playerPosition",
            partida.getPlayerCazadragones().transform.position.x + "$" +
            partida.getPlayerCazadragones().transform.position.y + "$" +
            partida.getPlayerCazadragones().transform.position.z);

        PlayerPrefs.SetString("playerRotation",
            partida.getPlayerCazadragones().transform.rotation.x + "$" +
            partida.getPlayerCazadragones().transform.rotation.y + "$" +
            partida.getPlayerCazadragones().transform.rotation.z);
    }

    public static void vibrarMano(int uno, int dos, int tres)
    {
        if (zurdo)
        {
            VibracionManager.vibracion(uno, dos, tres, OVRInput.Controller.LTouch);
        }
        else
        {
            VibracionManager.vibracion(uno, dos, tres, OVRInput.Controller.RTouch);
        }
    }

    public static void vibrarManoContraria(int uno, int dos, int tres)
    {
        if (zurdo)
        {
            VibracionManager.vibracion(uno, dos, tres, OVRInput.Controller.RTouch);
        }
        else
        {
            VibracionManager.vibracion(uno, dos, tres, OVRInput.Controller.LTouch);
        }
    }

    public static void activarArco()
    {
        player_arco = true;
        PlayerPrefs.SetString("arco", true.ToString());
    }
    public static void activarPico()
    {
        player_pico = true;
        PlayerPrefs.SetString("pico", true.ToString());
    }

    //DRAGONES
    public static void dragonAumentarEdad()
    {
        dragonAge += 0.1f;
        if (dragonAge > 1)
        {
            dragonAge = 1;
        }
        PlayerPrefs.SetFloat("dragonAge", dragonAge);
    }

    public static void desbloquearMontura()
    {
        PlayerPrefs.SetString("dragonMontura", "true");
        dragonMontura = true;
    }

    public static void setDragonVisible(bool valorActivo, bool valorVisible)
    {
        dragonActivo = valorActivo;
        dragonVisible = valorVisible;
        if (partida.getDragon() != null)
        {
            partida.getDragon().setDragonVisible(valorActivo, valorVisible);
        }
    }

    public static void desbloquearDragonLegendario(int numero)
    {
        PlayerPrefs.SetInt("dragonLegendario" + numero, 1);
    }

}
