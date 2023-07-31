using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipe", menuName = "ScriptableObjects/Recipe/BurningRecipe", order = 1)]
public class BurningRecipeSo : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTime;
}
