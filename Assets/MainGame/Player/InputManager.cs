using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static CameraController cameraController;

    static KeyCode run = KeyCode.LeftShift;
    public static Vector3 GetDirection()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        
        Vector3 forward = new Vector3(cameraController.transform.forward.x, 0f, cameraController.transform.forward.z).normalized;

        Vector3 movementVertical = forward * y;
        

        return movementVertical;

    }
    public static Vector3 GetSide()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 right = new Vector3(cameraController.transform.forward.x, 0f, 0f).normalized;

        Vector3 movementHorizontal = right * x;

        return movementHorizontal;

    }

    public static bool Run()
    {
        return Input.GetKeyDown(run);
    }
}
