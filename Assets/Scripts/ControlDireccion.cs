using UnityEngine;

public class ControlDireccion : MonoBehaviour
{
    private Player player;

    public enum Sentido
    {
        Der,
        Izq
    }

	public enum TipoInput {Mouse, Kinect, AWSD, Arrows}
	public TipoInput InputAct = TipoInput.Mouse;
    public Transform ManoDer;
	public Transform ManoIzq;
    public float MaxAng = 90;
	public float DesSencibilidad = 90;
    private float Giro;
    private Sentido DirAct;
    public bool Habilitado = true;
    private CarController carController;

    private void Awake()
    {
        player = GetComponent<Player>();
        carController = GetComponent<CarController>();
    }

    private void Update () 
	{
		switch(InputAct)
		{
		case TipoInput.Mouse:
			if(Habilitado) carController.SetGiro(MousePos.Relation(MousePos.AxisRelation.Horizontal));
            break;
			
		case TipoInput.Kinect:
            DirAct = (ManoIzq.position.y > ManoDer.position.y) ? Sentido.Der : Sentido.Izq;
			
			switch(DirAct)
			{
			case Sentido.Der:
				if(Angulo() <= MaxAng)
					Giro = Angulo() / (MaxAng + DesSencibilidad);
				else
					Giro = 1;

                if (Habilitado) carController.SetGiro(Giro);
                break;
				
			case Sentido.Izq:
                if (Angulo() <= MaxAng)
                    Giro = (Angulo() / (MaxAng + DesSencibilidad)) * (-1);
                else
                    Giro = -1;

                if (Habilitado) carController.SetGiro(Giro);
				
				break;
			}
			break;
            case TipoInput.AWSD:                // Todo: WASD GetKey() KeyConde.W
                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.A) || player.direction == Player.Direction.Left) 
                        carController.SetGiro(-1);
                    if (Input.GetKey(KeyCode.D) || player.direction == Player.Direction.Right)
                        carController.SetGiro(1);
                }
                break;
            case TipoInput.Arrows:
                if (Habilitado)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || player.direction == Player.Direction.Left) 
                        carController.SetGiro(-1);
                    if (Input.GetKey(KeyCode.RightArrow) || player.direction == Player.Direction.Right) 
                        carController.SetGiro(1);
                }
                break;
        }
	}
    public float GetGiro() => Giro;

    private float Angulo()
	{
		Vector2 diferencia = new Vector2(ManoDer.localPosition.x, ManoDer.localPosition.y) - new Vector2(ManoIzq.localPosition.x, ManoIzq.localPosition.y);
		
		return Vector2.Angle(diferencia,new Vector2(1,0));
	}
}