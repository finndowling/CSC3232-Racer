using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player (assign in the Inspector)
    private NavMeshAgent agent; // Reference to the NavMeshAgent component

    void Start()
    {
        // Get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            Debug.LogError("Player reference is missing. Assign the player in the Inspector.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Set the agent's destination to the player's position
            agent.SetDestination(player.position);
        }
    }
}
