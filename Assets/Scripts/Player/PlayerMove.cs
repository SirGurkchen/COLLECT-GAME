using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private PlayerInteract _interact;
    [SerializeField] private float _defaultMoveSpeed = 5f;
    [SerializeField] private float _shiftMoveSpeed = 8f;
    [SerializeField] private float _crouchedMoveSpeed = 3f;
    [SerializeField] private float _camBobSpeed = 10f;
    [SerializeField] private float _maxCamBob = 0.1f;
    [SerializeField] private float _uncroachRayDistance = 1f;
    [SerializeField] private float _radius = 0.5f;

    private float _moveSpeed;
    private Vector3 _velocity;
    private Vector3 _camOffset;
    private bool _isCrouched;

    private enum State { Walking, Running, Standing };
    private State _currentState;

    private void Start()
    {
        _camOffset = _playerCamera.transform.localPosition;
        _isCrouched = false;
        _currentState = State.Standing;
    }

    private void OnEnable()
    {
        _gameInput.OnCrouchPress += OnCrouchPress;
    }

    private void OnDisable()
    {
        _gameInput.OnCrouchPress -= OnCrouchPress;
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

        _velocity = _rb.linearVelocity;
        _velocity.x = moveVec.x * _moveSpeed;
        _velocity.z = moveVec.z * _moveSpeed;

        _rb.linearVelocity = _velocity;
    }

    private void SetPlayerMoveState()
    {
        if (_gameInput.ShiftIsPressed())
        {
            if (!_isCrouched)
            {
                _moveSpeed = _shiftMoveSpeed;
                _currentState = State.Running;
            }
            else
            {
                _currentState = State.Walking;
            }
        }
        else
        {
            if (!_isCrouched)
            {
                _moveSpeed = _defaultMoveSpeed;
            }
            _currentState = State.Walking;
        }

        if (_rb.linearVelocity.magnitude <= 0.1f)
        {
            _currentState = State.Standing;
        }
    }

    private void HandleBob()
    {
        if (_currentState == State.Running && !_isCrouched && !(_currentState == State.Standing))
        {
            float bob = Mathf.Sin(Time.time * _camBobSpeed) * _maxCamBob;
            _playerCamera.transform.localPosition = _camOffset + new Vector3(0, bob, 0);
        }
        else
        {
            _playerCamera.transform.localPosition = _camOffset;
        }
    }

    private void HandleWalkingAudio()
    {
        bool isMoving = !(_currentState == State.Standing);
        bool isWalking = _currentState == State.Walking && isMoving;
        bool isRunning = _currentState == State.Running && isMoving && !_isCrouched;

        _audioManager.PlayWalkSound(isWalking, isRunning);
    }

    private void OnCrouchPress()
    {
        if (_interact.GetHeldItem() != null)
        {
            return;
        }

        if (!_isCrouched)
        {
            _isCrouched = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.4f, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            _moveSpeed = _crouchedMoveSpeed;
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
        return _isCrouched;
    }

    public void SetStanding()
    {
        _isCrouched = false;
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _capsuleCollider.height = 2;
        _moveSpeed = _defaultMoveSpeed;
    }
}