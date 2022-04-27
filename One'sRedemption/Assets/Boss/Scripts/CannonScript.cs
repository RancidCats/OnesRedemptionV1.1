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
    private float _shootSpeed;
    [SerializeField]
    private float gravity;

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
        Vector3 direction = target.position - cannonSpawn.position;
        float distance = Vector3.Distance(cannonSpawn.position, target.position);


        GameObject cannonBall = Instantiate(this.cannonBall, cannonSpawn.position, Quaternion.identity);
        var sc = cannonBall.GetComponent<BallScript>();

        var time = Vector3.Distance(cannonSpawn.position, target.position) / _shootSpeed;

        var speed = new Vector3((target.position.x - cannonSpawn.position.x) / time,
            (target.position.y - cannonSpawn.position.y) / time - 0.5f * gravity * time,
            (target.position.z - cannonSpawn.position.z) / time);

        sc.Initial(speed, gravity);
    }
}
