using UnityEngine;

public class BoostZone : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 2f; // Multiplier for the boost speed
    [SerializeField] private float boostDuration = 2f;  // Duration of the boost in seconds

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the zone is the player
        GameObject car = other.attachedRigidbody?.gameObject ?? other.transform.root.gameObject;

        if (car.CompareTag("Player"))
        {
            Car carScript = car.GetComponent<Car>();
            if (carScript != null)
            {
                StartCoroutine(ApplyBoost(carScript));
            }
        }
    }

    private System.Collections.IEnumerator ApplyBoost(Car car)
    {
        float originalSpeed = car.drivespeed;

        // Apply the boost
        car.drivespeed *= boostMultiplier;
        Debug.Log($"{car.name} entered the boost zone! Speed increased to {car.drivespeed}.");

        // Wait for the boost duration
        yield return new WaitForSeconds(boostDuration);

        // Reset the speed to its original value
        car.drivespeed = originalSpeed;
        Debug.Log($"{car.name} boost ended. Speed reset to {car.drivespeed}.");
    }
}
