using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    [Header("Growth Settings")]
    public float growthDuration = 10f; // Time in seconds for the tree to fully grow
    public Vector3 maxScale = new Vector3(1f, 1f, 1f); // Maximum scale for the trees

    private Vector3 initialScale = Vector3.zero; // Starting scale
    private float growthProgress = 0f; // Progress of the growth (0 to 1)
    private Transform[] treeParts; // Sub-objects (LOD trees)

    void Start()
    {
        // Get all child objects (LOD trees)
        treeParts = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            treeParts[i] = transform.GetChild(i);
        }

        // Set the initial scale of all parts to zero
        foreach (var part in treeParts)
        {
            part.localScale = initialScale;
        }
    }

    void Update()
    {
        if (growthProgress < 1f)
        {
            // Increment growth progress based on time
            growthProgress += Time.deltaTime / growthDuration;

            // Clamp progress to 1 to avoid overshooting
            growthProgress = Mathf.Clamp01(growthProgress);

            // Apply scaling to all parts
            foreach (var part in treeParts)
            {
                part.localScale = Vector3.Lerp(initialScale, maxScale, growthProgress);
            }
        }
    }
}
