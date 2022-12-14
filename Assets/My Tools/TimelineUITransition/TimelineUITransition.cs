using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TimelineUITransition : MonoBehaviour
{
    private static TimelineUITransition instance;

    public static TimelineUITransition Instance
    {
        get
        {
            if (instance == null)
                instance = Instantiate(Resources.Load<TimelineUITransition>(nameof(TimelineUITransition)));

            return instance;
        }
    }

    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] TimelineAsset fadeIn;
    [SerializeField] TimelineAsset fadeOut;
    [SerializeField] Gradient fadeInGradient;
    [SerializeField] Gradient fadeOutGradient;
    [SerializeField] Image backgroundImage;

    PlayableDirector director;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            if(instance == null)
                instance = this;

            DontDestroyOnLoad(this.gameObject);

            director = GetComponent<PlayableDirector>();
        }
    }

    /// <summary>
    /// Comienza la animacion del Fade y se le puede pasar un metodo como parametro
    /// </summary>
    /// <param name="action"></param>
    IEnumerator FadeStart(string sceneName, float speed = 1f)
    {
        textMesh.text = $"0%";
        director.Play(fadeOut);
        director.playableGraph.GetRootPlayable(0).SetSpeed(speed);

        while (director.time < director.duration)
        {
            backgroundImage.color = fadeInGradient.Evaluate((float)(director.time / director.duration));
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            textMesh.text = $"{asyncLoad.progress * 100:0}%";
            yield return null;
        }

        textMesh.text = $"100%";

        director.Play(fadeIn);
        director.playableGraph.GetRootPlayable(0).SetSpeed(speed);

        while (director.time < director.duration)
        {
            backgroundImage.color = fadeOutGradient.Evaluate((float)(director.time / director.duration));
            yield return null;
        }
    }

    public void FadeStart(string sceneName, float speed = 1f, Gradient fadeInGradient = null, Gradient fadeOutGradient = null)
    {
        if(fadeInGradient != null) this.fadeInGradient = fadeInGradient;
        if (fadeOutGradient != null) this.fadeOutGradient = fadeOutGradient;

        StartCoroutine(FadeStart(sceneName, speed));
    }
}