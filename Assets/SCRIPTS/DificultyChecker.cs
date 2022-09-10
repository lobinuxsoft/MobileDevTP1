using CryingOnionTools.ScriptableVariables;
using UnityEngine;

public class DificultyChecker : MonoBehaviour
{
    [SerializeField] IntVariable dificultyVariable;

    [SerializeField] GameObject[] dificultyObjects;

    // Start is called before the first frame update
    void Start()
    {
        dificultyVariable.Value = Mathf.Clamp(dificultyVariable.Value, 0, dificultyObjects.Length);

        for (int i = 0; i < dificultyVariable.Value; i++)
        {
            dificultyObjects[i].SetActive(true);
        }
    }
}