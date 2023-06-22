using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Autocomplete : MonoBehaviour
{
    public CSVReader csvReader;
    public SuggestdFoodController suggestdFoodController;

    private TMP_InputField inputField;
    private List<string> foodNames;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        foodNames = csvReader.GetFoodNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnValueChanged()
    {
        string inputText = inputField.text;
        Debug.Log(inputText);

        suggestdFoodController.GenerateSuggestedFood(inputText);
    }

    public void OnEndEdit()
    {
        string inputeText = inputField.text;

        bool isValidInput = foodNames.Exists(name => name.Equals(inputeText, System.StringComparison.OrdinalIgnoreCase));

        if (!isValidInput)
        {
            inputField.text = string.Empty;
        }
    }
}
