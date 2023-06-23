using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.Linq;

public class AddButtonController : MonoBehaviour
{
    public TMP_Text kcalFor100gText;
    public TMP_InputField quantityInput;
    public CSVReader csvReader;
    public Transform ingredientListParent;
    public GameObject ingredientPrefab;
    public TMP_Text totalKcalText;

    private List<GameObject> ingredientList = new List<GameObject>();
    private Vector3 startIngredientPosition;

    private void Start()
    {
        startIngredientPosition = ingredientPrefab.transform.position;
    }

    public void OnAddButtonClick()
    {
        string foodName = transform.parent.Find("SearchFood/Food/Text").GetComponent<TMP_Text>().text;

        // Vérifier si foodName correspond à l'un des aliments dans foodList
        bool isValidFood = true;
        /*foreach (CSVReader.Food food in csvReader.foodList.food)
        {
            if (food.name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
            {
                
                isValidFood = true;
                break;
            }
            Debug.Log($"-{food.name}-{foodName}-");
        }*/

        if (isValidFood)
        {
            // Le foodName est valide, continuer avec l'ajout de l'ingrédient
            string kcalFor100g = kcalFor100gText.text;
            string quantity = quantityInput.text;

            float kcalForQuantity = float.Parse(quantity) * float.Parse(kcalFor100g) / 100f;

            GameObject newIngredient = Instantiate(ingredientPrefab, ingredientListParent);

            Vector3 ingredientPosition = startIngredientPosition + new Vector3(0f, -ingredientList.Count * 30f, 0f);
            newIngredient.transform.position = ingredientPosition;

            newIngredient.transform.Find("IngredientName").GetComponent<TMP_Text>().text = foodName;
            newIngredient.transform.Find("IngredientQty").GetComponent<TMP_Text>().text = quantity;
            newIngredient.transform.Find("Ingredientkcal").GetComponent<TMP_Text>().text = kcalForQuantity.ToString();
            newIngredient.SetActive(true);

            Debug.Log("Added ingredient: " + foodName + " with quantity: " + quantity + " and kcalForQuantity: " + kcalForQuantity);

            // Ajouter le nouvel ingrédient à la liste
            ingredientList.Add(newIngredient);
        }
        else
        {
            Debug.Log("Invalid food name: " + foodName);
            // Faire quelque chose en cas de nom d'aliment invalide
        }

        UpdateTotalKcal();
    }

    private void UpdateTotalKcal()
    {
        float totalKcal = ingredientList.Sum(ingredient => float.Parse(ingredient.transform.Find("Ingredientkcal").GetComponent<TMP_Text>().text));
        totalKcalText.text = totalKcal.ToString();
    }
}
