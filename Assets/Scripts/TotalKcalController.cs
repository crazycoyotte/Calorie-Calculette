using UnityEngine;
using TMPro;

public class TotalKcalController : MonoBehaviour
{
    public TMP_Text totalKcalText;

    public void UpdateTotalKcal(float totalKcal)
    {
        totalKcalText.text = totalKcal.ToString();
    }
}
