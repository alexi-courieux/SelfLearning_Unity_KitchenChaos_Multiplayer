using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PlayerPrefsBindings = "InputBindings";
    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteract; 
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPauseAction;
    public event EventHandler OnKeyRebind;

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        GamepadMove,
        GamepadInteract,
        GamepadInteractAlternate,
        GamepadPause
    }
    
    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractOnPerformed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnPerformed;
        _playerInputActions.Player.Pause.performed += PauseOnPerformed;

        if (PlayerPrefs.HasKey(PlayerPrefsBindings))
        {
            string bindingOverrides = PlayerPrefs.GetString(PlayerPrefsBindings);
            _playerInputActions.LoadBindingOverridesFromJson(bindingOverrides);
        }
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

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.MoveUp => _playerInputActions.Player.Move.bindings[1].ToDisplayString(),
            Binding.MoveDown => _playerInputActions.Player.Move.bindings[2].ToDisplayString(),
            Binding.MoveLeft => _playerInputActions.Player.Move.bindings[3].ToDisplayString(),
            Binding.MoveRight => _playerInputActions.Player.Move.bindings[4].ToDisplayString(),
            Binding.Interact => _playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.InteractAlternate => _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString(),
            Binding.Pause => _playerInputActions.Player.Pause.bindings[0].ToDisplayString(),
            Binding.GamepadMove => _playerInputActions.Player.Move.bindings[5].ToDisplayString(),
            Binding.GamepadInteract => _playerInputActions.Player.Interact.bindings[1].ToDisplayString(),
            Binding.GamepadInteractAlternate => _playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString(),
            Binding.GamepadPause => _playerInputActions.Player.Pause.bindings[1].ToDisplayString(),
            _ => throw new ArgumentOutOfRangeException(nameof(binding), binding, "Binding not implemented")
        };
    }

    public void RebindKey(Binding binding, Action onRebindingComplete)
    {
        _playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            case Binding.MoveUp:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.MoveDown:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.MoveLeft:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.MoveRight:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = _playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = _playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamepadMove:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteract:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GamepadInteractAlternate:
                inputAction = _playerInputActions.Player.InteractAlternate;
                bindingIndex = 1;
                break;
            case Binding.GamepadPause:
                inputAction = _playerInputActions.Player.Pause;
                bindingIndex = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(binding), binding, "Binding not implemented");
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                _playerInputActions.Player.Enable();
                onRebindingComplete();

                string bindings = _playerInputActions.SaveBindingOverridesAsJson();
                PlayerPrefs.SetString(PlayerPrefsBindings, bindings);
                PlayerPrefs.Save();
                
                OnKeyRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }
}