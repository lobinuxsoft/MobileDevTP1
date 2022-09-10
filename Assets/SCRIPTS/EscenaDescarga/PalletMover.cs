using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{
    [SerializeField] InputActionReference firstStepAction;
    [SerializeField] InputActionReference secondStepAction;
    [SerializeField] InputActionReference thirdStepAction;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Awake()
    {
        firstStepAction.action.Enable();
        secondStepAction.action.Enable();
        thirdStepAction.action.Enable();

        firstStepAction.action.performed += FirstStepPallet;
        secondStepAction.action.performed += SecondStepPallet;
        thirdStepAction.action.performed += ThirdStepPallet;
    }

    private void OnDestroy()
    {
        firstStepAction.action.Disable();
        secondStepAction.action.Disable();
        thirdStepAction.action.Disable();

        firstStepAction.action.performed -= FirstStepPallet;
        secondStepAction.action.performed -= SecondStepPallet;
        thirdStepAction.action.performed -= ThirdStepPallet;
    }

    private void OnEnable()
    {
        firstStepAction.action.performed += FirstStepPallet;
        secondStepAction.action.performed += SecondStepPallet;
        thirdStepAction.action.performed += ThirdStepPallet;
    }

    private void OnDisable()
    {
        firstStepAction.action.performed -= FirstStepPallet;
        secondStepAction.action.performed -= SecondStepPallet;
        thirdStepAction.action.performed -= ThirdStepPallet;
    }

    void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }

    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }

    void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }

    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }

    public void FirstStepPallet(InputAction.CallbackContext context)
    {
        if (!Tenencia() && Desde.Tenencia() && context.performed)
            PrimerPaso();
    }

    public void SecondStepPallet(InputAction.CallbackContext context)
    {
        if (Tenencia() && context.performed)
            SegundoPaso();
    }

    public void ThirdStepPallet(InputAction.CallbackContext context)
    {
        if (segundoCompleto && Tenencia() && context.performed)
            TercerPaso();
    }
}