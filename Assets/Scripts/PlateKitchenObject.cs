using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {


    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO KitchenObjectSo;
    }


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;


    private List<KitchenObjectSO> kitchenObjectSoList;


    protected override void Awake() {
        base.Awake();
        kitchenObjectSoList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSo)) {
            // Not a valid ingredient
            return false;
        }
        if (kitchenObjectSoList.Contains(kitchenObjectSo)) {
            // Already has this type
            return false;
        }

        AddIngredientServerRpc(
            KitchenGameMultiplayer.Instance.GetKitchenObjectSOIndex(kitchenObjectSo)
        );
        return true;
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void AddIngredientServerRpc(int kitchenObjectSoIndex)
    {
        AddIngredientClientRpc(kitchenObjectSoIndex);
    }
    
    [ClientRpc]
    private void AddIngredientClientRpc(int kitchenObjectSoIndex)
    {
        KitchenObjectSO kitchenObjectSo = KitchenGameMultiplayer.Instance.GetKitchenObjectSoFromIndex(kitchenObjectSoIndex);
        kitchenObjectSoList.Add(kitchenObjectSo);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
            KitchenObjectSo = kitchenObjectSo
        });
    }

    public List<KitchenObjectSO> GetKitchenObjectSoList() {
        return kitchenObjectSoList;
    }

}