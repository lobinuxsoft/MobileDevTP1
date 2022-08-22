using UnityEngine;

public class PalletMover : ManejoPallets
{
    public Player player;

    public enum MoveType {
        WASD,
        Arrows
    }

    public MoveType miInput;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Update() 
    {
        switch (miInput)
        {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && (Input.GetKeyDown(KeyCode.A) || player.direction == Player.Direction.Left))
                {
                    PrimerPaso();
                }

                if (Tenencia() && (Input.GetKeyDown(KeyCode.S) || player.direction == Player.Direction.None))
                {
                    SegundoPaso();
                }

                if (segundoCompleto && Tenencia() && (Input.GetKeyDown(KeyCode.D) || player.direction == Player.Direction.Right))
                {
                    TercerPaso();
                }

                break;
            case MoveType.Arrows:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || player.direction == Player.Direction.Left)
                    if (!Tenencia() && Desde.Tenencia())
                    {
                        FirstStep();
                    }

                if (Input.GetKeyDown(KeyCode.DownArrow) || player.direction == Player.Direction.None)
                    if (Tenencia())
                    {
                        SecondStep();
                    }

                if (Input.GetKeyDown(KeyCode.RightArrow) || player.direction == Player.Direction.Right)
                    if (segundoCompleto && Tenencia())
                    {
                        ThirdStep();
                    }

                break;
            default:
                break;
        }
    }
    public void FirstStep() => PrimerPaso();

    public void SecondStep() => SegundoPaso();

    public void ThirdStep() => TercerPaso();

    public void PrimerPaso() 
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }

    public void SegundoPaso() 
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }

    public void TercerPaso() 
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) 
    {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) 
            {
                Pallets.RemoveAt(0);
            }
        }
    }

    public override bool Recibir(Pallet pallet) 
    {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}