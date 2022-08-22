using System;
using JetBrains.Annotations;
using UnityEngine;
public class Player : MonoBehaviour
{
    public enum Direction
    {
        Left,
        None,
        Right
    }

    public Direction direction = Direction.None;

    public Action<int> AddMoney;
	public int Dinero = 0;
	public int IdPlayer = 0;
    public Bolsa[] Bolasas;
    public int CantBolsAct = 0;
	public string TagBolsas = "";
    public enum Estados{EnDescarga, EnConduccion, EnCalibracion, EnTutorial}
	public Estados EstAct = Estados.EnConduccion;
    public bool EnConduccion = true;
	public bool EnDescarga = false;
    public ControladorDeDescarga ContrDesc;
	public ContrCalibracion ContrCalib;
	public ContrTutorial ContrTuto;
    private Visualizacion MiVisualizacion;

    private void Awake()
    {
		MiVisualizacion = GetComponent<Visualizacion>();
        AddMoney += UpdateMoney;
    }
    void Start ()
    {
        for (int i = 0; i < Bolasas.Length; i++)
            Bolasas[i] = null;
    }

    public bool AgregarBolsa(Bolsa b)
	{
		if(CantBolsAct + 1 <= Bolasas.Length)
		{
			Bolasas[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();
            AddMoney?.Invoke(Dinero);
			return true;
		}
		else
		{
			return false;
		}
	}

    public void VaciarInv()
	{
		for(int i = 0; i< Bolasas.Length;i++)
			Bolasas[i] = null;
		
		CantBolsAct = 0;
        AddMoney?.Invoke(Dinero);
    }

    public bool ConBolasas()
	{
		for(int i = 0; i< Bolasas.Length;i++)
		{
			if(Bolasas[i] != null)
			{
				return true;
			}
		}
		return false;
	}

    public void SetContrDesc(ControladorDeDescarga contr)
	{
		ContrDesc = contr;
	}

    public ControladorDeDescarga GetContr()
	{
		return ContrDesc;
	}

    public void CambiarACalibracion()
	{
		MiVisualizacion.CambiarACalibracion();
		EstAct = Estados.EnCalibracion;
	}

    public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		EstAct = Estados.EnTutorial;
		ContrTuto.Iniciar();
	}

    public void CambiarAConduccion()
	{
		MiVisualizacion.CambiarAConduccion();
		EstAct = Estados.EnConduccion;
	}

    public void CambiarADescarga()
	{
		MiVisualizacion.CambiarADescarga();
		EstAct = Estados.EnDescarga;
	}

    void UpdateMoney(int money)
    {
        if (IdPlayer == 0)
            GameMaster.Get().moneyP1 = money;
        else if (IdPlayer == 1)
            GameMaster.Get().moneyP2 = money;
    }

	public void SacarBolasa()
	{
		for(int i = 0; i < Bolasas.Length; i++)
		{
			if(Bolasas[i] != null)
			{
				Bolasas[i] = null;
				return;
			}				
		}
	}
}