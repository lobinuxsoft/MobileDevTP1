using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour 
{
    [SerializeField] InputActionReference turnAction;
    public List<WheelCollider> throttleWheels = new List<WheelCollider>();
    public List<WheelCollider> steeringWheels = new List<WheelCollider>();
    public float throttleCoefficient = 20000f;
    public float maxTurn = 20f;
    float giro = 0f;
    float acel = 1f;

    private void Update()
    {
        if (turnAction.action.enabled)
            giro = turnAction.action.ReadValue<Vector2>().x;
        else
            giro = 0f;
    }

    // Update is called once per frame
    void FixedUpdate () {
        foreach (var wheel in throttleWheels) {
            wheel.motorTorque = throttleCoefficient * T.GetFDT() * acel;
        }
        foreach (var wheel in steeringWheels) {
            wheel.steerAngle = maxTurn * giro;
        }
    }

    public void SetAcel(float val) => acel = val;

    public void TurnEnable() => turnAction.action.Enable();
    public void TurnDisable() => turnAction.action.Disable();

    public void ResetTurn() => this.giro = 0f;

    public float GetTurn() => this.giro;
}