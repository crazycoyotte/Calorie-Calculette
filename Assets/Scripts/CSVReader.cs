using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;

    [System.Serializable]
    public class Food
    {
        public string name;
        public int rawKCal;
        public int cookedKCal;
    }

    [System.Serializable]
    public class FoodList
    {
        public Food[] food;
    }

    public FoodList foodList = new FoodList();
    
    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int tableSize = (data.Length - 3) / 3;
        foodList.food = new Food[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            foodList.food[i] = new Food();
            foodList.food[i].name = data[3 * (i + 1)];
            int rawKCal;
            if (int.TryParse(data[3 * (i + 1) + 1], out rawKCal))
            {
                foodList.food[i].rawKCal = rawKCal;
            }
            else
            {
                Debug.LogWarning("Failed to parse rawKCal for food: " + foodList.food[i].name);
            }

            int cookedKCal;
            if (int.TryParse(data[3 * (i + 1) + 2], out cookedKCal))
            {
                foodList.food[i].cookedKCal = cookedKCal;
            }
            else
            {
                Debug.LogWarning("Failed to parse cookedKCal for food: " + foodList.food[i].name);
            }
        }
    }

    public List<String> GetFoodNames()
    {
        List<string> foodNames = new List<string>();

        foreach (Food food in foodList.food)
        {
            foodNames.Add(food.name);
        }

        return foodNames;
    }

    public List<string> GetSuggestedFood(string inputText)
    {
        List<string> suggestedFood = new List<string>();
        

        foreach (Food food in foodList.food)
        {
            if (food.name.StartsWith(inputText, System.StringComparison.OrdinalIgnoreCase))
            {
                suggestedFood.Add(food.name);
            }
        }

        return suggestedFood;
    }

    public float GetRawKcal(string foodName)
    {
        float rawKcal = 0f;

        foreach (Food food in foodList.food)
        {
            if (food.name.Equals(foodName, StringComparison.OrdinalIgnoreCase))
            {
                rawKcal = food.rawKCal;
                break;
            }
        }

        return rawKcal;
    }
}
