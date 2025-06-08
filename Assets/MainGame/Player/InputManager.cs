using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static CameraController cameraController;

    static KeyCode run = KeyCode.LeftShift;

    static KeyCode shoot = KeyCode.Space;
    public static Vector3 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 forward = cameraController.transform.forward;
        Vector3 right = cameraController.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * y + right * x;
      

        return movement;
    }

    public static bool Shoot()
    {
        return Input.GetKeyDown(shoot);
    }

    public static bool Run()
    {
        return Input.GetKeyDown(run);
    }
}
