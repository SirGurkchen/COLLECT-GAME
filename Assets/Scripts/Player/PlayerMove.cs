using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private PlayerInteract interact;

    [SerializeField] private float _defaultMoveSpeed = 5f;
    [SerializeField] private float _shiftMoveSpeed = 8f;
    [SerializeField] private float _crouchedMoveSpeed = 3f;
    [SerializeField] private float _camBobSpeed = 10f;
    [SerializeField] private float _maxCamBob = 0.1f;
    [SerializeField] private float _uncroachRayDistance = 1f;
    [SerializeField] private float _radius = 0.5f;

    private float moveSpeed;
    private Vector3 velocity;
    private Vector3 camOffset;
    private bool isCrouched;

    private enum State { Walking, Running, Standing };
    private State currentState;

    private void Start()
    {
        camOffset = _playerCamera.transform.localPosition;
        isCrouched = false;
        currentState = State.Standing;

        _gameInput.OnCrouchPress += OnCrouchPress;
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
        Vector2 moveDir = _gameInput.PlayerMovementNormalized();

        Vector3 moveVec = new Vector3(moveDir.x, 0f, moveDir.y);
        moveVec = transform.TransformDirection(moveVec);

        velocity = _rb.linearVelocity;
        velocity.x = moveVec.x * moveSpeed;
        velocity.z = moveVec.z * moveSpeed;

        _rb.linearVelocity = velocity;
    }

    private void SetPlayerMoveState()
    {
        if (_gameInput.ShiftIsPressed())
        {
            if (!isCrouched)
            {
                moveSpeed = _shiftMoveSpeed;
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
                moveSpeed = _defaultMoveSpeed;
            }
            currentState = State.Walking;
        }

        if (_rb.linearVelocity.magnitude <= 0.1f)
        {
            currentState = State.Standing;
        }
    }

    private void HandleBob()
    {
        if (currentState == State.Running && !isCrouched && !(currentState == State.Standing))
        {
            float bob = Mathf.Sin(Time.time * _camBobSpeed) * _maxCamBob;
            _playerCamera.transform.localPosition = camOffset + new Vector3(0, bob, 0);
        }
        else
        {
            _playerCamera.transform.localPosition = camOffset;
        }
    }

    private void HandleWalkingAudio()
    {
        bool isMoving = !(currentState == State.Standing);
        bool isWalking = currentState == State.Walking && isMoving;
        bool isRunning = currentState == State.Running && isMoving && !isCrouched;

        _audioManager.PlayWalkSound(isWalking, isRunning);
    }

    private void OnCrouchPress()
    {
        if (interact.GetHeldItem() != null)
        {
            return;
        }

        if (!isCrouched)
        {
            isCrouched = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.4f, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            moveSpeed = _crouchedMoveSpeed;
        }
        else
        {
            if (!Physics.SphereCast(transform.position, _radius, Vector3.up, out RaycastHit hit, _uncroachRayDistance))
            {
                SetStanding();
            }
        }
    }

    public bool GetCrouched()
    {
        return isCrouched;
    }

    public void SetStanding()
    {
        isCrouched = false;
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _capsuleCollider.height = 2;
        moveSpeed = _defaultMoveSpeed;
    }
}