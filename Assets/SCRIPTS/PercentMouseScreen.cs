using UnityEngine;

public class MousePos : MonoBehaviour 
{

	public enum AxisRelation{Horizontal, Vertical}

	/// <summary>
	/// devuelve el centro de la pantalla, el mouse siempre deberia arrancar en el medio
	/// </summary>
	/// <returns></returns>
	static public float RelCalibration() => 0.5f;

	static public float Relation(AxisRelation axisR)
	{
		float res;
		switch(axisR)
		{
		case AxisRelation.Horizontal:
			res = ((float)(Input.mousePosition.x / Screen.width)) *2 -1;
				return res;
			
			
		case AxisRelation.Vertical:
			res = ((float)(Input.mousePosition.y / Screen.height)) *2 -1;
			return res;
		}
		return -1;
	}
}