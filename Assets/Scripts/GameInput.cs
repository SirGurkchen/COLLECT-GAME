using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event Action OnCrouchPress;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Crouch.performed += Crouch_performed;
    }

    private void Crouch_performed(InputAction.CallbackContext obj)
    {
        OnCrouchPress?.Invoke();
    }

    public Vector2 PlayerMovementNormalized()
    {
        Vector2 inputVec = inputActions.Player.Move.ReadValue<Vector2>();
        inputVec = inputVec.normalized;
        return inputVec;
    }

    public Vector2 PlayerLook()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool ShiftIsPressed()
    {
        return inputActions.Player.Run.IsPressed();
    }
}