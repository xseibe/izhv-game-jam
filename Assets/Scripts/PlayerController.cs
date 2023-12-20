using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera Camera;
    [SerializeField] Transform GroundCheckTransform;

    [Header("Settings")]
    [SerializeField] float WalkSpeed = 4.5f;
    [SerializeField] float CrouchSpeed = 2f;
    [SerializeField] float SprintSpeed = 10f;
    [SerializeField] float Gravity = 9.81f;
    [SerializeField] float JumpHeight = 1f;

    [SerializeField] float GroundDistance = 0.2f;
    [SerializeField] float CameraFovChangeSpeed = 50f;
    [SerializeField] float SprintCameraFov = 80f;

    [SerializeField] LayerMask GroundMask;

    public bool IsGrounded { get; set; }
    public float Speed { get; private set; }

    private CharacterController controller;

    private float jumpTimer;
    private float jumpCooldown;
    private Vector3 velocity;
    private float gravity;

    private bool isCrouching;
    private bool isJumping = false;
    private bool isSprinting;
    private bool isMoving;

    private float standingHeight;
    private float normalCameraFov;
    private float crouchingHeight;

    private float cameraFovChangeSpeed;
    private float mouseSensitivity;

    // Singletons
    private SettingsHelper settingsHelper;

    private KeyCode forwardKey;
    private KeyCode jumpKey;
    private KeyCode sprintKey;
    private KeyCode crouchKey;

    private Vector3 anim;

    // Character Controller
    private Vector3 standingCenter;
    private Vector3 crouchingCenter;

    // PostProcessing
    private MotionBlur motionBlur;

    // Movement
    private Vector3 vec = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        settingsHelper = SettingsHelper.GetInstance();

        gravity = Gravity * -1;
        Speed = WalkSpeed;

        standingHeight = controller.height;
        crouchingHeight = standingHeight * 0.6f;

        standingCenter = controller.center;
        crouchingCenter = new Vector3(standingCenter.x, standingCenter.y - 0.1f, standingCenter.z);

        normalCameraFov = Camera.fieldOfView;

        mouseSensitivity = settingsHelper.MouseSensitivity;
        forwardKey = settingsHelper.ForwardKey;
        jumpKey = settingsHelper.JumpKey;
        sprintKey = settingsHelper.SprintKey;
        crouchKey = settingsHelper.CrouchKey;

        jumpCooldown = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        jumpTimer -= Time.deltaTime;

        IsGrounded = CheckIfGrounded();

        vec.x = Input.GetAxis("Horizontal");
        if (isSprinting) { vec.x *= 0.5f; }
        vec.z = Input.GetAxis("Vertical");

        Vector3 move;

        move = transform.right * vec.x + transform.forward * vec.z;

        controller.Move(((Speed * move) + velocity) * Time.deltaTime);

        anim.x = Mathf.Lerp(anim.x, vec.x, Time.deltaTime * 8f);
        anim.z = Mathf.Lerp(anim.z, vec.z, Time.deltaTime * 8f);

        if (!IsGrounded)
        {
            velocity.y -= Gravity * Time.deltaTime;
        }

        // Landing
        if (isJumping && IsGrounded && velocity.y < 0f)
        {
            jumpTimer = jumpCooldown;
            isJumping = false;
        }

        // Jumping
        if (Input.GetKeyDown(jumpKey) && IsGrounded && jumpTimer <= 0 && !isCrouching && !isJumping)
        {
            isJumping = true;
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

        // Crouching
        if (Input.GetKey(crouchKey) && IsGrounded && !isSprinting)
        {
            float lastHeight = controller.height;
            controller.height = Mathf.MoveTowards(controller.height, crouchingHeight, 3.5f * Time.deltaTime);
            Vector3 newVec = transform.position;
            newVec.y = (controller.height - lastHeight);
            transform.position += newVec * 0.5f;
            isCrouching = true;
            Speed = CrouchSpeed;
        }
        else if (!Input.GetKey(crouchKey))
        {
            controller.height = Mathf.MoveTowards(controller.height, standingHeight, 3.5f * Time.deltaTime);
            isCrouching = false;
            Speed = WalkSpeed;
        }

        // Sprinting
        if (Input.GetKey(sprintKey) && Input.GetKey(forwardKey) && (IsGrounded))
        {
            isSprinting = true;
            cameraFovChangeSpeed = CameraFovChangeSpeed;
            Speed = SprintSpeed;
        }
        else if ((Input.GetKeyUp(sprintKey) || !Input.GetKey(forwardKey)) && !isCrouching)
        {
            isSprinting = false;
            cameraFovChangeSpeed = CameraFovChangeSpeed * -1;
            Speed =  WalkSpeed;
        }

      //  controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (cameraFovChangeSpeed != 0f)
        {
            if ((cameraFovChangeSpeed < 0f && Camera.fieldOfView <= normalCameraFov) || (cameraFovChangeSpeed > 0f && Camera.fieldOfView >= SprintCameraFov))
            {
                cameraFovChangeSpeed = 0f;
            }
            else
            {
                Camera.fieldOfView += cameraFovChangeSpeed * Time.deltaTime;
            }
        }
    }

    private bool CheckIfGrounded()
    {
        return Physics.CheckSphere(GroundCheckTransform.position, controller.radius - 0.005f, GroundMask);
    }
}
