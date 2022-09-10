using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CryingOnionTools.ScriptableVariables;
using UnityEngine.InputSystem;
using System.Globalization;
using CryingOnionTools.AudioTools;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textP1;
    [SerializeField] private TextMeshProUGUI textP2;
    [SerializeField] private Image playerWinner;
    [SerializeField] private UIntVariable moneyP1;
    [SerializeField] private UIntVariable moneyP2;
    [SerializeField] private Sprite spriteP1Win;
    [SerializeField] private Sprite spriteP2Win;
    [SerializeField] private float fadeTime = 0.5f;
    [SerializeField] AudioClip congratsSfx;
    [SerializeField] AudioClip confirmSfx;

    private bool p1Win;
    private float onTime;
    private bool upperTime;

    private SimpleSFXRequest sfxRequest;

    private void Awake() => sfxRequest = GetComponent<SimpleSFXRequest>();

    void Start()
    {
        textP1.text = $"${moneyP1.Value:N0}";
        textP2.text = $"${moneyP2.Value:N0}";

        p1Win = moneyP1.Value > moneyP2.Value;

        playerWinner.sprite = p1Win ? spriteP1Win : spriteP2Win;

        sfxRequest.PlaySFX(congratsSfx);
    }

    void Update()
    {
        if (upperTime)
        {
            onTime += Time.deltaTime;
            if (onTime > fadeTime)
            {
                upperTime = false;
                onTime = fadeTime;
            }
        }
        else
        {
            onTime -= Time.deltaTime;
            if (onTime < 0)
            {
                upperTime = true;
                onTime = 0;
            }
        }

        float lerp = onTime / fadeTime;
        playerWinner.color = Color.Lerp(Color.white, Color.blue, lerp);
    }

    public void ToMainMenu()
    {
        sfxRequest.PlaySFX(confirmSfx);
        moneyP1.EraseData();
        moneyP2.EraseData();
        SceneManager.LoadScene("MainMenu");
    }

    public void ToMainMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToMainMenu();
        }
    }

    public void QuitApplication(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moneyP1.EraseData();
            moneyP2.EraseData();
            Application.Quit();
        }
    }
}