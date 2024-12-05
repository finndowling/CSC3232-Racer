using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SkyLightMovement : MonoBehaviour
{
    [Header("Sky Movement Settings")]
    public float movementSpeed = 10f; // Speed of light movement (degrees per second)
    public float orbitRadius = 50f; // Radius of the circular orbit
    public Vector3 orbitCenter = Vector3.zero; // Center point of the orbit

    [Header("Lighting Settings")]
    public Gradient lightColorGradient; // Gradient for light color over the cycle
    public AnimationCurve lightIntensityCurve; // Curve for light intensity over the cycle
    public Light directionalLight; // Reference to the directional light
    public Gradient ambientColorGradient; // Gradient for ambient light color
    public Material skyboxMaterial; // Reference to the skybox material (if applicable)

    private float currentTime; // Current time of the cycle, normalized (0 to 1)

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>();
            if (directionalLight == null)
            {
                Debug.LogError("SkyLightMovement: No Light component found!");
                enabled = false;
                return;
            }
        }

        // Initialize skybox material if assigned
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }

        currentTime = 0f; // Start at the beginning of the cycle
    }

    void Update()
    {
        MoveLight();
        UpdateLighting();
    }

    private void MoveLight()
    {
        // Update the normalized time for the day-night cycle
        currentTime += movementSpeed * Time.deltaTime / 360f; // Normalize to a full cycle
        currentTime %= 1f; // Keep within [0, 1] range

        // Calculate the new position of the light along a circular path
        float angle = currentTime * 2f * Mathf.PI; // Convert normalized time to radians
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * orbitRadius;
        transform.position = orbitCenter + offset;

        // Ensure the light always points toward the center of the orbit
        transform.LookAt(orbitCenter);
    }

    private void UpdateLighting()
    {
        // Update the light color and intensity based on the current time
        if (lightColorGradient != null)
        {
            directionalLight.color = lightColorGradient.Evaluate(currentTime);
        }

        if (lightIntensityCurve != null)
        {
            directionalLight.intensity = lightIntensityCurve.Evaluate(currentTime);
        }

        // Update the ambient light color
        if (ambientColorGradient != null)
        {
            RenderSettings.ambientLight = ambientColorGradient.Evaluate(currentTime);
        }

        // Update the skybox material (if assigned)
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Exposure", lightIntensityCurve.Evaluate(currentTime) * 2f); // Example exposure adjustment
        }
    }
}
