using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().decreaseHealth = Player.instance.damage;
        }

        if (other.CompareTag("Boss"))
        {
          
            other.GetComponentInParent<BossController>().health -= (int) Player.instance.damage;
        }
    }
}
