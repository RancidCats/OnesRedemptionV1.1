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
    public Animator anim;

    private void Start()
    {
        inRange = false;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        inRange = Physics.CheckSphere(transform.position, rangeVision, layerPlayer);


        if (inRange == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            anim.SetBool("Run", true);
            anim.SetBool("Move", true);

        }
        if (inRange == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[indexPoints].position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, points[indexPoints].position);
            transform.LookAt(new Vector3(points[indexPoints].position.x, transform.position.y, points[indexPoints].position.z));
            anim.SetBool("Run", false);

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

    private void OnTriggerEnter(Collider collPlayer)
    {
        if (collPlayer.CompareTag("Player"))
        {
            anim.SetBool("Move", false);
            anim.SetBool("Attack", true);
        }
    }
    private void OnTriggerExit(Collider collPlayer)
    {
        if (collPlayer.CompareTag("Player"))
        {
            anim.SetBool("Move", true);
            anim.SetBool("Attack", false);
        }
    }
}

