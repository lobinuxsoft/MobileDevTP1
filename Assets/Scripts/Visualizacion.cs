using UnityEngine;
public class Visualizacion : MonoBehaviour 
{
	public enum Lado{Izq, Der}
	public Lado LadoAct;
    
	Player Pj;
	public Camera CamCalibracion;
	public Camera CamConduccion;
	public Camera CamDescarga;
	public Vector2[]DinPos;
	public Vector2 DinEsc = Vector2.zero;
	public Vector2[] VolantePos;
	public float VolanteEsc = 0;
	public Vector2[] FondoPos;
	public Vector2 FondoEsc = Vector2.zero;
	public Texture2D TexturaVacia;	//lo que aparece si no hay ninguna bolsa
	public Texture2D TextFondo;
    public float Parpadeo = 0.8f;
	public float TempParp = 0;
	public bool PrimIma = true;
    public Texture2D[] TextInvIzq;
	public Texture2D[] TextInvDer;
    public Vector2 BonusPos = Vector2.zero;
	public Vector2 BonusEsc = Vector2.zero;
	public Color32 ColorFondoBolsa;	
	public Vector2 ColorFondoPos = Vector2.zero;
	public Vector2 ColorFondoEsc = Vector2.zero;
	public Vector2 ColorFondoFondoPos = Vector2.zero;
	public Vector2 ColorFondoFondoEsc = Vector2.zero;
	public Vector2 ReadyPos = Vector2.zero;
	public Vector2 ReadyEsc = Vector2.zero;
	public Texture2D[] ImagenesDelTuto;
	public float Intervalo = 0.8f;	//tiempo de cada cuanto cambia de imagen

	public Texture2D ImaEnPosicion;
	public Texture2D ImaReady;
	public Texture2D TextNum1; 
	public Texture2D TextNum2;
	public GameObject Techo;

	void Start () => Pj = GetComponent<Player>();

    void OnGUI()
	{
        switch (Pj.EstAct)
        {
            case Player.Estados.EnConduccion:
                SetInv3();
                SetDinero();
                SetVolante();
                break;
            case Player.Estados.EnDescarga:
                SetInv3();
                SetBonus();
                SetDinero();
                break;


            case Player.Estados.EnCalibracion:
                //SetCalibr();
                break;


            case Player.Estados.EnTutorial:
                SetInv3();
                SetTuto();
                SetVolante();
                break;
        }
	}

	public void CambiarACalibracion()
	{
		CamCalibracion.enabled = true;
		CamConduccion.enabled = false;
		CamDescarga.enabled = false;
	}

    public void CambiarATutorial()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}

    public void CambiarAConduccion()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = true;
		CamDescarga.enabled = false;
	}

    public void CambiarADescarga()
	{
		CamCalibracion.enabled = false;
		CamConduccion.enabled = false;
		CamDescarga.enabled = true;
	}

	public void SetLado(Lado lado)
	{
		LadoAct = lado;
		
		Rect r = new Rect();
		r.width = CamConduccion.rect.width;
		r.height = CamConduccion.rect.height;
		r.y = CamConduccion.rect.y;

        switch (lado)
        {
            case Lado.Der:
                r.x = 0.5f;
                break;


            case Lado.Izq:
                r.x = 0;
                break;
        }

        CamCalibracion.rect = r;
		CamConduccion.rect = r;
		CamDescarga.rect = r;
		
		if(LadoAct == Visualizacion.Lado.Izq)
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum1;
		}
		else
		{
			Techo.GetComponent<Renderer>().material.mainTexture = TextNum2;
		}
	}

    void SetBonus()
	{
		// Todo: Se actualiza el Bonus ????????
	}
	
	void SetDinero()		// Todo: Se Actualiza en dinero
	{
		
	}
	
	void SetTuto()
	{
		// Todo: Se setea el Bonus ?¡??¡?¡?
	}

	void SetVolante()
	{
		// Todo: Se setea que pingo del volante?¡?¡?¡?
	}
	
	void SetInv2()
	{
        // Todo: Se setea el Inventario 2
	}
	
	void SetInv3()
	{
	}
	
	public string PrepararNumeros(int dinero)
	{
		string strDinero = dinero.ToString();
		string res = "";
		
		if(dinero < 1)	//sin ditero
		{
			res = "";
		}
		else if(strDinero.Length == 6)	//cientos de miles
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 2)
				{
					res += ".";
				}
			}
		}
		else if(strDinero.Length == 7)	//millones
		{
			for(int i = 0; i < strDinero.Length; i++)
			{
				res += strDinero[i];
				
				if(i == 0 || i == 3)
				{
					res += ".";
				}
			}
		}
		
		return res;
	}
}