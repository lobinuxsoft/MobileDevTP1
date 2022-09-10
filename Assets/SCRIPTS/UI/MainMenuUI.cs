using CryingOnionTools.ScriptableVariables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] IntVariable dificultyVariable;
    [SerializeField] UIntVariable scoreP1;
    [SerializeField] UIntVariable scoreP2;
    [SerializeField] Slider dificultySlider;
    [SerializeField] Button singlePlayerButton;
    [SerializeField] Button multiPlayerButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        dificultyVariable.Value = Mathf.Clamp(Mathf.RoundToInt(dificultyVariable.Value), 1, 3);
        dificultySlider.SetValueWithoutNotify(dificultyVariable.Value);
        dificultySlider.onValueChanged.AddListener(OnChangeDificulty);

        singlePlayerButton.onClick.AddListener(ToSinglePlayer);
        multiPlayerButton.onClick.AddListener(ToMultiPlayer);
        quitButton.onClick.AddListener(QuitGame);
    }


    public void ToSinglePlayer()
    {
        scoreP1.Value = 0;
        scoreP2.Value = 0;
        SceneManager.LoadScene("Gameplay-Singleplayer");
    }

    public void ToMultiPlayer()
    {
        scoreP1.Value = 0;
        scoreP2.Value = 0;
        SceneManager.LoadScene("Gameplay-Multiplayer");
    }

    public void QuitGame() => Application.Quit();

    public void OnChangeDificulty(float value) => dificultyVariable.Value = Mathf.Clamp(Mathf.RoundToInt(value), 1, 3);
}