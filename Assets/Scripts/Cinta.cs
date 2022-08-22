using UnityEngine;

public class Cinta : ManejoPallets
{
    private const float SpecificHeight = 3.61f;

	public bool encendida;
	public float velocidad = 1;

    private Transform ObjAct;
	
	void Update () 
	{
        for(int i = 0; i < Pallets.Count; i++)
		{
			if(Pallets[i].GetComponent<Renderer>().enabled)
			{
				if(!Pallets[i].GetComponent<Pallet>().EnSmoot)
				{
					Pallets[i].GetComponent<Pallet>().enabled = false;
					Pallets[i].TempoEnCinta += Time.deltaTime;

                    Pallets[i].transform.position += transform.right * (velocidad * Time.deltaTime);
					Vector3 vAux = Pallets[i].transform.localPosition;
                    vAux.y = SpecificHeight;
					Pallets[i].transform.localPosition = vAux;

                    if (Pallets[i].TempoEnCinta >= Pallets[i].TiempEnCinta)
                    {
                        Pallets[i].TempoEnCinta = 0;
                        ObjAct.gameObject.SetActive(false);
                    }
                }
			}
		}
	}
    void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
	}
	public override bool Recibir(Pallet p)
	{
        Controlador.LlegadaPallet(p);
        p.Portador = gameObject;
        ObjAct = p.transform;
        base.Recibir(p);
        Apagar();
        return true;
    }
    public void Encender() => encendida = true;
    public void Apagar()
	{
		encendida = false;
	}
}