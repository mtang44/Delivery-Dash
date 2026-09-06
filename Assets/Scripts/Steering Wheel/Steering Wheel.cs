using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private InputAction pressed, rotationAxis;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float maxRightRotationAngle, maxLeftRotationAngle;

    private Vector3 rotation;
    private bool rotateAllowed;
    private float currentWheelRotation;

    public float CurrentWheelRotation => currentWheelRotation;

    private void Awake()
    {
        pressed.Enable();
        rotationAxis.Enable();

        rotateAllowed = true;

        pressed.performed += OnPressed;
        pressed.canceled += OnPressedCanceled;
        rotationAxis.performed += OnRotation;
    }

    private void OnDestroy()
    {
        pressed.performed -= OnPressed;
        pressed.canceled -= OnPressedCanceled;
        rotationAxis.performed -= OnRotation;
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
        StartCoroutine(Rotate());
    }

    private void OnPressedCanceled(InputAction.CallbackContext context)
    {
        rotateAllowed = false;
    }

    private void OnRotation(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>();
    }

    void Update()
    {
        currentWheelRotation = transform.eulerAngles.z;

        if (currentWheelRotation > 180f)
        {
            currentWheelRotation -= 360f;
        }
    }

    private IEnumerator Rotate()
    {
        rotateAllowed = true;

        while (rotateAllowed)
        {
            float rotationAmount = rotation.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationAmount, Space.Self);

            yield return null;
        }

        transform.localEulerAngles = new Vector3(
            90f,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
        );
    }
}