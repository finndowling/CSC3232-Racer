using UnityEngine;

public class ExhaustControl : MonoBehaviour
{
    public ParticleSystem leftExhaust;  
    public ParticleSystem rightExhaust; 

    void Update()
    {
        // Check if the car is accelerating 
        if (Input.GetKey(KeyCode.W)) 
        {
            
            if (!leftExhaust.isPlaying)
                leftExhaust.Play();

            
            if (!rightExhaust.isPlaying)
                rightExhaust.Play();
        }
        else
        {
            
            if (leftExhaust.isPlaying)
                leftExhaust.Stop();

            
            if (rightExhaust.isPlaying)
                rightExhaust.Stop();
        }
    }
}
