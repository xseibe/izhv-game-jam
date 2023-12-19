using UnityEngine;

public class CameraShakeMovement : MonoBehaviour
{
    [SerializeField, Range(0, 0.1f)] private float Amplitude = 0.015f;
    [SerializeField] private Transform cameraPos = null;
    [SerializeField] private Transform cameraHolderPos = null;

    private float toggleSpeed = 1.0f;
    private Vector3 startPos;
    private CharacterController controller;

    private PlayerController playerController;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        startPos = cameraPos.localPosition;
    }

    void Update()
    {
        CheckMotion();
        ResetPosition();
        cameraPos.LookAt(FocusTarget());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * playerController.Speed * 2f) * Amplitude;
        pos.x += Mathf.Cos(Time.time * playerController.Speed * 2f / 2f) * Amplitude * 2f;
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        Debug.Log("X: " + controller.velocity.x);
        Debug.Log("Z: " + controller.velocity.z);


        if (speed < toggleSpeed) return;
        if (!playerController.IsGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void PlayMotion(Vector3 motion)
    {
        cameraPos.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolderPos.localPosition.y, transform.position.z);
        pos += cameraHolderPos.forward * 15.0f;
        return pos;
    }
    private void ResetPosition()
    {
        if (cameraPos.localPosition == startPos) return;
        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, startPos, 1 * Time.deltaTime);
    }
}