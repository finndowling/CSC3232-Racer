using UnityEngine;

public class BlockingAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float detectionRange = 10f;
    public float blockingSpeed = 5f;
    public LayerMask carLayer;
    
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public Transform targetCar;
    [HideInInspector] public int currentPatrolIndex = 0;

    // States
    public Patrolling patrolling = new Patrolling();
    public Chase chase = new Chase();
    public Blocking blocking = new Blocking();
    public Engage engage; // Created dynamically when detecting a car

    private AISTateBase currentState;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        if (patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned!");
        }

        // Start in Patrolling state
        currentState = patrolling;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(AISTateBase newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public AISTateBase GetCurrentState()
    {
        return currentState;
    }

    public void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        Vector3 nextPoint = patrolPoints[currentPatrolIndex].position;
        rigidBody.velocity = (nextPoint - transform.position).normalized * blockingSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager gm = FindObjectOfType<gameManager>();
            if (gm != null)
            {
                gm.OnPlayerHitByBlockingAI();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}