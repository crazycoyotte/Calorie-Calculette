using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuggestdFoodController : MonoBehaviour
{
    public CSVReader csvReader;
    public Material yourMaterial;
    public RectTransform placeHolder;
    public TMP_InputField foodInputField;
    public TextMeshProUGUI kcalFor100gText;

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

        // Set initial position below the PlaceHolder
        Vector2 startPosition = new Vector2(placeHolder.anchoredPosition.x, placeHolder.anchoredPosition.y - placeHolder.sizeDelta.y);

        // Generate new list
        List<string> suggestedFood = csvReader.GetSuggestedFood(inputText);
        int foodInList = suggestedFood.Count;
        float height = GetGameObjectHeight(gameObject);

        // Create a GameObject for each suggested food
        foreach (string food in suggestedFood)
        {
            // Create a new Button object
            GameObject buttonGO = new GameObject(food);
            buttonGO.transform.SetParent(transform);

            Button button = buttonGO.AddComponent<Button>();

            // Add a TextMeshProUGUI component for the button label
            TextMeshProUGUI textMeshPro = buttonGO.AddComponent<TextMeshProUGUI>();
            textMeshPro.text = food;

            // Add a Renderer component to the GameObject
            MeshRenderer meshRenderer = buttonGO.AddComponent<MeshRenderer>();

            // Set the material and other properties of the Renderer as needed
            GetComponent<Renderer>().material = yourMaterial;

            // Adjust the font size
            textMeshPro.fontSize = 14;

            // Adjust the font
            textMeshPro.font = Resources.Load<TMP_FontAsset>("Font/TIMES SDF");

            // Adjust the size of the game object
            RectTransform foodTransform2 = buttonGO.GetComponent<RectTransform>();
            float textHeight = textMeshPro.preferredHeight;
            float totalHeight = textHeight + 10f; // Add some extra space for padding
            foodTransform2.sizeDelta = new Vector2(foodTransform2.sizeDelta.x, totalHeight);

            // Adjust the position
            RectTransform foodTransform = buttonGO.GetComponent<RectTransform>();
            foodTransform.anchoredPosition = startPosition;
            startPosition += new Vector2(0f, -30f);

            // Add a click event handler for the button
            button.onClick.AddListener(() => OnFoodButtonClick(food));
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

        // Si aucun Renderer ni Collider n'est trouv�, retourner une valeur par d�faut
        Debug.LogWarning("Aucun Renderer ou Collider trouv� sur le GameObject : " + gameObject.name);
        return 0f;
    }

    // Event handler for food button click
    public void OnFoodButtonClick(string foodName)
    {
        // Update the InputField with the name of the button clicked
        foodInputField.text = foodName;

        // Get "rawKcal" of the matching food
        float rawKcal = csvReader.GetRawKcal(foodName);

        // Update "KcalFor100g" with "rawKcal"
        kcalFor100gText.text = rawKcal.ToString();

    }
}
