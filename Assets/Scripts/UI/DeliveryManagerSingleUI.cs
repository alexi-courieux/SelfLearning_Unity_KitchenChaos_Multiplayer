using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSo(RecipeSo recipeSo)
    {
        recipeNameText.text = recipeSo.recipeName;
        foreach (Transform icon in iconContainer)
        {
            if (icon == iconTemplate) continue;
            Destroy(icon.gameObject);
        }

        foreach (var ingredient in recipeSo.ingredients)
        {
            var iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = ingredient.sprite;
        }
    }
}
