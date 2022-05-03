using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public int speed = 10;
    public Transform target;
    public int damage;
    
    private void Start()
    {
        Vector3.Lerp(transform.position, target.position, 1);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Terrain") || col.CompareTag("Player"))
        {
            //if (col.CompareTag("Player")) col.GetComponent<PlayerController>().currHp -= damage;
            Destroy(gameObject);
        }
    }
}
