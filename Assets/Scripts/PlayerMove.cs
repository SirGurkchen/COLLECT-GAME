using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float shiftMoveSpeed = 8f;
    [SerializeField] private float crouchedMoveSpeed = 3f;
    [SerializeField] private float camBobSpeed = 10f;
    [SerializeField] private float maxCamBob = 0.1f;
    [SerializeField] private float uncroachRayDistance = 1f;
    [SerializeField] private float radius = 0.5f;

    private float moveSpeed;
    private Vector3 velocity;
    private Vector3 camOffset;
    private bool isCrouched;

    private enum State { Walking, Running, Standing };
    private State currentState;

    private void Start()
    {
        camOffset = playerCamera.transform.localPosition;
        isCrouched = false;
        currentState = State.Standing;

        gameInput.OnCrouchPress += OnCrouchPress;
    }

    private void Update()
    {
        HandleBob();
        HandleWalkingAudio();
        SetPlayerMoveState();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveDir = gameInput.PlayerMovementNormalized();

        Vector3 moveVec = new Vector3(moveDir.x, 0f, moveDir.y);
        moveVec = transform.TransformDirection(moveVec);

        velocity = rb.linearVelocity;
        velocity.x = moveVec.x * moveSpeed;
        velocity.z = moveVec.z * moveSpeed;



        rb.linearVelocity = velocity;
    }

    private void SetPlayerMoveState()
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

        if (rb.linearVelocity.magnitude <= 0.1f)
        {
            currentState = State.Standing;
        }
    }

    private void HandleBob()
    {
        if (currentState == State.Running && !isCrouched && !(currentState == State.Standing))
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
        bool isMoving = !(currentState == State.Standing);
        bool isWalking = currentState == State.Walking && isMoving;
        bool isRunning = currentState == State.Running && isMoving && !isCrouched;

        audioManager.PlayWalkSound(isWalking, isRunning);
    }

    private void OnCrouchPress()
    {
        if (!isCrouched)
        {
            isCrouched = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.4f, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            moveSpeed = crouchedMoveSpeed;
        }
        else
        {
            if (!Physics.SphereCast(transform.position, radius, Vector3.up, out RaycastHit hit, uncroachRayDistance))
            {
                isCrouched = false;
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                capsuleCollider.height = 2;
                moveSpeed = defaultMoveSpeed;
            }
        }
    }
}