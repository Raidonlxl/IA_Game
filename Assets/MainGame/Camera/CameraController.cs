using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sens;
    [SerializeField] private Vector3 offset = new Vector3(1, 2, -2.5f);
    [SerializeField] private Vector3 positionCamera;
    private float xAxis;
    private float yAxis;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {

        float x = 0;
        float y = 0;

        x = Input.GetAxis("Mouse X") * sens;
        y = Input.GetAxis("Mouse Y") * sens;

        xAxis += x;
        yAxis -= y;

        yAxis = Mathf.Clamp(yAxis, -25, 10);
        transform.rotation = Quaternion.Euler(yAxis, xAxis, 0f);

        transform.position = playerTransform.position + transform.rotation * new Vector3(offset.x, offset.y / 2, offset.z);

    }
}
