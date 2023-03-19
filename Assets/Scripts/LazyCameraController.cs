using UnityEngine;

public class LazyCameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float cameraHeightMovementSpeed;
    [SerializeField] private float cameraRotationSpeed;

    private Vector3 movementInputVector;
    private float cameraHeightInput;
    private float cameraRotationInput;
    
    void Update()
    {
        UpdateInputVector();
        UpdateCameraHeight();
        UpdateCameraRotationInput();
        transform.position += transform.TransformDirection((movementInputVector * movementSpeed) * Time.deltaTime);
        transform.position += new Vector3(0.0f, cameraHeightInput * cameraHeightMovementSpeed * Time.deltaTime, 0.0f);
        transform.RotateAround(Vector3.up, cameraRotationInput * cameraRotationSpeed * Time.deltaTime);
    }

    void UpdateInputVector()
    {
        movementInputVector = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }

    void UpdateCameraHeight()
    {
        cameraHeightInput = Input.GetAxis("Mouse ScrollWheel");
    }

    void UpdateCameraRotationInput()
    {
        cameraRotationInput = 0.0f;
        if (Input.GetKey(KeyCode.Q))
        {
            cameraRotationInput = 1.0f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            cameraRotationInput = -1.0f;
        }
    }
}
