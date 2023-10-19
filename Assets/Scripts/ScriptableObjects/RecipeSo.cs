using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe/Recipe")]
public class RecipeSo : ScriptableObject
{
    public List<KitchenObjectSO> ingredients;
    public String recipeName;
}