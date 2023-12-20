using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;

    private SettingsHelper settingsHelper;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float animX = 0f;

    private float sensitivity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        settingsHelper = SettingsHelper.GetInstance();
        sensitivity = settingsHelper.MouseSensitivity;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -65f, 65f);
        rotationY += mouseX;

        // Apply rotations to the camera's local rotation
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        animX = Mathf.Lerp(animX, Input.GetAxis("Mouse X") / (sensitivity * 3.2f), 3.2f * Time.deltaTime);
        animX = Mathf.Clamp(animX, -1f, 1f);
    }
}
