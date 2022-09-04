using UnityEngine;

public abstract class GameManagerStategy : ScriptableObject
{
    public abstract void InitTutorial(Player[] players, GameObject[] objsCalibracion, GameObject[] objsCarrera);

    public abstract void StartRace(Player[] players);

    public abstract void EndRace(Player[] players);

    //cambia a modo de carrera
    public abstract void ChangeToRace(Player[] players, GameObject[] objsCalibracion, GameObject[] objsCarrera, Vector3[] raceTrukPos);

    public abstract bool EndCalibration(int playerID, Player[] players);
}
