using UnityEngine;

[CreateAssetMenu(menuName = "Game Manager/ Strategy/ Multiplayer")]
public class MultiplayerStrategy : GameManagerStategy
{
    public override bool EndCalibration(int playerID, Player[] players)
    {
        players[playerID].FinTuto = true;

        foreach (var player in players)
        {
            if(!player.FinTuto)
                return false;
        }

        return true;
    }

    public override void InitTutorial(Player[] players, GameObject[] objsCalibracion, GameObject[] objsCarrera)
    {
        for (int i = 0; i < objsCalibracion.Length; i++)
        {
            objsCalibracion[i].SetActive(true);
        }

        for (int i = 0; i < objsCarrera.Length; i++)
        {
            objsCarrera[i].SetActive(false);
        }

        foreach (var player in players)
        {
            player.CambiarATutorial();
        }
    }

    public override void StartRace(Player[] players)
    {
        foreach (var player in players)
        {
            player.GetComponent<Frenado>().RestaurarVel();
            player.GetComponent<CarController>().TurnEnable();
        }
    }

    public override void ChangeToRace(Player[] players, GameObject[] objsCalibracion, GameObject[] objsCarrera, Vector3[] raceTrukPos)
    {
        for (int i = 0; i < objsCarrera.Length; i++)
        {
            objsCarrera[i].SetActive(true);
        }

        //desactivacion de la calibracion
        foreach (var player in players)
        {
            player.FinCalibrado = true;
        }

        foreach (var item in objsCalibracion)
        {
            item.SetActive(false);
        }

        //posiciona los camiones dependiendo de que lado de la pantalla esten
        if (players[0].LadoActual == Visualizacion.Lado.Izq)
        {
            players[0].gameObject.transform.position = raceTrukPos[0];
            players[1].gameObject.transform.position = raceTrukPos[1];
        }
        else
        {
            players[0].gameObject.transform.position = raceTrukPos[1];
            players[1].gameObject.transform.position = raceTrukPos[0];
        }

        foreach (var player in players)
        {
            player.transform.forward = Vector3.forward;
            player.GetComponent<Frenado>().Frenar();
            player.CambiarAConduccion();
            player.GetComponent<Frenado>().RestaurarVel();
            player.GetComponent<CarController>().TurnDisable();
        }
    }

    public override void EndRace(Player[] players)
    {
        foreach (var player in players)
        {
            player.GetComponent<Frenado>().Frenar();
            player.ContrDesc.FinDelJuego();
        }
    }
}