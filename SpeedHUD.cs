using UnityEngine;
using TMPro;

public class SpeedHUD : MonoBehaviour
{
    public TextMeshProUGUI speedText; // Reference to the TextMeshPro UI element
    public Car car;                  // Reference to the Car script

    void Update()
    {
        if (car != null && speedText != null)
        {
            // Get the car's speed from the Car script and display it
            float speed = car.GetSpeed();
            speedText.text = $"Speed: {Mathf.RoundToInt(speed)} km/h"; // Display speed rounded to nearest integer
        }
    }
}
