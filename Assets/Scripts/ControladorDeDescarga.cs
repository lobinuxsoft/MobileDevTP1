using System;
using UnityEngine;

public class ControladorDeDescarga : MonoBehaviour
{
	public Action<int> onAddMoneyBonus;

    private int Contador = 0;
    Deposito2 Dep;
    public GameObject[] Componentes;
    public Player Pj;
	MeshCollider CollCamion;
    public Pallet PEnMov = null;
    public GameObject CamaraConduccion;
	public GameObject Pallet1;
	public GameObject Pallet2;
	public GameObject Pallet3;
    public Estanteria Est1;
	public Estanteria Est2;
	public Estanteria Est3;
    public Cinta Cin2;
    public float Bonus = 0;
	float TempoBonus;
    public AnimMngDesc ObjAnimado;

	void Start () 
	{
		for (int i = 0; i < Componentes.Length; i++)
		{
			Componentes[i].SetActive(false);
		}
		
		CollCamion = Pj.GetComponentInChildren<MeshCollider>();
		Pj.SetContrDesc(this);
		if(ObjAnimado != null)
			ObjAnimado.ContrDesc = this;
	}
    void Update () 
	{
		if(PEnMov)
		{
			if(TempoBonus > 0)
			{
				Bonus = (TempoBonus * (float)PEnMov.Valor) / PEnMov.Tiempo;
				TempoBonus -= Time.deltaTime;
			}
			else
			{
				Bonus = 0;
			}		
		}
    }	
	public void Activar(Deposito2 d)
	{
		Dep = d;
		CamaraConduccion.SetActive(false);
        for (int i = 0; i < Componentes.Length; i++)
		{
			Componentes[i].SetActive(true);
		}
        CollCamion.enabled = false;
		Pj.CambiarADescarga();
        GameObject go;

        for(int i = 0; i < Pj.Bolasas.Length; i++)
		{
			if(Pj.Bolasas[i] != null)
			{
				Contador++;
				
				switch(Pj.Bolasas[i].Monto)
				{
				case Pallet.Valores.Valor1:
					go = Instantiate(Pallet1);
					Est1.Recibir(go.GetComponent<Pallet>());
					break;
					
				case Pallet.Valores.Valor2:
					go = Instantiate(Pallet2);
					Est2.Recibir(go.GetComponent<Pallet>());
					break;
					
				case Pallet.Valores.Valor3:
					go = Instantiate(Pallet3);
					Est3.Recibir(go.GetComponent<Pallet>());
					break;
				}
			}
		}
		ObjAnimado.Entrar();
    }

	public void SalidaPallet(Pallet p)
	{
		PEnMov = p;
		TempoBonus = p.Tiempo;
		Pj.SacarBolasa();
	}

	public void LlegadaPallet(Pallet p)
	{
		PEnMov = null;
		Contador--;
		
		Pj.Dinero += (int)Bonus;
        onAddMoneyBonus?.Invoke(Pj.Dinero);
		if (Contador <= 0)
		{
			Finalizacion();
		}
		else
		{
			Est2.EncenderAnim();
		}
	}

    public void FinDelJuego()
	{
		Est2.enabled = false;
		Cin2.enabled = false;
	}
    void Finalizacion()
	{
		ObjAnimado.Salir();
	}

    public Pallet GetPalletEnMov() => PEnMov;

    public void FinAnimEntrada()
	{
		Est2.EncenderAnim();
	}
    public void FinAnimSalida()
	{
		for (int i = 0; i < Componentes.Length; i++)
		{
			Componentes[i].SetActive(false);
		}
        CamaraConduccion.SetActive(true);
        CollCamion.enabled = true;
        Pj.CambiarAConduccion();
        Dep.Soltar();
    }
}