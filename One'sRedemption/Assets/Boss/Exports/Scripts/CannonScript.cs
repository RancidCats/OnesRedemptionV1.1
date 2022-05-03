using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cannonBall;


    [SerializeField]
    private Transform cannonSpawn;
    public bool fire;
    [SerializeField]
    private Transform target;



    [SerializeField]
    private float h = 25;
    [SerializeField]
    private float gravity = -9.8f;

    void Start()
    {
        fire = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!fire)
        {
            fire = true;
            Fire();
        }
    }

    void Fire()
    {
        GameObject go = Instantiate(cannonBall, cannonSpawn.position, Quaternion.identity);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.velocity += CalculateLaunchData();
        rb.useGravity = true;
    }

    Vector3 CalculateLaunchData()
    {
        float displacementY = target.position.y - cannonSpawn.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - cannonSpawn.position.x, 0, target.position.z - cannonSpawn.position.z);

        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return velocityXZ + velocityY;

    }

}
