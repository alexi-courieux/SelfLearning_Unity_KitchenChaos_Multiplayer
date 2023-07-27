using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] protected KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;

        // Spawn a new kitchen object
        var kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
    }
}