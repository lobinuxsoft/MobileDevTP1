using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake() => canvasGroup = GetComponent<CanvasGroup>();

    public void Pause()
    {
        if(Time.timeScale > 0)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }
    }

    private void ShowMenu()
    {
        Time.timeScale = 0;

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void HideMenu()
    {
        Time.timeScale = 1;

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    public void ReturnToMainMenu()
    {
        HideMenu();
        TimelineUITransition.Instance.FadeStart("MainMenu");
    }
}