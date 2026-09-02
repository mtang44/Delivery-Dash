using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Rotatable : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private InputAction pressed, rotationAxis;
    
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float maxRightRotationAngle, maxLeftRotationAngle;
    private Vector3 rotation;
    private bool rotateAllowed;
    private float currentWheelRotation; 
    

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
    
        currentWheelRotation = transform.eulerAngles.x;
        Debug.Log("Current Rotation: " + currentWheelRotation);
        // CheckRotationBounds();
    }

    private void CheckRotationBounds()
    {
        if(currentWheelRotation > maxLeftRotationAngle)
        {
            transform.Rotate(Vector3.up,maxLeftRotationAngle, Space.Self );
        }
        else if(currentWheelRotation < maxRightRotationAngle)
        {
            transform.Rotate(Vector3.up,maxRightRotationAngle, Space.Self );
        }
        else
        {
            return ;
        }
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;
        while(rotateAllowed ) //&& currentWheelRotation < maxLeftRotationAngle && currentWheelRotation > maxRightRotationAngle
        {
            // apply rotation 
            rotation *= rotationSpeed;
            transform.Rotate(Vector3.up,rotation.x , Space.Self);
            yield return null;
        }
    
    }
}
