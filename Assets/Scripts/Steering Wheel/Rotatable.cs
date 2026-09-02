using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Rotatable : MonoBehaviour
{

   
    [SerializeField] private InputAction pressed, rotationAxis;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float maxRightRotationAngle, maxLeftRotationAngle;
    private Vector3 rotation;
    private bool rotateAllowed;
    private float currentWheelRotation; 
    
     public float CurrentWheelRotation => currentWheelRotation; // allows for currentWheelRotation to be accessed by other scripts without giving direct access to variable itself

    
    private void Awake()
    {

        pressed.Enable();
        rotationAxis.Enable();
        rotateAllowed = true; 
        pressed.performed += _=> {StartCoroutine(Rotate());};
        pressed.canceled += _=>{rotateAllowed = false;};  
        rotationAxis.performed += context => {rotation = context.ReadValue<Vector2>();};

    }
    void Update()
    {
      
        currentWheelRotation = transform.eulerAngles.z;
        if (currentWheelRotation > 180f)
        {
            currentWheelRotation -= 360f;
        }
        // Debug.Log("Current Rotation: " + currentWheelRotation);
      
        // CheckRotationBounds();
    }

   

    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while(rotateAllowed ) //&& currentWheelRotation < maxLeftRotationAngle && currentWheelRotation > maxRightRotationAngle
        {
            // apply rotation 
            float rotationAmount = rotation.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up,rotationAmount , Space.Self);
            yield return null;
        }
         // Snap wheel back to 90 degrees
            transform.localEulerAngles = new Vector3(
            90f,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
        );

    
    }
}
