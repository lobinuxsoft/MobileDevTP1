using UnityEngine;

public class Bolsa : MonoBehaviour
{
	public Pallet.Valores Monto;
	public string TagPlayer = "";
	public Texture2D ImagenInventario;
	public GameObject Particulas;
	public float TiempParts = 2.5f;

    private Player Pj;
    private bool Desapareciendo;
    private Renderer rend;
    private Collider col;

	private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }
    void Start () 
	{
		Monto = Pallet.Valores.Valor2;
        if(Particulas != null)
			Particulas.SetActive(false);
    }
	void Update ()
	{
        if(Desapareciendo)
		{
			TiempParts -= Time.deltaTime;
			if(TiempParts <= 0)
			{
				rend.enabled = true;
                col.enabled = true;
                Particulas.GetComponent<ParticleSystem>().Stop();
				gameObject.SetActive(false);
			}
		}
    }
    void OnTriggerEnter(Collider coll)
	{
		if(coll.CompareTag(TagPlayer))
		{
			Pj = coll.GetComponent<Player>();
            if (Pj.AgregarBolsa(this))
                Desaparecer();
        }
	}
    public void Desaparecer()
	{
		Particulas.GetComponent<ParticleSystem>().Play();
		Desapareciendo = true;
        rend.enabled = false;
		col.enabled = false;
		
		if(Particulas != null)
		{
			Particulas.GetComponent<ParticleSystem>().Play();
		}
    }
}