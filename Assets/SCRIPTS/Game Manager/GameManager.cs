using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using UnityEngine.InputSystem;
using CryingOnionTools.AudioTools;
using TMPro;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instancia;

    [SerializeField] private GameManagerStategy strategy;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public float TiempEspMuestraPts = 3;
    public int ConteoParaInicion = 3;
    [SerializeField] AudioClip hurrySfx;
    [SerializeField] AudioClip timeOverSfx;
    [SerializeField] AudioClip[] sfxCount;

    public Rect ConteoPosEsc;
    public TextMeshProUGUI ConteoInicio;
    public TextMeshProUGUI TiempoDeJuegoText;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion;

    public Player[] players;

    //la pista de carreras
    public GameObject[] ObjsCarrera;

    bool ConteoRegresivo = true;

    SimpleSFXRequest simpleSFX;
    Coroutine countDownRutine = null;
    Coroutine hurryRoutine = null;
    Coroutine gameoverRoutine = null;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];

    void Awake()
    {
        GameManager.Instancia = this;

        simpleSFX = GetComponent<SimpleSFXRequest>();
    }

    IEnumerator Start()
    {
        yield return null;
        IniciarTutorial();
    }

    void Update() {

        switch (EstAct) {

            case EstadoJuego.Jugando:

                if (TiempoDeJuego <= 0) {
                    FinalizarCarrera();
                }

                if (!ConteoRegresivo)
                {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                    TiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                    if (TiempoDeJuego < 15 && hurryRoutine == null)
                        hurryRoutine = StartCoroutine(HurryRoutine());
                        
                }
                else
                {
                    if (countDownRutine == null)
                        countDownRutine = StartCoroutine(CountDownRoutine(ConteoParaInicion));
                }

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                {
                     TimelineUITransition.Instance.FadeStart("Gameover");
                    this.enabled = false;
                }

                break;
        }

        TiempoDeJuegoText.transform.parent.gameObject.SetActive(EstAct == EstadoJuego.Jugando && !ConteoRegresivo);
    }

    public void IniciarTutorial()
    {
        strategy.InitTutorial(players, ObjsCalibracion, ObjsCarrera);
        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);
    }

    void EmpezarCarrera() => strategy.StartRace(players);

    void FinalizarCarrera()
    {
        StartCoroutine(TimeOverRoutine());
        EstAct = GameManager.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        strategy.EndRace(players);
    }

    //cambia a modo de carrera
    void CambiarACarrera()
    {
        EstAct = GameManager.EstadoJuego.Jugando;
        TiempoDeJuegoText.transform.parent.gameObject.SetActive(false);
        ConteoInicio.gameObject.SetActive(false);

        strategy.ChangeToRace(players, ObjsCalibracion, ObjsCarrera, PosCamionesCarrera);
    }

    public void FinCalibracion(int playerID)
    {
        if (strategy.EndCalibration(playerID, players))
            CambiarACarrera();
    }

    IEnumerator CountDownRoutine(int duration)
    {
        ConteoInicio.gameObject.SetActive(true);
        while (duration > 0)
        {
            simpleSFX.PlaySFX(sfxCount[duration]);
            ConteoInicio.text = duration.ToString("0");
            yield return new WaitForSeconds(1);
            duration--;
        }

        simpleSFX.PlaySFX(sfxCount[duration]);
        ConteoInicio.text = "GO";
        EmpezarCarrera();
        ConteoRegresivo = false;
        ConteoInicio.gameObject.SetActive(false);
    }

    IEnumerator HurryRoutine()
    {
        simpleSFX.PlaySFX(hurrySfx);
        yield return new WaitForEndOfFrame();
    }

    IEnumerator TimeOverRoutine()
    {
        simpleSFX.PlaySFX(timeOverSfx);
        yield return new WaitForEndOfFrame();
    }

    public void SelectPlayerByID(int index) => players[index].Seleccionado = true;

    public void Player1Selected(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectPlayerByID(0);
    }

    public void Player2Selected(InputAction.CallbackContext context)
    {
        if (context.performed)
            SelectPlayerByID(1);
    }

    public void SkipRace(InputAction.CallbackContext context)
    {
        if (context.performed)
            TiempoDeJuego = 0;
    }

    public void ReLoadLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
            TimelineUITransition.Instance.FadeStart(SceneManager.GetActiveScene().name);
    }

    public void CloseApplication(InputAction.CallbackContext context)
    {
        if (context.performed)
            Application.Quit();
    }
}