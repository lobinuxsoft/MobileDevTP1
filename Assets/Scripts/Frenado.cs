using UnityEngine;
public class Frenado : MonoBehaviour
{
    public float velEntrada;
    public string tagDeposito = "Deposito";
    public bool frenando;
    private int cantMensajes = 10;
    private CarController carController;
    private int contador;
    private Vector3 destino;
    private ControlDireccion KInput;
    private float tempo;
    private float tiempoFrenado = 0.5f;
    private Player player;
    private ControlDireccion controlDireccion;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        controlDireccion = GetComponent<ControlDireccion>();
        carController = GetComponent<CarController>();
    }
    private void Start()
    {
        Frenar();
    }
    private void FixedUpdate()
    {
        if (frenando)
        {
            tempo += Time.deltaTime;
            if (tempo >= tiempoFrenado / cantMensajes * contador) contador++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDeposito))
        {
            Deposito2 dep = other.GetComponent<Deposito2>();
            if (!dep.Vacio) return;

            if (player.ConBolasas())
            {
                dep.Entrar(player);
                destino = other.transform.position;
                transform.forward = destino - transform.position;
                Frenar();
            }
        }
    }
    public void Frenar()
    {
        controlDireccion.enabled = false;
        carController.SetAcel(0);
        rb.velocity = Vector3.zero;

        frenando = true;

        tempo = 0;
        contador = 0;
    }
    public void RestaurarVel()
    {
        controlDireccion.enabled = true;
        carController.SetAcel(1);
        frenando = false;
        tempo = 0;
        contador = 0;
    }
}