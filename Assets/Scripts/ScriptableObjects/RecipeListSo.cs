using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeList", menuName = "ScriptableObjects/Lists/RecipeList")]
public class RecipeListSo : ScriptableObject
{
    public List<RecipeSo> RecipeSoList;
}
