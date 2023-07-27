using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    public KitchenObjectSO KitchenObjectSo => kitchenObjectSo;

    private IKitchenObjectParent _kitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectParent value)
    {
        if (value.HasKitchenObject() && value.GetKitchenObject() != this)
        {
            Debug.LogError("Target is already carrying a kitchen object!");
            return;
        }

        if (_kitchenObjectParent != null)
        {
            _kitchenObjectParent.ClearKitchenObject();
        }

        _kitchenObjectParent = value;
        _kitchenObjectParent.SetKitchenObject(this);
        transform.parent = _kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() => _kitchenObjectParent;

    [SerializeField] private KitchenObjectSO kitchenObjectSo;
}