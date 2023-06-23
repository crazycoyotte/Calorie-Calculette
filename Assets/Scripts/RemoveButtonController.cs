using UnityEngine;


public class RemoveButtonController : MonoBehaviour
{
    public GameObject ingredientObject;

    public void OnRemoveButtonClick()
    {
        // Retirer l'objet ingredientObject de la liste
        IngredientListController ingredientListController = FindObjectOfType<IngredientListController>();
        if (ingredientListController != null)
        {
            ingredientListController.RemoveIngredient(ingredientObject);
        }
        else
        {
            Debug.LogWarning("IngredientListController not found.");
        }

        // Détruire l'objet ingredientObject
        Destroy(ingredientObject);
    }
}
