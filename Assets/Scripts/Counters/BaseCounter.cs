using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform kitchenObjectHoldPoint;
    
    public static event EventHandler OnAnyObjectPlacedHere;
    public static void ResetStaticData()
    {
        OnAnyObjectPlacedHere = null;
    }
    
    private KitchenObject _kitchenObject;
    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player)
    {
        // Not doing anything, but can be overridden, it's a bad pattern though
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;

        if (kitchenObject != null) 
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
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