using UnityEngine;

public class FadeInicioFinal : MonoBehaviour 
{
	public float Duracion = 2;
	public float Vel = 2;
	float TiempInicial;
	MngPts Mng;
    Color aux;
    bool MngAvisado = false;

    void Start ()
	{
		Mng = (MngPts)GameObject.FindObjectOfType(typeof (MngPts));
		TiempInicial = Mng.TiempEspReiniciar;
		
		aux = GetComponent<Renderer>().material.color;
		aux.a = 0;
		GetComponent<Renderer>().material.color = aux;
	}

	void Update () 
	{
		if(Mng.TiempEspReiniciar > TiempInicial - Duracion)
		{
			aux = GetComponent<Renderer>().material.color;
			aux.a += Time.deltaTime / Duracion;
			GetComponent<Renderer>().material.color = aux;			
		}
		else if(Mng.TiempEspReiniciar < Duracion)
		{
			aux = GetComponent<Renderer>().material.color;
			aux.a -= Time.deltaTime / Duracion;
			GetComponent<Renderer>().material.color = aux;
			
			if(!MngAvisado)
			{
				MngAvisado = true;
			}
		}
	}
}