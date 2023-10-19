using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform recipeContainer;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManagerOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManagerOnRecipeCompleted;
    }

    private void DeliveryManagerOnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManagerOnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform recipe in recipeContainer)
        {
            if (recipe == recipeTemplate) continue;
            Destroy(recipe.gameObject);
        }
            
        foreach (var waitingRecipe in DeliveryManager.Instance.GetWaitingRecipeSoList())
        {
            var recipeTransform = Instantiate(recipeTemplate, recipeContainer);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSo(waitingRecipe);
        }
        
    }
}
