using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float defaultMoveSpeed = 5f;
    [SerializeField] private float shiftMoveSpeed = 8f;
    [SerializeField] private float camBobSpeed = 10f;
    [SerializeField] private float maxCamBob = 0.1f;

    private float moveSpeed;
    private Vector3 velocity;
    private Vector3 camOffset;

    private void Start()
    {
        camOffset = playerCamera.transform.localPosition;
    }

    private void Update()
    {
        HandleBob();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (gameInput.ShiftIsPressed())
        {
            moveSpeed = shiftMoveSpeed;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
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
        if (gameInput.ShiftIsPressed() && rb.linearVelocity.magnitude > 0.1f)
        {
            float bob = Mathf.Sin(Time.time * camBobSpeed) * maxCamBob;
            playerCamera.transform.localPosition = camOffset + new Vector3(0, bob, 0);
        }
        else
        {
            playerCamera.transform.localPosition = camOffset;
        }
    }
}