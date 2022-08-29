using UnityEngine;

public class AnimMngDesc : MonoBehaviour 
{
	public string AnimEntrada = "Entrada";
	public string AnimSalida = "Salida";
	public ControladorDeDescarga ContrDesc;
	public GameObject PuertaAnimada;
	
	enum AnimEnCurso{Salida,Entrada,Nada}
	AnimEnCurso AnimAct = AnimMngDesc.AnimEnCurso.Nada;

	Animation anim;
	Animation doorAnima;

	private void Awake() 
	{
        anim = GetComponent<Animation>();
		doorAnima = PuertaAnimada.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Z))
			Entrar();
		if(Input.GetKeyDown(KeyCode.X))
			Salir();
		
		switch(AnimAct)
		{
		case AnimEnCurso.Entrada:
			
			if(!anim.IsPlaying(AnimEntrada))
			{
				AnimAct = AnimMngDesc.AnimEnCurso.Nada;
				ContrDesc.FinAnimEntrada();
			}
			
			break;
			
		case AnimEnCurso.Salida:
			
			if(!anim.IsPlaying(AnimSalida))
			{
				AnimAct = AnimMngDesc.AnimEnCurso.Nada;
				ContrDesc.FinAnimSalida();
			}
			
			break;
			
		case AnimEnCurso.Nada:
			break;
		}
	}
	
	public void Entrar()
	{
		AnimAct = AnimMngDesc.AnimEnCurso.Entrada;
        anim.Play(AnimEntrada);
		
		if(PuertaAnimada != null)
		{
            doorAnima["AnimPuerta"].time = 0;
            doorAnima["AnimPuerta"].speed = 1;
            doorAnima.Play("AnimPuerta");
		}
	}
	
	public void Salir()
	{
		AnimAct = AnimMngDesc.AnimEnCurso.Salida;
        anim.Play(AnimSalida);
		
		if(PuertaAnimada != null)
		{
            doorAnima["AnimPuerta"].time = doorAnima["AnimPuerta"].length;
            doorAnima["AnimPuerta"].speed = -1;
            doorAnima.Play("AnimPuerta");
		}
	}
}