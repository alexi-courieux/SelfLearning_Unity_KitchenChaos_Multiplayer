using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSo availableRecipeListSo;
    private List<RecipeSo> _waitingRecipeSoList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        _waitingRecipeSoList = new List<RecipeSo>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer += spawnRecipeTimerMax;
            SpawnRecipe();
        }
    }

    private void SpawnRecipe()
    {
        if (_waitingRecipeSoList.Count >= waitingRecipesMax) return;
        
        var availableRecipes = availableRecipeListSo.RecipeSoList;
        var recipe = availableRecipes[Random.Range(0, availableRecipes.Count)];
        
        _waitingRecipeSoList.Add(recipe);
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
                    Debug.Log("Correct recipe delivered - " + waitingRecipe.name);
                    return;
                }
            }
        }

        // The wrong recipe has been delivered
        Debug.Log("Wrong recipe delivered");
        return;
    }
}
