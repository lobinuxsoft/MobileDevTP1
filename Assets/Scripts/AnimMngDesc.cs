using UnityEngine;

public class AnimMngDesc : MonoBehaviour 
{
	public string AnimEntrada = "Entrada";
	public string AnimSalida = "Salida";
	public ControladorDeDescarga ContrDesc;
	
	enum AnimEnCurso{Salida,Entrada,Nada}
	AnimEnCurso AnimAct = AnimMngDesc.AnimEnCurso.Nada;
	
	public GameObject PuertaAnimada;

    public Material[] materials;

    void Start()
    {
        if (materials.Length == 9)
        {
            Transform door = PuertaAnimada.transform.GetChild(0);
            
            door.GetComponent<MeshRenderer>().materials[0] = materials[0];
            door.GetComponent<MeshRenderer>().materials[1] = materials[1];
            door.GetComponent<MeshRenderer>().materials[2] = materials[2];
            door.GetComponent<MeshRenderer>().materials[3] = materials[3];

            door.GetChild(0).GetComponent<MeshRenderer>().materials[0] = materials[4];
            door.GetChild(1).GetComponent<MeshRenderer>().materials[0] = materials[5];
            door.GetChild(2).GetComponent<MeshRenderer>().materials[0] = materials[6];
            door.GetChild(3).GetComponent<MeshRenderer>().materials[0] = materials[7];

            PuertaAnimada.transform.GetChild(1).GetComponent<MeshRenderer>().materials[0] = materials[8];
        }
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
			
			if(!GetComponent<Animation>().IsPlaying(AnimEntrada))
			{
				AnimAct = AnimMngDesc.AnimEnCurso.Nada;
				ContrDesc.FinAnimEntrada();
			}
			
			break;
			
		case AnimEnCurso.Salida:
			
			if(!GetComponent<Animation>().IsPlaying(AnimSalida))
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
		GetComponent<Animation>().Play(AnimEntrada);
		
		if(PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}
	
	public void Salir()
	{
		AnimAct = AnimMngDesc.AnimEnCurso.Salida;	
		GetComponent<Animation>().Play(AnimSalida);
		
		if(PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}
}
