using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    private static Player _instance;

    public static Player Instance
    {
        get => _instance;
        private set
        {
            if (_instance != null)
            {
                Debug.LogError("There is more than one Player instance in the scene!");
                return;
            }

            _instance = value;
        }
    }

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    public bool IsWalking() => _isWalking;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private KitchenObject _kitchenObject;
    private bool _isWalking = false;
    private Vector3 _lastInteractDirection;

    private BaseCounter _selectedCounter;

    private BaseCounter SelectedCounter
    {
        get => _selectedCounter;
        set
        {
            _selectedCounter = value;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter = _selectedCounter
            });
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteract += HandleInteractionInput;
        gameInput.OnInteractAlternate += HandleInteractionAlternateInput;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        var moveDistance = moveSpeed * Time.deltaTime;

        const float playerRadius = 1.3f / 2f;
        const float playerHeight = 1.8f;
        var canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, moveDirection, moveDistance);

        _isWalking = false;

        if (!canMove)
        {
            // Cannot move towards moveDirection

            //Attempt only X movement
            var moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = moveDirection.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveDirectionX, moveDistance);
            if (canMove) moveDirection = moveDirectionX;
            else
            {
                //Attempt only Z movement
                var moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveDirectionZ, moveDistance);
                if (canMove) moveDirection = moveDirectionZ;
                else
                {
                    // Cannot move towards moveDirectionX or moveDirectionZ
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }

        _isWalking = moveDirection != Vector3.zero;

        const float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotationSpeed * Time.deltaTime);
    }

    private void HandleInteractionInput(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (SelectedCounter != null)
        {
            SelectedCounter.Interact(this);
        }
    }

    private void HandleInteraction()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }

        const float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out var hitInfo, interactDistance,
                countersLayerMask))
        {
            SelectedCounter = hitInfo.transform.TryGetComponent(out BaseCounter counter) ? counter : null;
        }
        else
        {
            SelectedCounter = null;
        }
    }
    
    private void HandleInteractionAlternateInput(object sender, EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternate(this);
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        
        if(kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}