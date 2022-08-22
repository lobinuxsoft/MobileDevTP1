using UnityEngine;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float tiempTuto = 15;
	public float tempo;
    public bool finalizado;

    private GameManager GM;

	void Start ()
    {
        GM = GameManager.Instancia;
        Pj.ContrTuto = this;
	}

    void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}

	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
	}

    public void Finalizar()
	{
		finalizado = true;
		GM.FinTutorial(Pj.IdPlayer);
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.VaciarInv();
	}
}