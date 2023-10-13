using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredients;
    
    private List<KitchenObjectSO> _ingredients;

    private void Awake()
    {
        _ingredients = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if (_ingredients.Contains(kitchenObjectSo)) return false;
        if (!validIngredients.Contains(kitchenObjectSo)) return false;
        
        _ingredients.Add(kitchenObjectSo);
        return true;
    }
}
