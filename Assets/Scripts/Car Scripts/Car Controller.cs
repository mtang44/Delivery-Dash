using UnityEngine;

public class CarController : MonoBehaviour
{
    private CarBasicMovement controls;

    [SerializeField] Rotatable streeringWheel;
    [SerializeField] float acceleration = 300f;
    [SerializeField] float breakingForce = 300f;
    [SerializeField] float maxTurnAngle = 30f;
    [SerializeField] float maxWheelRotation = 90f;

    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightWheelMesh;
    [SerializeField] Transform frontLeftWheelMesh;
    [SerializeField] Transform backRightWheelMesh;
    [SerializeField] Transform backLeftWheelMesh;

    float currentAcceleration = 0; 
    float currentBreakForce = 0; 
    float currentTurnAngle = 0; 
    void Awake()
    {
        controls = new CarBasicMovement();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        
        // Debug.Log(controls.Player.Move.ReadValue<Vector2>());
        // handling acceleration
        currentAcceleration = acceleration * controls.Player.Move.ReadValue<Vector2>().y; 
       

        Debug.Log(controls.Player.Move.ReadValue<Vector2>().y + " | CA:  " + currentAcceleration);
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;


        // handling wheel braking
        frontRight.brakeTorque = currentBreakForce; 
        frontLeft.brakeTorque = currentBreakForce; 
        backRight.brakeTorque = currentBreakForce; 
        backLeft.brakeTorque = currentBreakForce; 


        //handling turning
        // currentTurnAngle = maxTurnAngle * controls.Player.Move.ReadValue<Vector2>().x; // used to check a d handling

       float steeringInput = Mathf.Clamp(
        streeringWheel.CurrentWheelRotation / 90f,
        -1f,
        1f
         );

        currentTurnAngle = maxTurnAngle * steeringInput;
        // Debug.Log("Wheel: " + streeringWheel.CurrentWheelRotation +" | Turn: " + currentTurnAngle);
       
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        SetWheel(frontRight, frontRightWheelMesh);
        SetWheel(frontLeft, frontLeftWheelMesh);
        SetWheel(backRight, backRightWheelMesh);
        SetWheel(backLeft, backLeftWheelMesh);
    }

    void SetWheel(WheelCollider wheelCol, Transform wheelMesh)
    {
        Vector3 pos;
        Quaternion rotation;
        wheelCol.GetWorldPose(out pos, out rotation);
        wheelMesh.rotation = rotation;
    }
}
