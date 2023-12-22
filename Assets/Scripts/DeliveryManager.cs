using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSo availableRecipeListSo;
    private List<RecipeSo> _waitingRecipeSoList;

    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        _waitingRecipeSoList = new List<RecipeSo>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        spawnRecipeTimer -= Time.deltaTime;
        if (!(spawnRecipeTimer <= 0f)) return;
        spawnRecipeTimer += spawnRecipeTimerMax;
        SpawnRecipe();
    }

    private void SpawnRecipe()
    {
        if (_waitingRecipeSoList.Count >= waitingRecipesMax) return;
        
        var availableRecipes = availableRecipeListSo.RecipeSoList;
        var recipe = availableRecipes[Random.Range(0, availableRecipes.Count)];
        
        _waitingRecipeSoList.Add(recipe);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (var index = 0; index < _waitingRecipeSoList.Count; index++)
        {
            var waitingRecipe = _waitingRecipeSoList[index];
            if (waitingRecipe.ingredients.Count == plateKitchenObject.GetIngredients().Count)
            {
                // Have the same number of ingredients
                var plateIngredientsMatchRecipe = true;
                foreach (var recipeIngredient in waitingRecipe.ingredients)
                {
                    var ingredientMatch = false;
                    foreach (var plateIngredient in plateKitchenObject.GetIngredients())
                    {
                        if (plateIngredient == recipeIngredient)
                        {
                            ingredientMatch = true;
                            break;
                        }
                    }

                    if (!ingredientMatch)
                    {
                        // Ingredients don't match
                        plateIngredientsMatchRecipe = false;
                        break;
                    }
                }

                if (plateIngredientsMatchRecipe)
                {
                    // The correct recipe has been delivered
                    _waitingRecipeSoList.Remove(waitingRecipe);
                    successfulRecipesAmount++;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // The wrong recipe has been delivered
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        return;
    }
    
    public List<RecipeSo> GetWaitingRecipeSoList()
    {
        return _waitingRecipeSoList;
    }
    
    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
