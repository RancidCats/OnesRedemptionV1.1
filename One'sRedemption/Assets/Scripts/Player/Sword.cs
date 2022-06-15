using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
      
       if (other.GetComponentInParent<IDamageable>() != null && !other.CompareTag("Player"))//Pregunto por el player porque sino le hace daño
       {
            Debug.Log(other.name);
           other.GetComponentInParent<IDamageable>().DecreaseHealth((int)Player.instance.damage);
       }

    }
}
