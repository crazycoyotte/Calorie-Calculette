using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class IngredientListController : MonoBehaviour
{
    private List<GameObject> ingredientList = new List<GameObject>();

    public void AddIngredient(GameObject ingredient)
    {
        ingredientList.Add(ingredient);
    }

    public void RemoveIngredient(GameObject ingredient)
    {
        ingredientList.Remove(ingredient);
        UpdateTotalKcal();
    }

    private void UpdateTotalKcal()
    {
        float totalKcal = 0f;
        foreach (GameObject ingredient in ingredientList)
        {
            TMP_Text kcalText = ingredient.transform.Find("Ingredientkcal").GetComponent<TMP_Text>();
            float kcal = 0f;
            if (float.TryParse(kcalText.text, out kcal))
            {
                totalKcal += kcal;
            }
            else
            {
                Debug.LogWarning("Failed to parse kcal value for ingredient: " + ingredient.name);
            }
        }

        TotalKcalController totalKcalController = FindObjectOfType<TotalKcalController>();
        if (totalKcalController != null)
        {
            totalKcalController.UpdateTotalKcal(totalKcal);
        }
        else
        {
            Debug.LogWarning("TotalKcalController not found.");
        }
    }
}
