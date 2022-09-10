using UnityEngine;

public class ManejoPallets : MonoBehaviour 
{
	protected System.Collections.Generic.List<Pallet> Pallets = new System.Collections.Generic.List<Pallet>();
	public ControladorDeDescarga Controlador;
	protected int Contador = 0;
	
	public virtual bool Recibir(Pallet pallet)
	{
		Pallets.Add(pallet);
		pallet.Pasaje();
		return true;
	}
	
	public bool Tenencia()
	{
		
		if(Pallets.Count != 0)
			return true;
		else
			return false;
	}

    /// <summary>
    /// es el encargado de decidir si le da o no la bolsa
    /// </summary>
    /// <param name="receptor"></param>
    public virtual void Dar(ManejoPallets receptor) { }
}
