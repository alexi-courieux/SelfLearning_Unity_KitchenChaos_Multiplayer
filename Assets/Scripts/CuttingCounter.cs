using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSo[] cuttingRecipeSoArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasCuttingRecipeWithInput(player.GetKitchenObject().KitchenObjectSo))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (!HasKitchenObject()) return;

        var output = GetOutputFromInput(GetKitchenObject().KitchenObjectSo);
        if (output == null) return;

        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(output, this);
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO input)
    {
        return (from cuttingRecipeSo in cuttingRecipeSoArray where cuttingRecipeSo.input == input select cuttingRecipeSo.output).FirstOrDefault();
    }
    
    private bool HasCuttingRecipeWithInput(KitchenObjectSO input)
    {
        return cuttingRecipeSoArray.Any(cuttingRecipeSo => cuttingRecipeSo.input == input);
    }
}
