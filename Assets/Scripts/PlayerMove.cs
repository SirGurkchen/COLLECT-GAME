using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float moveSpeed;

    private Vector3 velocity;

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
}