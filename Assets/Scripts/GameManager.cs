using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instancia;
    public float TiempoDeJuego = 60;
    public List<GameObject> obtaclesList = new List<GameObject>();
    public enum EstadoJuego{Calibrando, Jugando, Finalizado}
	public EstadoJuego EstAct = EstadoJuego.Calibrando;
    public PlayerInfo PlayerInfo1;
	public PlayerInfo PlayerInfo2;
    public Player Player1;
	public Player Player2;
	public Transform Esqueleto1;
	public Transform Esqueleto2;
	public Vector3[] PosEsqsCarrera;
    bool ConteoRedresivo = true;
	public Rect ConteoPosEsc;
	public float ConteoParaInicion = 3;
    public Rect TiempoGUI = new Rect();
	Rect R = new Rect();
    public float TiempEspMuestraPts = 3;
    public Vector3[]PosCamionesCarrera = new Vector3[2];
    public Vector3 PosCamion1Tuto = Vector3.zero;
	public Vector3 PosCamion2Tuto = Vector3.zero;
    public GameObject[] ObjsCalibracion1;
	public GameObject[] ObjsCalibracion2;
	//escena de tutorial
	public GameObject[] ObjsTuto1;
	public GameObject[] ObjsTuto2;
	//la pista de carreras
	public GameObject[] ObjsCarrera;
	IList<int> users;
    public ControladorDeDescarga controladorP2;
    public PalletMover palletP2;
    public GameObject[] buttonStart;

	void Awake()
	{
		GameManager.Instancia = this;
	}

    void Start()
	{
		IniciarCalibracion();
        if (GameMaster.Get().IsSinglePlayer())
        {
            StartCoroutine(UseAutimaticArrow());
        }

        int dificult = GameMaster.Get().dificult;
        for (int i = 0; i <= dificult; i++)
        {
            obtaclesList[i].SetActive(true);
        }
	}

    private IEnumerator UseAutimaticArrow()
    {
        CreatePlayer2();

        yield return new WaitForSeconds(0.1f);
        palletP2.FirstStep();

        yield return new WaitForSeconds(0.1f);
        palletP2.SecondStep();

        yield return new WaitForSeconds(0.1f);
        palletP2.ThirdStep();
	}

    public void CreatePlayer1()
    {
        if (!PlayerInfo1.PJ)
        {
            buttonStart[0].SetActive(false);
			PlayerInfo1 = new PlayerInfo(0, Player1);
            PlayerInfo1.LadoAct = Visualizacion.Lado.Izq;
            SetPosicion(PlayerInfo1);
        }
    }

    public void CreatePlayer2()
    {
        if (!PlayerInfo2.PJ)
        {
            buttonStart[1].SetActive(false);
            PlayerInfo2 = new PlayerInfo(1, Player2);
            PlayerInfo2.LadoAct = Visualizacion.Lado.Der;
            SetPosicion(PlayerInfo2);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();

        switch (EstAct)
        {
            case EstadoJuego.Calibrando:
                if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.Keypad0))
                {
                    if (PlayerInfo1 != null && PlayerInfo2 != null)
                    {
                        EndCalib();
                    }
					else if (PlayerInfo1 != null && GameMaster.Get().IsSinglePlayer())
                    {
                        EndCalib();
					}

                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    CreatePlayer1();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    CreatePlayer2();
                }

                if (PlayerInfo1.PJ && PlayerInfo2.PJ)
                {
                    if (PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
                    {
                        EmpezarCarrera();
                    }
                }

                break;
            case EstadoJuego.Jugando:
                if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Keypad0))
                    TiempoDeJuego = 0;

                if (TiempoDeJuego <= 0) FinalizarCarrera();

                if (ConteoRedresivo)
                {
                    ConteoParaInicion -= Time.deltaTime;
                    if (ConteoParaInicion < 0)
                    {
                        EmpezarCarrera();
                        ConteoRedresivo = false;
                    }
                }
                else
                {
                    TiempoDeJuego -= Time.deltaTime;
                }

                break;
            case EstadoJuego.Finalizado:
                TiempEspMuestraPts -= Time.deltaTime;
                if (TiempEspMuestraPts <= 0)
                    SceneManager.LoadScene("GameOver");
                break;
        }
    }

    void EndCalib()
    {
        FinCalibracion(0);
        FinCalibracion(1);

        FinTutorial(0);
        FinTutorial(1);
	}

	public void IniciarCalibracion()
	{
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActive(true);
			ObjsCalibracion2[i].SetActive(true);
		}
		
		for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActive(false);
			ObjsTuto1[i].SetActive(false);
		}

        for(int i = 0; i < ObjsCarrera.Length; i++)
		{
			ObjsCarrera[i].SetActive(false);
		}

        Player1.CambiarACalibracion();

        if (!GameMaster.Get().IsSinglePlayer())
            Player2.CambiarACalibracion();
    }
	void CambiarATutorial()
	{
		PlayerInfo1.FinCalibrado = true;
			
		for(int i = 0; i < ObjsTuto1.Length; i++)
		{
			ObjsTuto1[i].SetActive(true);
		}
		
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActive(false);
		}

		Player1.GetComponent<Frenado>().Frenar();
		Player1.CambiarATutorial();
		Player1.gameObject.transform.position = PosCamion1Tuto;//posiciona el camion
		Player1.transform.forward = Vector3.forward;
			
			
		PlayerInfo2.FinCalibrado = true;
			
		for(int i = 0; i < ObjsCalibracion2.Length; i++)
		{
			ObjsCalibracion2[i].SetActive(false);
		}
		
		for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActive(true);
		}

		Player2.GetComponent<Frenado>().Frenar();
		Player2.gameObject.transform.position = PosCamion2Tuto;
		Player2.CambiarATutorial();
		Player2.transform.forward = Vector3 .forward;
	}

    void EmpezarCarrera()
	{
		Player1.GetComponent<Frenado>().RestaurarVel();
		Player1.GetComponent<ControlDireccion>().Habilitado = true;
			
		Player2.GetComponent<Frenado>().RestaurarVel();
		Player2.GetComponent<ControlDireccion>().Habilitado = true;
	}

    void FinalizarCarrera()
	{		
		EstAct = GameManager.EstadoJuego.Finalizado;
		
		TiempoDeJuego = 0;
		
		if(Player1.Dinero > Player2.Dinero)
		{
			//lado que gano
			if(PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
			else
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
			
			//puntajes
			DatosPartida.PtsGanador = Player1.Dinero;
			DatosPartida.PtsPerdedor = Player2.Dinero;
		}
		else
		{
			//lado que gano
			if(PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
			else
				DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;
			
			//puntajes
			DatosPartida.PtsGanador = Player2.Dinero;
			DatosPartida.PtsPerdedor = Player1.Dinero;
		}
		
		Player1.GetComponent<Frenado>().Frenar();
		Player2.GetComponent<Frenado>().Frenar();
		
		Player1.ContrDesc.FinDelJuego();

        if (!GameMaster.Get().IsSinglePlayer())
            Player2.ContrDesc.FinDelJuego();
    }

	void SetPosicion(PlayerInfo pjInf)
	{	
		pjInf.PJ.GetComponent<Visualizacion>().SetLado(pjInf.LadoAct);
		pjInf.PJ.ContrCalib.IniciarTesteo();
		
		if(pjInf.PJ == Player1)
		{
			if(pjInf.LadoAct == Visualizacion.Lado.Izq)
				Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
			else
				Player2.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
		}
		else
		{
			if(pjInf.LadoAct == Visualizacion.Lado.Izq)
				Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Der);
			else
				Player1.GetComponent<Visualizacion>().SetLado(Visualizacion.Lado.Izq);
		}
    }

    void CambiarACarrera()
	{
		Esqueleto1.transform.position = PosEsqsCarrera[0];
		Esqueleto2.transform.position = PosEsqsCarrera[1];
		
		for(int i = 0; i < ObjsCarrera.Length; i++)
		{
			ObjsCarrera[i].SetActive(true);
		}

		PlayerInfo1.FinCalibrado = true;
			
		for(int i = 0; i < ObjsTuto1.Length; i++)
		{
			ObjsTuto1[i].SetActive(true);
		}
		
		for(int i = 0; i < ObjsCalibracion1.Length; i++)
		{
			ObjsCalibracion1[i].SetActive(false);
		}

        PlayerInfo2.FinCalibrado = true;

        for(int i = 0; i < ObjsCalibracion2.Length; i++)
		{
			ObjsCalibracion2[i].SetActive(false);
		}

        for(int i = 0; i < ObjsTuto2.Length; i++)
		{
			ObjsTuto2[i].SetActive(true);
		}

        if(PlayerInfo1.LadoAct == Visualizacion.Lado.Izq)
		{
			Player1.gameObject.transform.position = PosCamionesCarrera[0];
			Player2.gameObject.transform.position = PosCamionesCarrera[1];
		}
		else
		{
			Player1.gameObject.transform.position = PosCamionesCarrera[1];
			Player2.gameObject.transform.position = PosCamionesCarrera[0];
		}

        Player1.transform.forward = Vector3 .forward;
		Player1.GetComponent<Frenado>().Frenar();
		Player1.CambiarAConduccion();

        if (!GameMaster.Get().IsSinglePlayer())
        {
            Player2.transform.forward = Vector3.forward;
            Player2.GetComponent<Frenado>().Frenar();
            Player2.CambiarAConduccion();
        }

        Player1.GetComponent<Frenado>().RestaurarVel();
		Player2.GetComponent<Frenado>().RestaurarVel();
		Player1.GetComponent<ControlDireccion>().Habilitado = false;
		Player2.GetComponent<ControlDireccion>().Habilitado = false;
		Player1.transform.forward = Vector3.forward;
		Player2.transform.forward = Vector3.forward;
        EstAct = GameManager.EstadoJuego.Jugando;
	}

    public void FinTutorial(int playerID)
	{
        if (playerID == 0)
        {
            PlayerInfo1.FinTuto2 = true;

        }
        else if (playerID == 1)
        {
            PlayerInfo2.FinTuto2 = true;
        }

        if(PlayerInfo1.FinTuto2 && PlayerInfo2.FinTuto2)
		{
			CambiarACarrera();
		}
	}

    public void FinCalibracion(int playerID)
	{
		if(playerID == 0)
		{
			PlayerInfo1.FinTuto1 = true;
			
		}
		else if(playerID == 1)
		{
			PlayerInfo2.FinTuto1 = true;
		}
		
		if(PlayerInfo1.PJ != null && PlayerInfo2.PJ != null)
            if (PlayerInfo1.FinTuto1 && PlayerInfo2.FinTuto1)
                CambiarACarrera();
    }

    [System.Serializable]
	public class PlayerInfo
	{
        public PlayerInfo(int tipoDeInput, Player pj)
        {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinCalibrado = false;
		public bool FinTuto1 = false;
		public bool FinTuto2 = false;
        public Visualizacion.Lado LadoAct;
        public int TipoDeInput = -1;
        public Player PJ;
	}
}