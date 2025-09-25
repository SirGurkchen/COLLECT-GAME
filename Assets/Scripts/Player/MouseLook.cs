using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private GameInput _gameInput;

    private float _xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 lookInput = _gameInput.PlayerLook();

        float mouseX = lookInput.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        _playerBody.Rotate(Vector3.up * mouseX);
    }
}