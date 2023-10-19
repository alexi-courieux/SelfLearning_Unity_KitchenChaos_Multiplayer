using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconsTemplate;


    private void Awake()
    {
        iconsTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnIngredientAdded;
    }

    private void PlateKitchenObjectOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if(child == iconsTemplate) continue;
            Destroy(child.gameObject);
        }
        
        var ingredients = plateKitchenObject.GetIngredients();
        foreach (var ingredient in ingredients)
        {
            var iconTransform = Instantiate(iconsTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSo(ingredient);
        }
    }
}
