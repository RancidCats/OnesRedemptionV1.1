using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int damage;
    //public GameObject particlesPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().DecreaseHealth(damage);
        }
        //Vector3 spawnPos = this.gameObject.transform.position;
        //Instantiate(particlesPrefab, spawnPos, Quaternion.identity); //explosion
        if (!other.gameObject.CompareTag("Cannon")) 
            Destroy(gameObject);
    }
}
