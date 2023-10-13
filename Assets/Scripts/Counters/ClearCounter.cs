public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There's nothing on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            // There's a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
                {
                    // Player is carrying a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().KitchenObjectSo))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is carrying something that is not a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // There's a plate on the counter
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().KitchenObjectSo))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}