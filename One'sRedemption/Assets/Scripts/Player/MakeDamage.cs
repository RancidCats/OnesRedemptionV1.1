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
          
            other.GetComponentInParent<BossController>().ModifyHealth(0 ,(int) Player.instance.damage);
        }
    }
}
