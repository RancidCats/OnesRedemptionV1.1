using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //Tag = Enemy, CAMBIAR PROPIEDAD DECREASE HEALT X METODO MAKEDAMAGE
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().decreaseHealth = Player.instance.damage;
        }

        //Tag = Obstacles
        if (other.CompareTag("Obstacles"))
        {
            other.GetComponentInParent<Obstacle>().ModifyHealth(0, (int)Player.instance.damage);
        }

        //Tag = Boss
        if (other.CompareTag("Boss"))
        { 
            other.GetComponentInParent<BossController>().ModifyHealth(0 ,(int) Player.instance.damage);
        }
    }
}
