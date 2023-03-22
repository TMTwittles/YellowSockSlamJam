using UnityEngine;

public class LazyCameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float cameraHeightMovementSpeed;
    [SerializeField] private float cameraRotationSpeed;
    [SerializeField] private float minZoom = 10f;
    [SerializeField] private float maxZoom = 50f;
    public float FurthestPlanetMagnitude { get; set; }

    private Vector3 movementInputVector;
    private float cameraHeightInput;
    private float cameraRotationInput;
    
    void Update()
    {
        UpdateInputVector();
        UpdateCameraHeight();
        //UpdateCameraRotationInput();
        transform.position += transform.TransformDirection((movementInputVector * movementSpeed) * Time.deltaTime);        
        transform.position += new Vector3(0.0f, cameraHeightInput * cameraHeightMovementSpeed * Time.deltaTime, 0.0f);

        Vector2 positionXZ = new Vector3(transform.position.x, transform.position.z);
        if (positionXZ.magnitude > FurthestPlanetMagnitude) positionXZ = Vector3.Normalize(positionXZ) * FurthestPlanetMagnitude;
        float positionY = Mathf.Clamp(transform.position.y, minZoom, maxZoom);

        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.y);
        //transform.RotateAround(Vector3.up, cameraRotationInput * cameraRotationSpeed * Time.deltaTime);
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
