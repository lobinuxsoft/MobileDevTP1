using UnityEngine;
using UnityEngine.UI;

public class LoopTextura : MonoBehaviour 
{
	public float Intervalo = 1;
	float Tempo = 0;

	public Image imageToRender;
	public Sprite[] Imagenes;
	int Contador = 0;

	// Use this for initialization
	void Start () 
	{
		if(Imagenes.Length > 0)
			imageToRender.sprite = Imagenes[0];
	}
	
	// Update is called once per frame
	void Update () 
	{
		Tempo += Time.deltaTime;
		
		if(Tempo >= Intervalo)
		{
			Tempo = 0;
			Contador++;
			if(Contador >= Imagenes.Length)
			{
				Contador = 0;
			}
            imageToRender.sprite = Imagenes[Contador];
		}
	}
}