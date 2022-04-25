using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] points;
    public int indexPoints;
    public Transform target;
    public float speed;
    public float rangeVision;
    public bool inRange;
    public LayerMask layerPlayer;

    private void Start()
    {
        inRange = false;
    }

    public void Update()
    {
        inRange = Physics.CheckSphere(transform.position, rangeVision, layerPlayer);


        if (inRange == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
        if (inRange == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[indexPoints].position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, points[indexPoints].position);
            transform.LookAt(new Vector3(points[indexPoints].position.x, transform.position.y, points[indexPoints].position.z));


            if (distance <= 0.1)
            {
                indexPoints++;
            }
            if (indexPoints >= points.Length)
            {
                indexPoints = 0;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeVision);
    }


}

