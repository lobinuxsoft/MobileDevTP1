using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instancia;

    [SerializeField] private GameManagerStategy strategy;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Calibrando;

    public float TiempEspMuestraPts = 3;
    public float ConteoParaInicion = 3;

    public Rect ConteoPosEsc;
    public Text ConteoInicio;
    public Text TiempoDeJuegoText;

    //listas de GO que activa y desactiva por sub-escena
    //escena de tutorial
    public GameObject[] ObjsCalibracion;

    public Player[] players;

    //la pista de carreras
    public GameObject[] ObjsCarrera;

    bool ConteoRegresivo = true;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];

    void Awake() => GameManager.Instancia = this;

    IEnumerator Start() {
        yield return null;
        IniciarTutorial();
    }

    void Update() {

        switch (EstAct) {

            case EstadoJuego.Jugando:

                if (TiempoDeJuego <= 0) {
                    FinalizarCarrera();
                }

                if (ConteoRegresivo) {
                    ConteoParaInicion -= T.GetDT();
                    if (ConteoParaInicion < 0) {
                        EmpezarCarrera();
                        ConteoRegresivo = false;
                    }
                }
                else {
                    //baja el tiempo del juego
                    TiempoDeJuego -= T.GetDT();
                }

                if (ConteoRegresivo) {
                    if (ConteoParaInicion > 1) {
                        ConteoInicio.text = ConteoParaInicion.ToString("0");
                    }
                    else
                    {
                        ConteoInicio.text = "GO";
                    }
                }

                ConteoInicio.gameObject.SetActive(ConteoRegresivo);

                TiempoDeJuegoText.text = TiempoDeJuego.ToString("00");

                break;

            case EstadoJuego.Finalizado:

                //muestra el puntaje

                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    SceneManager.LoadScene("Gameover");

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CloseApplication(InputAction.CallbackContext context)
    {
        if (context.performed)
            Application.Quit();
    }
}