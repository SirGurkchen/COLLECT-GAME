using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float shiftMoveSpeed = 8f;
    [SerializeField] private float crouchedMoveSpeed = 3f;
    [SerializeField] private float camBobSpeed = 10f;
    [SerializeField] private float maxCamBob = 0.1f;

    private float moveSpeed;
    private Vector3 velocity;
    private Vector3 camOffset;
    private bool isCrouched;

    private enum State { Walking, Running };
    private State currentState;

    private void Start()
    {
        camOffset = playerCamera.transform.localPosition;
        isCrouched = false;
    }

    private void Update()
    {
        HandleBob();
        HandleWalkingAudio();
        CheckCrouch();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (gameInput.ShiftIsPressed())
        {
            if (!isCrouched)
            {
                moveSpeed = shiftMoveSpeed;
                currentState = State.Running;
            }
            else
            {
                currentState = State.Walking;
            }
        }
        else
        {
            if (!isCrouched)
            {
                moveSpeed = defaultMoveSpeed;
            }
            currentState = State.Walking;
        }

        Vector2 moveDir = gameInput.PlayerMovementNormalized();

        Vector3 moveVec = new Vector3(moveDir.x, 0f, moveDir.y);
        moveVec = transform.TransformDirection(moveVec);

        velocity = rb.linearVelocity;
        velocity.x = moveVec.x * moveSpeed;
        velocity.z = moveVec.z * moveSpeed;

        rb.linearVelocity = velocity;
    }

    private void HandleBob()
    {
        if (currentState == State.Running && !isCrouched)
        {
            float bob = Mathf.Sin(Time.time * camBobSpeed) * maxCamBob;
            playerCamera.transform.localPosition = camOffset + new Vector3(0, bob, 0);
        }
        else
        {
            playerCamera.transform.localPosition = camOffset;
        }
    }

    private void HandleWalkingAudio()
    {
        bool isMoving = rb.linearVelocity.magnitude > 0.1f;
        bool isWalking = currentState == State.Walking && isMoving;
        bool isRunning = currentState == State.Running && isMoving && !isCrouched;

        audioManager.PlayWalkSound(isWalking, isRunning);
    }

    private void CheckCrouch()
    {
        if (gameInput.CrouchWasPressed())
        {
            if (!isCrouched)
            {
                isCrouched = true;
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0.5f, gameObject.transform.localScale.z);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
                moveSpeed = crouchedMoveSpeed;
            }
            else
            {
                isCrouched = false;
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, gameObject.transform.localScale.z);
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1f, gameObject.transform.position.z);
                moveSpeed = defaultMoveSpeed;
            }
        }
    }
}