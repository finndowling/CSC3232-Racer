using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer2 : MonoBehaviour
{
    public GameObject Target = null;
    public GameObject T = null;
    public float speed = 1.5f;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        T = GameObject.FindGameObjectWithTag("Target");

        if (Target == null)
        {
            Debug.LogError("Target with tag 'Player' not found!");
        }

        if (T == null)
        {
            Debug.LogError("GameObject with tag 'Target' not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        if (Target == null || T == null)
            return;

        this.transform.LookAt(Target.transform);
        float car_Move = Mathf.Abs(Vector3.Distance(this.transform.position, T.transform.position) * speed);
        this.transform.position = Vector3.MoveTowards(this.transform.position, T.transform.position, car_Move * Time.deltaTime);
    }
}
