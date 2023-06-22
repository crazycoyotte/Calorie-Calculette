using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuggestdFoodController : MonoBehaviour
{
    public CSVReader csvReader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateSuggestedFood(string inputText)
    {
        // Erase old list
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Set initial position
        Vector2 startPosition = new Vector2(0f, -30f); // Adjust the y-coordinate as needed

        // Generate new list
        List<string> suggestedFood = csvReader.GetSuggestedFood(inputText);
        int foodInList = suggestedFood.Count;
        float height = GetGameObjectHeight(gameObject);

        // Create a GameObject for each suggested food
        foreach (string food in suggestedFood)
        {
            GameObject foodGO = new GameObject(food);
            foodGO.transform.SetParent(transform);
            
            // Add a TMP component to display the food
            TextMeshProUGUI textMeshPro = foodGO.AddComponent<TextMeshProUGUI>();
            textMeshPro.text = food;

            // Adjust the font size
            textMeshPro.fontSize = 14; // Adjust the font size as needed

            // Adjust the font
            textMeshPro.font = Resources.Load<TMP_FontAsset>("Font/TIMES SDF");

            // Adjust the size of the game object
            RectTransform foodTransform2 = foodGO.GetComponent<RectTransform>();
            float textHeight = textMeshPro.preferredHeight;
            float totalHeight = textHeight + 10f; // Add some extra space for padding
            foodTransform2.sizeDelta = new Vector2(foodTransform2.sizeDelta.x, totalHeight);

            // Adjust the position
            RectTransform foodTransform = foodGO.GetComponent<RectTransform>();
            foodTransform.anchoredPosition = startPosition;
            startPosition += new Vector2(0f, -30f); // Adjust the y-coordinate as needed
        }
    }

    private float GetGameObjectHeight(GameObject gameObject)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            return renderer.bounds.size.y;
        }

        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds.size.y;
        }

        Debug.LogWarning("Aucun Renderer ou Collider trouvé sur le GameObject : " + gameObject.name);
        return 0f;
    }
}
