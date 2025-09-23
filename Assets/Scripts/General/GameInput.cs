using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event Action OnCrouchPress;
    public event Action OnInteractPress;
    public event Action OnShoot;

    private InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Crouch.performed += Crouch_performed;
        _inputActions.Player.Interact.performed += Interact_performed;
        _inputActions.Player.Shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        OnShoot?.Invoke();
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractPress?.Invoke();
    }

    private void Crouch_performed(InputAction.CallbackContext obj)
    {
        OnCrouchPress?.Invoke();
    }

    public Vector2 PlayerMovementNormalized()
    {
        Vector2 inputVec = _inputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        return inputVec;
    }

    public Vector2 PlayerLook()
    {
        return _inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool ShiftIsPressed()
    {
        return _inputActions.Player.Run.IsPressed();
    }
}