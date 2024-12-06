using UnityEngine;

public class BoostZone : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 3f; // Multiplier for the boost speed
    [SerializeField] private float boostDuration = 2f;  // Duration of the boost in seconds

    private void OnTriggerEnter(Collider other)
    {
        
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

        
        car.drivespeed *= boostMultiplier;
        
        yield return new WaitForSeconds(boostDuration);

        
        car.drivespeed = originalSpeed;
        
    }
}
