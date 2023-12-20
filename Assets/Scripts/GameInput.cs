using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteract; 
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseAction;
    
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractOnPerformed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
        _playerInputActions.Player.Pause.performed += PauseOnPerformed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= InteractOnPerformed;
        _playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnPerformed;
        _playerInputActions.Player.Pause.performed -= PauseOnPerformed;
        
        _playerInputActions.Dispose();
    }

    private void PauseOnPerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternateOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnPerformed(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}