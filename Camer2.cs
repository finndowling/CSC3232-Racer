using UnityEngine;

public class Camer2 : MonoBehaviour
{
    public GameObject Target;
    public GameObject T;
    public float speed = 1.5f;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        T = GameObject.FindGameObjectWithTag("Target");
    }

    void FixedUpdate()
    {
        if (Target == null || T == null)
            return;

        transform.LookAt(Target.transform);
        float car_Move = Vector3.Distance(transform.position, T.transform.position) * speed;
        transform.position = Vector3.MoveTowards(transform.position, T.transform.position, car_Move * Time.deltaTime);
    }
}
